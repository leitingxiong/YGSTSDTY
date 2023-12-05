using UnityEngine;

namespace DC {

    public static class DCLog {

        public static void Log(object message) {
            Debug.Log(message);
        }

        public static void Warning(object message) {
            Debug.LogWarning(message);
        }

        public static void Error(object message) {
            Debug.LogError(message);
        }

    }
}