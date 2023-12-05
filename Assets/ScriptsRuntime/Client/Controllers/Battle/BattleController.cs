using DC.UIApplication;
using DC.Infrastructure.Context;
using DC.BattleBusiness.Context;
using DC.BattleBusiness.Factory;

namespace DC.BattleBusiness.Controller {

    // 战斗时
    public class BattleController {

        InfraContext infraContext;

        AllBattleDomain allBattleDomain;
        BattleContext battleContext;
        BattleFactory battleFactory;

        public BattleController() {
            this.allBattleDomain = new AllBattleDomain();
            this.battleContext = new BattleContext();
            this.battleFactory = new BattleFactory();
        }

        public void Inject(InfraContext infraContext, UIApp uIApp) {
            
            this.infraContext = infraContext;

            battleFactory.Inject(infraContext, battleContext);
            battleContext.Inject(uIApp);
            allBattleDomain.Inject(infraContext, battleContext, battleFactory);

        }

        public void Init() {

        }

        public void Enter(int chapter, int level) {

            // 0. Load Player DB

            // 1. Load First Mission

            // 2. Spawn Mission: Monsters

            // 3. Spawn Minion

            // 4. Open UI
            var uiBattle = battleContext.UIApp.Open<UI_Battle>();
            allBattleDomain.MissionDomain.SpawnMissionByTemplate(chapter,level);

        }

        public void Tick(float dt) {
            // var missionRepo = battleContext.MissionRepo();
        }

        public void Exit() {

        }

    }

}