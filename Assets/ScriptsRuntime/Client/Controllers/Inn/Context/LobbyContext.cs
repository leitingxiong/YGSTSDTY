using ScriptsRuntime.Client.Applications.UIApplication;

namespace ScriptsRuntime.Client.Controllers.Inn.Context {

    public class LobbyContext {

        UIApp uiApp;
        public UIApp UIApp => uiApp;

        public LobbyContext() { }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

    }

}