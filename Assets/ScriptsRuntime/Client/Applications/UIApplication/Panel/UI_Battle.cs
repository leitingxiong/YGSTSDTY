using UnityEngine;

namespace DC.UIApplication {

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