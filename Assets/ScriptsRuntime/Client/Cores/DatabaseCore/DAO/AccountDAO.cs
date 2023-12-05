using LiteDB;

namespace DC.Database.Repository {

    // 增删改查
    public class AccountDAO {

        LiteDatabase conn;
        ILiteCollection<AccountTable> accountTable;

        public AccountDAO() { }

        public void Inject(LiteDatabase conn) {
            this.conn = conn;
        }

        public void Init() {
            accountTable = conn.GetCollection<AccountTable>(AccountTable.TableName);
        }

        public BsonValue Insert(AccountTable account) {
            return accountTable.Insert(account);
        }

        public void Delete(AccountTable account) {
            accountTable.Delete(account.Id);
        }

        public void Update(AccountTable account) {
            accountTable.Update(account);
        }

        public AccountTable GetAccount(int id) {
            return accountTable.FindOne(x => x.Id == id);
        }

        public AccountTable GetAccountByName(string name) {
            return accountTable.FindOne(x => x.Name == name);
        }

    }

}