using ScriptsRuntime.Client.Controllers.Battle.Generic.Enum;

namespace ScriptsRuntime.Client.Controllers.Battle.Entities {

    public class BattleStateEntity {

        BattleStageFlag stageFlag;
        public BattleStageFlag StageFlag => stageFlag;
        public void SetStageFlag(BattleStageFlag value) => stageFlag = value;

        public BattleStateEntity() { }

    }

}