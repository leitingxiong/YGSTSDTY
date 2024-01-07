using DC;
using DC.Database;
using DC.Infrastructure.Context;
using ScriptsRuntime.Client.Applications.UIApplication.Panel;
using ScriptsRuntime.Client.Controllers.Inn.Context;

namespace ScriptsRuntime.Client.Controllers.Inn.Domain {

    public class LobbyDomain {

        InfraContext infraContext;
        LobbyContext lobbyContext;

        public LobbyDomain() { }

        public void Inject(InfraContext infraContext, LobbyContext lobbyContext) {
            this.infraContext = infraContext;
            this.lobbyContext = lobbyContext;
        }

        public void OpenLobby(string accountName) {

            // - DB
            var db = infraContext.DBCore;
            db.LoadLocalDataByDatabaseName(accountName);

            var account = db.AccountDomain.GetByAccountName(accountName);
            if (account == null) {
                account = new AccountTable(accountName);
                _ = db.AccountDomain.Insert(account);
            }

            var chest = db.ChestDomain.GetOneByAccountID(account.Id);
            if (chest == null) {
                chest = new ChestTable(account.Id);
                _ = db.ChestDomain.Insert(chest);
            }

            var minion = db.MinionDomain.GetOneByAccountID(account.Id);
            if (minion == null) {
                minion = new MinionTable(account.Id);
                _ = db.MinionDomain.Insert(minion);
            }

            // - UI
            var uiApp = lobbyContext.UIApp;
            var uiLobby = lobbyContext.UIApp.Open<UI_Lobby>();
            uiLobby.Ctor();

            uiLobby.OnClickStartBattleHandle += OnStartBattle;

        }

        void OnStartBattle() {
            DCLog.Log("OnStartBattle");
            var uiApp = lobbyContext.UIApp;
            uiApp.Close<UI_Lobby>();

            var battleEV = infraContext.EventCore.BattleEventCenter;
            battleEV.SetEnterBattleEvent((true, 1, 1));

        }

    }
}