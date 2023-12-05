namespace ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity.FSMComponent.Enum {

    public enum BattleMissionFSMStatus {

        Calculate,  // 战斗计算

        ActMovingForward,
        ActActing,
        ActMovingBackward,

        Waiting,    // 等待玩家操作

    }
}