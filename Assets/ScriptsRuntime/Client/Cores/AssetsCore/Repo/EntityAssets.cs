using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DC.Assets {

    public class EntityAssets {

        Dictionary<string, GameObject> all;

        public EntityAssets() {
            this.all = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.ENTITY;
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            foreach (var go in list) {
                all.Add(go.name, go);
            }
        }

        public bool TryGetMinion(out GameObject prefab) {
            return all.TryGetValue("entity_minion", out prefab);
        }

    }

}