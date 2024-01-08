using UnityEngine;

namespace Grid
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Start is called before the first frame update
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
        }
    }
}
