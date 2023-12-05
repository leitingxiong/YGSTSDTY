using DC.UIApplication;

namespace DC.LobbyBusiness.Context {

    public class LobbyContext {

        UIApp uiApp;
        public UIApp UIApp => uiApp;

        public LobbyContext() { }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

    }

}