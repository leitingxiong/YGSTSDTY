using System.Threading.Tasks;

namespace DC.Assets {

    public class AssetsCore {

        EntityAssets entityAssets;
        public EntityAssets EntityAssets => entityAssets;

        UIPanelAssets uiPanelAssets;
        public UIPanelAssets UIPanelAssets => uiPanelAssets;

        ModAssets modAssets;
        public ModAssets ModAssets => modAssets;

        public AssetsCore() {
            this.entityAssets = new EntityAssets();
            this.uiPanelAssets = new UIPanelAssets();
            this.modAssets = new ModAssets();
        }

        public async Task LoadAll() {
            await entityAssets.LoadAll();
            await uiPanelAssets.LoadAll();
            await modAssets.LoadAll();
        }

    }

}