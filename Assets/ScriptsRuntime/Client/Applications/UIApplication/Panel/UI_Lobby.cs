using System;
using UnityEngine;
using UnityEngine.UI;

namespace DC.UIApplication {

    public class UI_Lobby : MonoBehaviour, IUIPanel {

        UIRootLevel IUIPanel.RootLevel => UIRootLevel.Medium;
        int IUIPanel.OrderWeight => 1;
        bool IUIPanel.IsUnique => true;

        Button startBattleBtn;

        public event Action OnClickStartBattleHandle;

        public void Ctor() {

            var bd = transform.GetChild(0);
            startBattleBtn = bd.GetChild(0).GetComponent<Button>();

            startBattleBtn.onClick.AddListener(() => {
                OnClickStartBattleHandle.Invoke();
            });

        }

        void IUIPanel.TearDown() {
            OnClickStartBattleHandle = null;
            startBattleBtn.onClick.RemoveAllListeners();
            GameObject.Destroy(gameObject);
        }
    }

}