using System.IO;
using UnityEngine;
using LiteDB;
using DC.Database.Context;
using DC.Database.Domain;

namespace DC.Database {

    public class DatabaseCore {

        LiteDatabase conn;

        AccountDBDomain accountDomain;
        public AccountDBDomain AccountDomain => accountDomain;

        ChestDBDomain chestDomain;
        public ChestDBDomain ChestDomain => chestDomain;

        MinionDBDomain minionDomain;
        public MinionDBDomain MinionDomain => minionDomain;

        DatabaseContext dbContext;

        public DatabaseCore() {

            dbContext = new DatabaseContext();

            accountDomain = new AccountDBDomain();
            chestDomain = new ChestDBDomain();
            minionDomain = new MinionDBDomain();

            accountDomain.Inject(dbContext);
            chestDomain.Inject(dbContext);
            minionDomain.Inject(dbContext);

        }

        public void LoadLocalDataByDatabaseName(string dbName) {
            var path = Path.Combine(Application.persistentDataPath, dbName + ".db");
            LoadLocalData(path);
        }

        public void LoadLocalData(string path) {
            if (conn == null) {
                conn = new LiteDatabase(path);
                dbContext.Inject(conn);
                dbContext.Init();
            }
        }

        public void TearDown() {
            conn?.Dispose();
        }

    }

}