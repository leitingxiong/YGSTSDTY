using LiteDB;

namespace DC.Database.Repository {

    public class ChestDAO {

        LiteDatabase conn;
        ILiteCollection<ChestTable> chestTable;

        public ChestDAO() { }

        public void Inject(LiteDatabase conn) {
            this.conn = conn;
        }

        public void Init() {
            chestTable = conn.GetCollection<ChestTable>(ChestTable.TableName);
        }

        public BsonValue Insert(ChestTable chest) {
            return chestTable.Insert(chest);
        }

        public void Delete(ChestTable chest) {
            chestTable.Delete(chest.Id);
        }

        public void Update(ChestTable chest) {
            chestTable.Update(chest);
        }

        public ChestTable GetChest(int id) {
            return chestTable.FindOne(x => x.Id == id);
        }

        public ChestTable GetOneChestByAccountID(int accountID) {
            return chestTable.FindOne(x => x.AccountId == accountID);
        }

    }
}