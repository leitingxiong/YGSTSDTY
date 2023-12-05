using LiteDB;
using DC.Database.Repository;

namespace DC.Database.Context {

    public class DatabaseContext {

        // ==== DAO ====
        // 持久化
        AccountDAO accountDAO;
        public AccountDAO AccountDAO => accountDAO;

        ChestDAO chestDAO;
        public ChestDAO ChestDAO => chestDAO;

        MinionDAO minionDAO;
        public MinionDAO MinionDAO => minionDAO;

        public DatabaseContext() {
            accountDAO = new AccountDAO();
            chestDAO = new ChestDAO();
            minionDAO = new MinionDAO();
        }

        public void Inject(LiteDatabase conn) {
            accountDAO.Inject(conn);
            chestDAO.Inject(conn);
            minionDAO.Inject(conn);
        }

        public void Init() {
            accountDAO.Init();
            chestDAO.Init();
            minionDAO.Init();
        }

    }

}