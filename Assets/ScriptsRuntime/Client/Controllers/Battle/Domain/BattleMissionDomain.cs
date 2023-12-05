using DC;
using ScriptsRuntime.Client.Controllers.Battle.Context;
using ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity;
using ScriptsRuntime.Client.Controllers.Battle.Factory;

namespace ScriptsRuntime.Client.Controllers.Battle.Domain {

    public class BattleMissionDomain {

        BattleContext battleContext;
        BattleFactory battleFactory;

        BattleMinionDomain minionDomain;

        public BattleMissionDomain() { }

        public void Inject(BattleContext battleContext, BattleFactory battleFactory, BattleMinionDomain minionDomain) {
            this.battleContext = battleContext;
            this.battleFactory = battleFactory;

            this.minionDomain = minionDomain;
        }

        public BattleMissionEntity SpawnMissionByTemplate(int chapter, int level) {

            // 1. Battle
            var repo = battleContext.MissionRepo;
            var mission = battleFactory.CreateMission(chapter, level);
            repo.Add(mission);

            // 2. Minion
            var role = minionDomain.SpawnMinionByTemplate(1000, AllyStatus.Player);
            role.Init(mission.MinionVector3);

            // 3. Monster
            var enemy = minionDomain.SpawnMinionByTemplate(2000, AllyStatus.Computer);
            enemy.Init(mission.MonsterVector3);

            return mission;

        }

    }
}