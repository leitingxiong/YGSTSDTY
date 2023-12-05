using DC.Database.Context;

namespace DC.Database.Domain {

    public class MinionDBDomain {

        DatabaseContext dbContext;

        public MinionDBDomain() { }

        public void Inject(DatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public MinionTable GetByMinionID(int minionID) {
            return dbContext.MinionDAO.GetMinion(minionID);
        }

        public MinionTable GetOneByAccountID(int accountID) {
            var model = dbContext.MinionDAO.GetOneMinionByAccountID(accountID);
            if (model != null) {
                return model;
            }
            return null;
        }

        public int Insert(MinionTable table) {
            int id = dbContext.MinionDAO.Insert(table);
            return id;
        }

    }

}