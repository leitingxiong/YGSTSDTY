using DC.UIApplication;

namespace DC.LoginBusiness.Context {

    public class LoginContext {

        UIApp uiApp;
        public UIApp UIApp => uiApp;

        public LoginContext() { }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

    }
}