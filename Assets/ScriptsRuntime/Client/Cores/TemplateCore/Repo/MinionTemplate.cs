using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace DC.Template {

    public class MinionTemplate {

        Dictionary<int, MinionTM> all;

        public MinionTemplate() {
            this.all = new Dictionary<int, MinionTM>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.TM_MINION;
            var list = await Addressables.LoadAssetsAsync<MinionSo>(labelReference, null).Task;
            foreach (var so in list) {
                var tm = so.tm;
                all.Add(tm.templateID, tm);
            }
        }

        public bool TryGet(int templateID, out MinionTM tm) {
            return all.TryGetValue(templateID, out tm);
        }

    }

}