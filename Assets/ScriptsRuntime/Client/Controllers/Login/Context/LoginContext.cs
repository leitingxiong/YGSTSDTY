using ScriptsRuntime.Client.Applications.UIApplication;

namespace ScriptsRuntime.Client.Controllers.Login.Context {

    public class LoginContext {

        UIApp uiApp;
        public UIApp UIApp => uiApp;

        public LoginContext() { }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

    }
}