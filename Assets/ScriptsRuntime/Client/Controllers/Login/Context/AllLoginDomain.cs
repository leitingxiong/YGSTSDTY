using DC.LoginBusiness.Domain;

namespace DC.LoginBusiness.Context {

    public class AllLoginDomain {

        LoginDomain loginDomain;
        public LoginDomain LoginDomain => loginDomain;

        public AllLoginDomain() {
            this.loginDomain = new LoginDomain();
        }

    }

}