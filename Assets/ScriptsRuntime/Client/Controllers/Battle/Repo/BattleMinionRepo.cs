using System;
using System.Collections.Generic;

namespace DC.BattleBusiness {

    public class BattleMinionRepo {

        Dictionary<int, BattleMinionEntity> all;

        public BattleMinionRepo() {
            this.all = new Dictionary<int, BattleMinionEntity>();
        }

        public void Add(BattleMinionEntity entity) {
            this.all.Add(entity.EntityID, entity);
        }

        public bool TryGet(int entityID, out BattleMinionEntity entity) {
            return all.TryGetValue(entityID, out entity);
        }

        public BattleMinionEntity GetRole() {
            foreach (var entity in all.Values) {
                if (entity.AllyStatus == AllyStatus.Player) {
                    return entity;
                }
            }
            return null;
        }

        public BattleMinionEntity[] GetAllEnemy() {
            var list = new List<BattleMinionEntity>();
            foreach (var entity in all.Values) {
                if (entity.AllyStatus == AllyStatus.Computer) {
                    list.Add(entity);
                }
            }
            return list.ToArray();
        }

        public void Foreach(Action<BattleMinionEntity> action) {
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