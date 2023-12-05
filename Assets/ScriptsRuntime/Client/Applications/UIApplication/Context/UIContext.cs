using DC.Assets;
using ScriptsRuntime.Client.Applications.UIApplication.Repo;
using UnityEngine;

namespace ScriptsRuntime.Client.Applications.UIApplication.Context {

    public class UIContext {

        Canvas canvas;
        public Canvas Canvas => canvas;

        AssetsCore assetsCore;
        public AssetsCore AssetsCore => assetsCore;

        UIRootRepo rootRepo;
        public UIRootRepo RootRepo => rootRepo;

        UIPanelRepo panelRepo;
        public UIPanelRepo PanelRepo => panelRepo;

        public UIContext() {
            this.rootRepo = new UIRootRepo();
            this.panelRepo = new UIPanelRepo();
        }

        public void Inject(Canvas canvas, AssetsCore assetsCore) {
            this.canvas = canvas;
            this.assetsCore = assetsCore;
        }

    }

}