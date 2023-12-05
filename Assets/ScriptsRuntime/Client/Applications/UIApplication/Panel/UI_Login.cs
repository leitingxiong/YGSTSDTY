using System;
using ScriptsRuntime.Client.Applications.UIApplication.Enum;
using ScriptsRuntime.Client.Applications.UIApplication.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsRuntime.Client.Applications.UIApplication.Panel {

    public class UI_Login : MonoBehaviour, IUIPanel {

        UIRootLevel IUIPanel.RootLevel => UIRootLevel.Top;
        int IUIPanel.OrderWeight => 0;
        bool IUIPanel.IsUnique => true;

        Button startGameBtn;

        public event Action OnStartGameHandle;

        public void Ctor() {
            var bd = transform.GetChild(0);
            startGameBtn = bd.GetChild(0).GetComponent<Button>();

            startGameBtn.onClick.AddListener(() => {
                OnStartGameHandle.Invoke();
            });
        }

        void IUIPanel.TearDown() {
            startGameBtn.onClick.RemoveAllListeners();
            OnStartGameHandle = null;
            GameObject.Destroy(gameObject);
        }

    }

}