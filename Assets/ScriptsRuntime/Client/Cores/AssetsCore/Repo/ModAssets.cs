using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DC.Assets {

    public class ModAssets {

        Dictionary<string, GameObject> all;

        public ModAssets() {
            this.all = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.MOD_MINION;
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            foreach (var go in list) {
                all.Add(go.name, go);
            }
        }

        public bool TryGetMinionMod(string name, out GameObject go) {
            return all.TryGetValue(name, out go);
        }

    }

}