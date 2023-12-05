using DC.BattleBusiness.Context;
using DC.BattleBusiness.Factory;

namespace DC.BattleBusiness.Domain {

    public class BattleMinionDomain {

        BattleContext battleContext;
        BattleFactory battleFactory;

        public BattleMinionDomain() { }

        public void Inject(BattleContext battleContext, BattleFactory battleFactory) {
            this.battleContext = battleContext;
            this.battleFactory = battleFactory;
        }

        public BattleMinionEntity SpawnMinionByTemplate(int templateID, AllyStatus allyStatus) {
            var repo = battleContext.MinionRepo;
            var minion = battleFactory.CreateMinionEntity(templateID, allyStatus);
            repo.Add(minion);

            return minion;
        }

    }

}