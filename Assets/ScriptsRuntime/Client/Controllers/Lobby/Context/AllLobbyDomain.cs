using DC.LobbyBusiness.Domain;

namespace DC.LobbyBusiness.Context {

    public class AllLobbyDomain {

        LobbyDomain lobbyDomain;
        public LobbyDomain LobbyDomain => lobbyDomain;

        public AllLobbyDomain() {
            this.lobbyDomain = new LobbyDomain();
        }

    }
}