using DC.Infrastructure.Context;
using ScriptsRuntime.Client.Applications.UIApplication;
using ScriptsRuntime.Client.Applications.UIApplication.Panel;
using ScriptsRuntime.Client.Controllers.Battle.Context;
using ScriptsRuntime.Client.Controllers.Battle.Factory;

namespace ScriptsRuntime.Client.Controllers.Battle {

    // 战斗时
    public class YardController {

        InfraContext infraContext;

        BattleContext battleContext = new();
        BattleFactory battleFactory = new();

        public void Inject(InfraContext infraContext, UIApp uIApp) {
            
            this.infraContext = infraContext;

            battleFactory.Inject(infraContext, battleContext);
            battleContext.Inject(uIApp);

        }

        public void Init() {

        }

        public void Enter(int chapter, int level) {

            var uiBattle = battleContext.UIApp.Open<UI_Battle>();

        }

        public void Tick(float dt) {
            // var missionRepo = battleContext.MissionRepo();
        }

        public void Exit() {

        }

    }

}