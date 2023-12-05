using DC.Database.Context;

namespace DC.Database.Domain {

    public class AccountDBDomain {

        DatabaseContext dbContext;

        public AccountDBDomain() { }

        public void Inject(DatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public AccountTable GetByAccountName(string accountName) {
            return dbContext.AccountDAO.GetAccountByName(accountName);
        }

        public int Insert(AccountTable table) {
            int id = dbContext.AccountDAO.Insert(table);
            return id;
        }

    }

}