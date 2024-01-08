using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class NewRoomController : MonoBehaviour, IController
    {
       void Start()
        {
            GetComponent<Button>()
                .onClick.AddListener(this.SendCommand<NewRoomCommand>);
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}
