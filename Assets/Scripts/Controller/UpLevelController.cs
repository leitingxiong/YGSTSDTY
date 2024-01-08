using System;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class UpLevelController : MonoBehaviour, IController
    {
        private void Start()
        {
            GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<UpLevelCommand>(); });
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}