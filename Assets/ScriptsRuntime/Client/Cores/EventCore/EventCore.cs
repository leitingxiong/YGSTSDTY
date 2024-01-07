using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC.Events {

    public class EventCore {
        public LobbyEventCenter LobbyEventCenter { get; }

        public BattleEventCenter BattleEventCenter { get; }

        public EventCore() {
            LobbyEventCenter = new LobbyEventCenter();
            BattleEventCenter = new BattleEventCenter();
        }

    }

}