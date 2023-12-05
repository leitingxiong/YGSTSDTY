using System.Collections.Generic;
using ScriptsRuntime.Client.Applications.UIApplication.Enum;
using UnityEngine;

namespace ScriptsRuntime.Client.Applications.UIApplication.Repo {

    public class UIRootRepo {

        Dictionary<UIRootLevel, Transform> all;

        public UIRootRepo() {
            this.all = new Dictionary<UIRootLevel, Transform>();
        }

        public void Add(UIRootLevel rootLevel, Transform tf) {
            all.Add(rootLevel, tf);
        }

        public bool TryGet(UIRootLevel rootLevel, out Transform tf) {
            return all.TryGetValue(rootLevel, out tf);
        }

    }

}