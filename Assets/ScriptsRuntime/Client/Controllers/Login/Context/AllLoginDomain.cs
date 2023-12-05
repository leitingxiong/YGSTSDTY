using ScriptsRuntime.Client.Controllers.Login.Domain;

namespace ScriptsRuntime.Client.Controllers.Login.Context {

    public class AllLoginDomain {

        LoginDomain loginDomain;
        public LoginDomain LoginDomain => loginDomain;

        public AllLoginDomain() {
            this.loginDomain = new LoginDomain();
        }

    }

}