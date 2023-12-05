namespace DC.Events {

    public class LobbyEventCenter {

        bool isEnterLobby;
        public bool IsEnterLobby => isEnterLobby;
        public void SetIsEnterLobby(bool value) => isEnterLobby = value;

        public LobbyEventCenter() { }

    }

}