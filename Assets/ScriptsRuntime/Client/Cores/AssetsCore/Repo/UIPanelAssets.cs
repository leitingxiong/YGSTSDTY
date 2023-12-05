using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DC.Assets {

    public class UIPanelAssets {

        Dictionary<string, GameObject> all;

        public UIPanelAssets() {
            this.all = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.UI_PANEL;
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            foreach (var go in list) {
                all.Add(go.name, go);
            }
        }

        public bool TryGet(string name, out GameObject prefab) {
            return all.TryGetValue(name, out prefab);
        }

    }

}