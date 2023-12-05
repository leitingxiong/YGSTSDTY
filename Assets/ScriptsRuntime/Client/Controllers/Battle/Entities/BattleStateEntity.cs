namespace DC.BattleBusiness {

    public class BattleStateEntity {

        BattleStageFlag stageFlag;
        public BattleStageFlag StageFlag => stageFlag;
        public void SetStageFlag(BattleStageFlag value) => stageFlag = value;

        public BattleStateEntity() { }

    }

}