using DC.Database.Context;

namespace DC.Database.Domain {

    public class ChestDBDomain {

        DatabaseContext dbContext;

        public ChestDBDomain() { }

        public void Inject(DatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public ChestTable GetByChestID(int chestID) {
            return dbContext.ChestDAO.GetChest(chestID);
        }

        public ChestTable GetOneByAccountID(int accountID) {
            return dbContext.ChestDAO.GetOneChestByAccountID(accountID);
        }

        public int Insert(ChestTable table) {
            int id = dbContext.ChestDAO.Insert(table);
            return id;
        }

    }

}