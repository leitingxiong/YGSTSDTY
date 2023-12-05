using DC.Infrastructure.Context;
using DC.LoginBusiness.Context;
using DC.UIApplication;

namespace DC.LoginBusiness.Domain {

    public class LoginDomain {

        InfraContext infraContext;
        LoginContext loginContext;

        public LoginDomain() { }

        public void Inject(InfraContext infraContext, LoginContext loginContext) {
            this.infraContext = infraContext;
            this.loginContext = loginContext;
        }

        public void UILoginOpen() {
            var uiApp = loginContext.UIApp;
            var uiLogin = uiApp.Open<UI_Login>();
            uiLogin.Ctor();

            uiLogin.OnStartGameHandle += OnStartGame;
        }

        void OnStartGame() {
            DCLog.Log("OnStartGame");
            var uiApp = loginContext.UIApp;
            uiApp.Close<UI_Login>();

            EnterLobby();
        }

        void EnterLobby() {
            infraContext.EventCore.LobbyEventCenter.SetIsEnterLobby(true);
        }

    }

}