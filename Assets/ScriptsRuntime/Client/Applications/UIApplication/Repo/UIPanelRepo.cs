using System;
using System.Collections.Generic;

namespace DC.UIApplication {

    public class UIPanelRepo {

        Dictionary<Type, IUIPanel> uniqueDict;

        public UIPanelRepo() {
            this.uniqueDict = new Dictionary<Type, IUIPanel>();
        }

        public void Add(Type type, IUIPanel panel) {
            if (panel.IsUnique) {
                uniqueDict.Add(type, panel);
            } else {
                DCLog.Warning("TODO: Panel is not unique");
            }
        }

        public bool TryGet(Type type, out IUIPanel panel) {
            return uniqueDict.TryGetValue(type, out panel);
        }

        public void RemoveUnique(Type type) {
            uniqueDict.Remove(type);
        }

    }
}