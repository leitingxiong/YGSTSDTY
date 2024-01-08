using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class GetBuffController : MonoBehaviour, IController
    {
        void Start()
        {
            GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<GetBuffCommand>(); });
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}
