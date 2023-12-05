namespace DC.BattleBusiness {

    public class IDService {

        int minionIDRecord;

        public IDService() {
            this.minionIDRecord = 0;
        }

        public int PickMinionID() {
            minionIDRecord += 1;
            return minionIDRecord;
        }

    }
}
