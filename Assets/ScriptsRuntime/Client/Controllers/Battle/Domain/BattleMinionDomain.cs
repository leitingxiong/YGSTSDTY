using DC;
using ScriptsRuntime.Client.Controllers.Battle.Context;
using ScriptsRuntime.Client.Controllers.Battle.Entities.Minion;
using ScriptsRuntime.Client.Controllers.Battle.Factory;

namespace ScriptsRuntime.Client.Controllers.Battle.Domain {

    public class BattleMinionDomain {

        BattleContext battleContext;
        BattleFactory battleFactory;

        public BattleMinionDomain() { }

        public void Inject(BattleContext battleContext, BattleFactory battleFactory) {
            this.battleContext = battleContext;
            this.battleFactory = battleFactory;
        }

        public PlayerAttributeEntity SpawnMinionByTemplate(int templateID, AllyStatus allyStatus) {
            var repo = battleContext.MinionRepo;
            var minion = battleFactory.CreateMinionEntity(templateID, allyStatus);
            repo.Add(minion);

            return minion;
        }

    }

}