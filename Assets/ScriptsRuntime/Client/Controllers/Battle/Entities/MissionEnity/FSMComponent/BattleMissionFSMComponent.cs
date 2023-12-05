using ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity.FSMComponent.Enum;
using ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity.FSMComponent.StateModel;

namespace ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity.FSMComponent {

    public class BattleMissionFSMComponent {

        BattleMissionFSMStatus status;
        public BattleMissionFSMStatus Status => status;

        BattleMissionActMovingForwardStateModel actMovingForwardStateModel;
        public BattleMissionActMovingForwardStateModel ActMovingForwardStateModel => actMovingForwardStateModel;

        BattleMissionActActingStateModel actActingStateModel;
        public BattleMissionActActingStateModel ActActingStateModel => actActingStateModel;

        BattleMissionActMovingBackwardStateModel actMovingBackwardStateModel;
        public BattleMissionActMovingBackwardStateModel ActMovingBackwardStateModel => actMovingBackwardStateModel;

        public BattleMissionFSMComponent() {
            actMovingForwardStateModel = new BattleMissionActMovingForwardStateModel();
            actActingStateModel = new BattleMissionActActingStateModel();
            actMovingBackwardStateModel = new BattleMissionActMovingBackwardStateModel();
        }

        // 战斗计算

        // 回合: 前进
        public void EnterActMovingForward() {
            status = BattleMissionFSMStatus.ActMovingForward;
            var stateModel = actMovingForwardStateModel;
            stateModel.isEntering = true;
            stateModel.maintainTimeSec = 1f;
            stateModel.time = 0;
        }

        // 回合: 行动
        public void EnterActActing() {
            status = BattleMissionFSMStatus.ActActing;
            var stateModel = actActingStateModel;
            stateModel.isEntering = true;
            stateModel.maintainTimeSec = 1f;
            stateModel.timeSec = 0;
            stateModel.isActed = false;
        }

        // 回合: 后退
        public void EnterActMovingBackward() {
            status = BattleMissionFSMStatus.ActMovingBackward;
            var stateModel = actMovingBackwardStateModel;
            stateModel.isEntering = true;
            stateModel.maintainTimeSec = 1f;
            stateModel.time = 0;
        }

        // 等待玩家操作
        public void EnterWaiting() {
            status = BattleMissionFSMStatus.Waiting;
        }

    }

}