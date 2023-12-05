using LiteDB;

namespace DC.Database.Repository {

    public class MinionDAO {

        LiteDatabase conn;
        ILiteCollection<MinionTable> minionTable;

        public MinionDAO() { }

        public void Inject(LiteDatabase conn) {
            this.conn = conn;
        }

        public void Init() {
            minionTable = conn.GetCollection<MinionTable>(MinionTable.TableName);
        }

        public BsonValue Insert(MinionTable minion) {
            return minionTable.Insert(minion);
        }

        public void Delete(MinionTable minion) {
            minionTable.Delete(minion.Id);
        }

        public void Update(MinionTable minion) {
            minionTable.Update(minion);
        }

        public MinionTable GetMinion(int id) {
            return minionTable.FindOne(x => x.Id == id);
        }

        public MinionTable GetOneMinionByAccountID(int accountID) {
            return minionTable.FindOne(x => x.AccountId == accountID);
        }

    }

}