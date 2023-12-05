using UnityEngine;
using UnityEngine.UI;

namespace DC.BattleBusiness {

    public class BattleMinionEntity : MonoBehaviour {

        // ==== Logic ====
        // - Identity
        public EntityType EntityType => EntityType.Minion;

        int entityID;
        public int EntityID => entityID;
        public void SetEntityID(int value) => entityID = value;

        AllyStatus allyStatus;
        public AllyStatus AllyStatus => allyStatus;
        public void SetAllyStatus(AllyStatus value) => allyStatus = value;

        public bool IsLeftSide => allyStatus == AllyStatus.Player;

        // - Attribute
        BattleMinionAttributeComponent attributeComponent;
        public BattleMinionAttributeComponent AttributeComponent => attributeComponent;

        // - Movement
        Vector3 originPosition;
        public Vector3 OriginPosition => originPosition;

        public Vector3 Position => transform.position;
        public void SetPosition(Vector3 value) => transform.position = value;

        // ==== Renderer ====
        // - Body
        Transform bodyRoot;
        public Transform BodyRoot => bodyRoot;

        // - Root
        Transform hudRoot;
        public Transform HUDRoot => hudRoot;

        // - BloodBar
        HUDHpBar hudHpBar;
        public HUDHpBar HUDHpBar => hudHpBar;

        public void Ctor() {
            this.attributeComponent = new BattleMinionAttributeComponent();

            bodyRoot = transform.Find("body");
            hudRoot = bodyRoot.Find("hud_root");

            hudHpBar = hudRoot.Find("hud_hpBar").GetComponent<HUDHpBar>();
            hudHpBar.Ctor();

            Debug.Assert(bodyRoot != null);
            Debug.Assert(hudRoot != null);
            Debug.Assert(hudHpBar != null);
        }

        public void Init(Vector3 originPosition) {
            this.originPosition = originPosition;
            SetPosition(originPosition);
        }
        
        public CalculationMinionModel ToCalculationModel() {
            var clone = new CalculationMinionModel();
            clone.entityType = EntityType;
            clone.entityID = entityID;
            clone.hp = attributeComponent.Hp;
            clone.hpMax = attributeComponent.HpMax;
            clone.atk = attributeComponent.Atk;
            clone.def = attributeComponent.Def;
            return clone;
        }

    }
}