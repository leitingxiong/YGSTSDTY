using ScriptsRuntime.Client.Controllers.Inn.Domain;

namespace ScriptsRuntime.Client.Controllers.Inn.Context {

    public class AllInnDomain {

        LobbyDomain lobbyDomain;
        public LobbyDomain LobbyDomain => lobbyDomain;

        public AllInnDomain() {
            this.lobbyDomain = new LobbyDomain();
        }

    }
}