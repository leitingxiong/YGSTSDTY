using DC.Infrastructure.Context;
using ScriptsRuntime.Client.Controllers.Battle.Domain;
using ScriptsRuntime.Client.Controllers.Battle.Factory;

namespace ScriptsRuntime.Client.Controllers.Battle.Context {

    public class AllBattleDomain {

        BattleCalculationDomain calculationDomain;
        public BattleCalculationDomain CalculationDomain => calculationDomain;

        BattleMinionDomain minionDomain;
        public BattleMinionDomain MinionDomain => minionDomain;

        BattleMissionDomain missionDomain;
        public BattleMissionDomain MissionDomain => missionDomain;

        BattleMissionFSMDomain missionFSMDomain;
        public BattleMissionFSMDomain MissionFSMDomain => missionFSMDomain;

        public AllBattleDomain() {
            this.calculationDomain = new BattleCalculationDomain();
            this.minionDomain = new BattleMinionDomain();
            this.missionDomain = new BattleMissionDomain();
            this.missionFSMDomain = new BattleMissionFSMDomain();
        }

        public void Inject(InfraContext infraContext, BattleContext battleContext, BattleFactory battleFactory) {
            this.calculationDomain.Inject(battleContext);
            this.minionDomain.Inject(battleContext, battleFactory);
            this.missionDomain.Inject(battleContext, battleFactory, minionDomain);
            this.missionFSMDomain.Inject(battleContext);
        }

    }

}

