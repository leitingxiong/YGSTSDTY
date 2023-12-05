using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC.Events {

    public class EventCore {

        LobbyEventCenter lobbyEventCenter;
        public LobbyEventCenter LobbyEventCenter => lobbyEventCenter;

        BattleEventCenter battleEventCenter;
        public BattleEventCenter BattleEventCenter => battleEventCenter;

        public EventCore() {
            lobbyEventCenter = new LobbyEventCenter();
            battleEventCenter = new BattleEventCenter();
        }

    }

}