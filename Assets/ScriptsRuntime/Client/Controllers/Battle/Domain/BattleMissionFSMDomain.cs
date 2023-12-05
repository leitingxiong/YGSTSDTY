using System.Collections.Generic;
using UnityEngine;
using DC.BattleBusiness.Context;

namespace DC.BattleBusiness.Domain {

    public class BattleMissionFSMDomain {

        BattleContext battleContext;

        public BattleMissionFSMDomain() { }

        public void Inject(BattleContext battleContext) {
            this.battleContext = battleContext;
        }

        public void ApplyFSM(BattleMissionEntity mission, float dt) {

            ApplyAny(mission, dt);

            var fsm = mission.FSM;
            var status = fsm.Status;
            if (status == BattleMissionFSMStatus.Calculate) {
                DCLog.Error("BattleFSMStatus.Calculate 未实现");
            } else if (status == BattleMissionFSMStatus.ActMovingForward) {
                ApplyActMovingForward(mission, dt);
            } else if (status == BattleMissionFSMStatus.ActActing) {
                ApplyActActing(mission, dt);
            } else if (status == BattleMissionFSMStatus.ActMovingBackward) {
                ApplyActMovingBackward(mission, dt);
            } else if (status == BattleMissionFSMStatus.Waiting) {
                ApplyWaiting(mission, dt);
            } else {
                DCLog.Error("未知的 BattleFSMStatus");
            }

        }

        void ApplyAny(BattleMissionEntity mission, float dt) {
            // 场景事件
        }

        // ActMovingForward
        void ApplyActMovingForward(BattleMissionEntity mission, float dt) {

            // Enter
            var fsm = mission.FSM;
            var stateModel = fsm.ActMovingForwardStateModel;
            if (stateModel.isEntering) {
                stateModel.isEntering = false;
                // TODO: 表现模型
                return;
            }

            // Execute
            var resultModel = mission.resultModel;
            var act = resultModel.GetCurrentAct(mission.turnIndex, mission.actIndex);
            if (act == null) {
                // 找不到 act, 退出
                fsm.EnterWaiting();
                return;
            }

            var minionRepo = battleContext.MinionRepo;
            bool hasCaster = minionRepo.TryGet(act.casterEntityID, out var caster);
            bool hasVictim = minionRepo.TryGet(act.victimEntityID, out var victim);
            if (!hasCaster || !hasVictim) {
                // 找不到施法者或受害者, 退出
                DCLog.Error($"是否有Caster:{hasCaster}, 是否有Victim:{hasVictim}");
                return;
            }

            // 1. 获取双方的距离
            Vector3 casterPos = caster.Position;
            Vector3 victimPos = victim.Position;
            Vector3 bodyDiff = victim.IsLeftSide ? Vector3.left * 1.2f : Vector3.right * 1.2f;
            Vector3 diff = victimPos - casterPos - bodyDiff;

            // 2. 计算步进
            float time = stateModel.time;
            time += dt;
            stateModel.time = time;

            float timePercent = time / stateModel.maintainTimeSec;
            diff *= timePercent;

            // 4. 更新表现模型
            caster.SetPosition(casterPos + diff);

            // 5. 判断是否到达目标 -> 进入 ActActing
            if (time >= stateModel.maintainTimeSec) {
                fsm.EnterActActing();
            }

        }

        // ActActing
        void ApplyActActing(BattleMissionEntity mission, float dt) {

            // Enter
            var fsm = mission.FSM;
            var stateModel = fsm.ActActingStateModel;
            if (stateModel.isEntering) {
                stateModel.isEntering = false;
                return;
            }

            // Execute
            var resultModel = mission.resultModel;
            var act = resultModel.GetCurrentAct(mission.turnIndex, mission.actIndex);
            var minionRepo = battleContext.MinionRepo;
            bool hasCaster = minionRepo.TryGet(act.casterEntityID, out var caster);
            bool hasVictim = minionRepo.TryGet(act.victimEntityID, out var victim);
            if (!hasCaster || !hasVictim) {
                // 找不到施法者或受害者, 退出
                DCLog.Error($"是否有Caster:{hasCaster}, 是否有Victim:{hasVictim}");
                return;
            }

            if (!stateModel.isActed) {

                if (act.actType == ActType.Attack) {
                    // TODO: 扣血
                    var casterAttr = caster.AttributeComponent;
                    var victimAttr = victim.AttributeComponent;
                    victimAttr.SetHp(victimAttr.Hp - casterAttr.Atk);
                    victim.HUDHpBar.SetHp(victimAttr.Hp, victimAttr.HpMax);
                    DCLog.Log($"攻击方ID:{caster.EntityID} 攻击力:{casterAttr.Atk}; 受害方ID:{victim.EntityID}, 防御力:{victimAttr.Def}; 伤害:{casterAttr.Atk - victimAttr.Def};生命{victimAttr.Hp}");
                } else {
                    DCLog.Error("未知的 ActType: " + act.actType.ToString());
                }

                stateModel.isActed = true;

            }

            // 持续时长
            stateModel.timeSec += dt;
            if (stateModel.timeSec >= stateModel.maintainTimeSec) {
                fsm.EnterActMovingBackward();
            }

        }

        // ActMovingBackward
        void ApplyActMovingBackward(BattleMissionEntity mission, float dt) {
            // Enter
            var fsm = mission.FSM;
            var stateModel = fsm.ActMovingBackwardStateModel;
            if (stateModel.isEntering) {
                stateModel.isEntering = false;
                return;
            }

            // Execute
            var resultModel = mission.resultModel;
            var act = resultModel.GetCurrentAct(mission.turnIndex, mission.actIndex);
            var minionRepo = battleContext.MinionRepo;
            bool hasCaster = minionRepo.TryGet(act.casterEntityID, out var caster);
            if (!hasCaster) {
                // 找不到施法者, 退出
                DCLog.Error($"是否有Caster:{hasCaster}");
                return;
            }

            // 1. 获取回家的距离
            Vector3 casterPos = caster.Position;
            Vector3 casterOriginPos = caster.OriginPosition;
            Vector3 diff = casterPos - casterOriginPos;

            // 2. 计算步进
            float time = stateModel.time;
            time += dt;
            stateModel.time = time;

            float timePercent = 1 - time / stateModel.maintainTimeSec;
            diff *= timePercent;

            // 4. 更新表现模型
            caster.SetPosition(casterOriginPos + diff);

            // 5. 判断是否到达目标 -> 进入 ActMovingForward
            if (time >= stateModel.maintainTimeSec) {

                if (resultModel.IsOverAct(mission.turnIndex, mission.actIndex + 1)) {
                    // 本回合的行动结束
                    if (resultModel.IsOverTurn(mission.turnIndex + 1)) {

                        // 全回合结束
                        fsm.EnterWaiting();
                        return;
                    } else {
                        // 本回合未结束
                        mission.turnIndex += 1;
                        mission.actIndex = 0;
                        fsm.EnterActMovingForward();
                    }
                } else {
                    // 本回合内的行动未结束
                    mission.actIndex += 1;
                    fsm.EnterActMovingForward();
                }
            }

        }

        void ApplyWaiting(BattleMissionEntity mission, float dt) {
            DCLog.Log("全回合结束");
        }

    }

}