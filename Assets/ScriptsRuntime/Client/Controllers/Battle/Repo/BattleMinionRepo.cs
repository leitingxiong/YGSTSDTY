using System;
using System.Collections.Generic;
using DC;
using ScriptsRuntime.Client.Controllers.Battle.Entities.Minion;

namespace ScriptsRuntime.Client.Controllers.Battle.Repo {

    public class BattleMinionRepo {

        Dictionary<int, PlayerAttributeEntity> all;

        public BattleMinionRepo() {
            this.all = new Dictionary<int, PlayerAttributeEntity>();
        }

        public void Add(PlayerAttributeEntity entity) {
            this.all.Add(entity.EntityID, entity);
        }

        public bool TryGet(int entityID, out PlayerAttributeEntity entity) {
            return all.TryGetValue(entityID, out entity);
        }

        public PlayerAttributeEntity GetRole() {
            foreach (var entity in all.Values) {
                if (entity.AllyStatus == AllyStatus.Player) {
                    return entity;
                }
            }
            return null;
        }

        public PlayerAttributeEntity[] GetAllEnemy() {
            var list = new List<PlayerAttributeEntity>();
            foreach (var entity in all.Values) {
                if (entity.AllyStatus == AllyStatus.Computer) {
                    list.Add(entity);
                }
            }
            return list.ToArray();
        }

        public void Foreach(Action<PlayerAttributeEntity> action) {
            foreach (var entity in all.Values) {
                action(entity);
            }
        }

        public void Remove(int entityID) {
            all.Remove(entityID);
        }

        public void Clear() {
            all.Clear();
        }

    }
}