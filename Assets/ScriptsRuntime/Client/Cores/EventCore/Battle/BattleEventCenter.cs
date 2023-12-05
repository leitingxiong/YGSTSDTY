namespace DC.Events {

    public class BattleEventCenter {

        (bool, int, int) enterBattleEvent;
        public (bool isEnter, int chapter, int level) EnterBattleEvent => enterBattleEvent;
        public void SetEnterBattleEvent((bool isEnter, int chapter, int level) value) => enterBattleEvent = value;

        public BattleEventCenter() { }

    }

}