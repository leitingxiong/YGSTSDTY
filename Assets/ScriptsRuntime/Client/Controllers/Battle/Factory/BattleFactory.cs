using UnityEngine;
using DC.BattleBusiness.Context;
using DC.Infrastructure.Context;

namespace DC.BattleBusiness.Factory {

    public class BattleFactory {

        InfraContext infraContext;        
        BattleContext battleContext;

        public BattleFactory() { }

        public void Inject(InfraContext infraContext, BattleContext battleContext) {
            this.infraContext = infraContext;
            this.battleContext = battleContext;
        }

        public BattleMinionEntity CreateMinionEntity(int templateID, AllyStatus allyStatus) {
            // - Template
            var template = infraContext.TemplateCore.MinionTemplate;
            bool has = template.TryGet(templateID, out var tm);
            if (!has) {
                DCLog.Error("BattleFactory.CreateMinionEntity: template not found: " + templateID);
                return null;
            }

            // - Assets
            var entityAssets = infraContext.AssetsCore.EntityAssets;
            has = entityAssets.TryGetMinion(out var prefab);
            if (!has) {
                DCLog.Error("BattleFactory.CreateMinionEntity: prefab not found: " + templateID);
                return null;
            }

            var modAssets = infraContext.AssetsCore.ModAssets;
            has = modAssets.TryGetMinionMod(tm.modName, out var modPrefab);
            if (!has) {
                DCLog.Error("BattleFactory.CreateMinionEntity: mod prefab not found: " + templateID);
                return null;
            }

            // - Instantiate
            BattleMinionEntity entity = GameObject.Instantiate(prefab).GetComponent<BattleMinionEntity>();
            entity.Ctor();

            // - Mod
            var mod = GameObject.Instantiate(modPrefab, entity.BodyRoot).GetComponent<BattleMinionMod>();
            mod.Ctor();

            // - ID
            var idService = battleContext.IDService;
            entity.SetEntityID(idService.PickMinionID());

            // - Ally
            entity.SetAllyStatus(allyStatus);

            // - Set Model
            var attrCom = entity.AttributeComponent;
            attrCom.SetAtk(tm.atk);
            attrCom.SetHp(tm.hp);
            attrCom.SetHpMax(tm.hp);
            attrCom.SetDef(tm.def);
            attrCom.SetWidth(tm.width);
            attrCom.SetHeight(tm.height);

            // - HUD
            var hudHpBar = entity.HUDHpBar;
            hudHpBar.SetHp(attrCom.Hp, attrCom.HpMax);

            return entity;

        }

        public BattleMissionEntity CreateMission(int chapter, int level) {

            var template = infraContext.TemplateCore.MissionTemplate;
            bool has = template.TryGet(chapter, level, out var tm);
            if (!has) {
                DCLog.Error("BattleFactory.CreateBattle: template not found: " + chapter + level);
                return null;
            }

            BattleMissionEntity entity = new BattleMissionEntity();
            entity.SetMinionVector3(tm.minionVector3);
            entity.SetMonsterVector3(tm.monsterVector3);

            return entity;
        }
    }
}