using QFramework;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;
namespace Controller
{
    public class KillController : MonoBehaviour, IController
    {
        void Start()
        {
            GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<KillCommand>(); });
        }
        
        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;

        }
    }
}