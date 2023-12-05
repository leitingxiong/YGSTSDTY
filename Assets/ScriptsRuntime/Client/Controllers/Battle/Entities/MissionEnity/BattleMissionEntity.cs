using ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity.FSMComponent;
using ScriptsRuntime.Client.Controllers.Battle.Model;
using UnityEngine;

namespace ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity {

    public class BattleMissionEntity {

        int entityID;
        public int EntityID => entityID;
        public void SetEntityID(int value) => entityID = value;

        int chapter;
        public int Chapter => chapter;

        int level;
        public int Level => level;

        Vector3 minionVector3;
        public Vector3 MinionVector3 => minionVector3;
        public void SetMinionVector3(Vector3 v) => minionVector3 = v;

        Vector3 monsterVector3;
        public Vector3 MonsterVector3 => monsterVector3;
        public void SetMonsterVector3(Vector3 v) => monsterVector3 = v;

        // 战斗结果
        public CalculationResultModel resultModel;

        // FSM
        BattleMissionFSMComponent fsm;
        public BattleMissionFSMComponent FSM => fsm;

        // TEMP
        public int turnIndex;
        public int actIndex;

        public BattleMissionEntity() {
            this.resultModel = new CalculationResultModel();
            this.fsm = new BattleMissionFSMComponent();

        }

    }
}
