using DC.LobbyBusiness.Domain;

namespace DC.LobbyBusiness.Context {

    public class AllInnDomain {

        LobbyDomain lobbyDomain;
        public LobbyDomain LobbyDomain => lobbyDomain;

        public AllInnDomain() {
            this.lobbyDomain = new LobbyDomain();
        }

    }
}