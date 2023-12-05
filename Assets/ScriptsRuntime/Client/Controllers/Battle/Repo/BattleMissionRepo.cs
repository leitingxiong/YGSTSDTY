using System;
using System.Collections.Generic;
using ScriptsRuntime.Client.Controllers.Battle.Entities.MissionEnity;

namespace ScriptsRuntime.Client.Controllers.Battle.Repo {

    public class BattleMissionRepo {

        Dictionary<int, BattleMissionEntity> all;

        BattleMissionEntity battleEntity;
        public BattleMissionEntity BattleEntity => battleEntity;

        public BattleMissionRepo() {
            this.all = new Dictionary<int, BattleMissionEntity>();
        }

        public void Add(BattleMissionEntity entity) {
            this.all.Add(entity.EntityID, entity);
            battleEntity = entity;
        }

        public bool TryGet(int entityID, out BattleMissionEntity entity) {
            return all.TryGetValue(entityID, out entity);
        }

        public void Foreach(Action<BattleMissionEntity> action) {
            foreach (var entity in all.Values) {
                action(entity);
            }
        }

        public BattleMissionEntity GetFirst() {
            return battleEntity;
        }

        public void Remove(int entityID) {
            all.Remove(entityID);
        }

        public void Clear() {
            all.Clear();
        }

    }
}