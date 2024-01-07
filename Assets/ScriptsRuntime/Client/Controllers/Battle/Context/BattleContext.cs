using ScriptsRuntime.Client.Applications.UIApplication;
using ScriptsRuntime.Client.Controllers.Battle.Entities;
using ScriptsRuntime.Client.Controllers.Battle.Repo;
using ScriptsRuntime.Client.Controllers.Battle.Service;

namespace ScriptsRuntime.Client.Controllers.Battle.Context {

    public class BattleContext {

        // ==== State ====
        BattleStateEntity stateEntity;
        public BattleStateEntity StateEntity => stateEntity;

        // ==== Application ====
        UIApp uiApp;
        public UIApp UIApp => uiApp;

        // ==== Repositories ====

        BattleMissionRepo missionRepo;
        public BattleMissionRepo MissionRepo => missionRepo;

        // ==== Service ====
        IDService idService;
        public IDService IDService => idService;

        public BattleContext() {
            this.stateEntity = new BattleStateEntity();

            this.missionRepo = new BattleMissionRepo();

            this.idService = new IDService();
        }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

    }
}