using UnityEngine;

namespace Grid
{
    public class CustomCursor : MonoBehaviour
    {
        private void Awake()
        {

            transform.position = Input.mousePosition;
        }
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
