using ScriptsRuntime.Client.Applications.UIApplication.Enum;
using ScriptsRuntime.Client.Applications.UIApplication.Interface;
using UnityEngine;

namespace ScriptsRuntime.Client.Applications.UIApplication.Panel {

    public class UI_Battle : MonoBehaviour, IUIPanel {

        UIRootLevel IUIPanel.RootLevel => UIRootLevel.Bottom;
        int IUIPanel.OrderWeight => 1;
        bool IUIPanel.IsUnique => true;

        public void Ctor() {

        }

        void IUIPanel.TearDown() {

        }

    }

}