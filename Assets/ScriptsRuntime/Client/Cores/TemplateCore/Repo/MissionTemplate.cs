using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace DC.Template {

    public class MissionTemplate {

        Dictionary<ulong, MissionTM> all;

        public MissionTemplate() {
            this.all = new Dictionary<ulong, MissionTM>();
        }

        public async Task LoadAll() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = AssetLabelCollection.TM_MISSION;
            var list = await Addressables.LoadAssetsAsync<MissionSo>(labelReference, null).Task;
            foreach (var so in list) {
                var tm = so.tm;
                ulong key = CombineKey(tm.chapter, tm.level);
                all.Add(key, tm);
            }
        }

        public bool TryGet(int chapter, int level, out MissionTM tm) {
            ulong key = CombineKey(chapter, level);
            return all.TryGetValue(key, out tm);
        }

        ulong CombineKey(int chapter, int level) {
            ulong value = (uint)level;
            value |= (ulong)(((uint)chapter) << 32);
            return value;
        }
    }
}