using System.Collections.Generic;
using DC.BattleBusiness.Context;
using DC.BattleBusiness.Factory;

namespace DC.BattleBusiness.Domain {

    public class BattleCalculationDomain {

        BattleContext battleContext;

        public BattleCalculationDomain() { }

        public void Inject(BattleContext battleContext) {
            this.battleContext = battleContext;
        }

        // 战斗计算 Loop: 运算结果
        public void Calculate(BattleMissionEntity battle) {

            var minionRepo = battleContext.MinionRepo;
            BattleMinionEntity role = minionRepo.GetRole();
            BattleMinionEntity[] enemyArray = minionRepo.GetAllEnemy();

            // 4. 计算战斗结果
            // 4.1 规则: 谁先出手 -> Minion
            // 4.2 规则: 如何胜利 -> 一方全员死亡

            // 找到 Minion
            // 找到所有 Monster
            // 战斗计算 Enter: 生成纯数据 Model
            var roleClone = role.ToCalculationModel();
            var enemyArrayClone = new CalculationMinionModel[enemyArray.Length];
            for (int i = 0; i < enemyArrayClone.Length; i += 1) {
                enemyArrayClone[i] = enemyArray[i].ToCalculationModel();
            }

            // 战斗计算 Loop
            int turnIndex = 0;
            bool isMinionAct = true;
            CalculationResultModel resultModel = battle.resultModel;
            List<CalculationTurnModel> turnList = new List<CalculationTurnModel>();
            while (!IsBattleEnd(roleClone, enemyArrayClone)) {

                int actIndex = 0;
                int totalAct = 1 + enemyArrayClone.Length;
                int monsterAct = 0;
                CalculationTurnModel turnModel = new CalculationTurnModel();
                turnModel.actArray = new CalculationActModel[totalAct];
                turnModel.turnIndex = turnIndex;

                while (actIndex < totalAct) {

                    if (isMinionAct) {

                        CalculationActModel actModel = new CalculationActModel();
                        actModel.casterEntityType = roleClone.entityType;
                        actModel.casterEntityID = roleClone.entityID;

                        actModel.actType = ActType.Attack;

                        var victimMonster = enemyArrayClone[0];
                        actModel.victimEntityType = victimMonster.entityType;
                        actModel.victimEntityID = victimMonster.entityID;

                        turnModel.actArray[actIndex] = actModel;

                        victimMonster.hp -= roleClone.atk;
                        actIndex += 1;

                        isMinionAct = !isMinionAct;

                    } else {

                        while (monsterAct < enemyArrayClone.Length) {

                            var monster = enemyArrayClone[monsterAct];
                            CalculationActModel actModel = new CalculationActModel();
                            actModel.casterEntityType = monster.entityType;
                            actModel.casterEntityID = monster.entityID;

                            actModel.actType = ActType.Attack;

                            var victim = roleClone;
                            actModel.victimEntityType = roleClone.entityType;
                            actModel.victimEntityID = roleClone.entityID;

                            turnModel.actArray[actIndex] = actModel;

                            victim.hp -= monster.atk;

                            actIndex += 1;
                            monsterAct += 1;

                        }
                        isMinionAct = !isMinionAct;
                    }
                }
                turnList.Add(turnModel);
                turnIndex += 1;
            }
            resultModel.totalTurn = turnIndex;
            resultModel.turnArray = turnList.ToArray();
            DCLog.Log(resultModel.turnArray);

            // 战斗计算 Exit:
            bool isMinionWin = IsRoleDead(roleClone) ? false : true;
            DCLog.Log($"战斗结果出来了 isMinionWin:{isMinionWin}, totalTurn:{turnIndex}");

            battle.FSM.EnterActMovingForward();

        }

        bool IsBattleEnd(CalculationMinionModel role, CalculationMinionModel[] enemyArray) {
            if (IsAllEnemyDead(enemyArray) || IsRoleDead(role)) {
                return true;
            } else {
                return false;
            }
        }

        bool IsAllEnemyDead(CalculationMinionModel[] monsters) {
            for (int i = 0; i < monsters.Length; i++) {
                var monster = monsters[i];
                if (monster.hp > 0) {
                    return false;
                }
            }
            return true;
        }

        bool IsRoleDead(CalculationMinionModel minion) {
            return minion.hp <= 0;
        }

    }

}