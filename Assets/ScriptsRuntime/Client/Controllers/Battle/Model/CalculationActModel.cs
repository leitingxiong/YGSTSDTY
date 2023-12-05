using DC;

namespace ScriptsRuntime.Client.Controllers.Battle.Model {

    // 一次行动
    public class CalculationActModel {

        // ==== Caster ====
        public EntityType casterEntityType;
        public int casterEntityID;

        // ==== Caster Behaviour ====
        public ActType actType; // 0: 不动; 1: 攻击; 2: 治疗

        // ==== Victim =====
        public EntityType victimEntityType;
        public int victimEntityID;

        public CalculationActModel() { }

    }

}