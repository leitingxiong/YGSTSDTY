using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CleanController : MonoBehaviour, IController
    {
        void Start()
        {
            GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<CleanCommand>(); });
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}
