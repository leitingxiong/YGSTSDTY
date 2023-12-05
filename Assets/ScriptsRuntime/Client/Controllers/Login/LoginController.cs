using DC.Infrastructure.Context;
using ScriptsRuntime.Client.Applications.UIApplication;
using ScriptsRuntime.Client.Controllers.Login.Context;

namespace ScriptsRuntime.Client.Controllers.Login {

    // 登录
    public class LoginController {

        InfraContext infraContext;

        LoginContext loginContext;
        AllLoginDomain allLoginDomain;

        public LoginController() {
            this.loginContext = new LoginContext();
            this.allLoginDomain = new AllLoginDomain();
        }

        public void Inject(InfraContext infraContext, UIApp uiApp) {
            this.infraContext = infraContext;
            loginContext.Inject(uiApp);

            allLoginDomain.LoginDomain.Inject(infraContext, loginContext);

        }

        public void Init() {

        }

        public void Enter() {
            var loginDomain = allLoginDomain.LoginDomain;
            loginDomain.UILoginOpen();
        }

        public void Tick() {

        }

        public void Exit() {
            
        }

    }

}