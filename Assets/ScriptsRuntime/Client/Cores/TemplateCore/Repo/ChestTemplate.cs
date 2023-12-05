using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace DC.Template {

    public class ChestTemplate {

        Dictionary<int, ChestTM> all;

        public ChestTemplate() {
            this.all = new Dictionary<int, ChestTM>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.TM_CHEST;
            var list = await Addressables.LoadAssetsAsync<ChestSo>(labelReference, null).Task;
            foreach (var so in list) {
                var tm = so.tm;
                all.Add(tm.templateID, tm);
            }
        }

        public bool TryGet(int templateID, out ChestTM tm) {
            return all.TryGetValue(templateID, out tm);
        }

    }
}