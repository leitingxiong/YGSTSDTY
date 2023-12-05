namespace ScriptsRuntime.Client.Controllers.Battle.Entities.CommonComponent {

    public class PlayerAttributeComponent {

        int hp;
        public int Hp => hp;
        public void SetHp(int value) => hp = value;

        int hpMax;
        public int HpMax => hpMax;
        public void SetHpMax(int value) => hpMax = value;

        int atk;
        public int Atk => atk;
        public void SetAtk(int value) => atk = value;

        int def;
        public int Def => def;
        public void SetDef(int value) => def = value;

        int width;
        public int Width => width;
        public void SetWidth(int value) => width = value;

        int height;
        public int Height => height;
        public void SetHeight(int value) => height = value;

        public PlayerAttributeComponent() { }

    }
}