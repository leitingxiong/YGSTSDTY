using DC;
using DC.Assets;
using ScriptsRuntime.Client.Applications.UIApplication.Context;
using ScriptsRuntime.Client.Applications.UIApplication.Enum;
using ScriptsRuntime.Client.Applications.UIApplication.Interface;
using UnityEngine;

namespace ScriptsRuntime.Client.Applications.UIApplication {

    public class UIApp {

        UIContext uiContext;

        public UIApp() {
            this.uiContext = new UIContext();
        }

        public void Inject(Canvas canvas, AssetsCore assetsCore) {
            uiContext.Inject(canvas, assetsCore);
        }

        public void Init() {
            var canvas = uiContext.Canvas;
            var rootRepo = uiContext.RootRepo;
            rootRepo.Add(UIRootLevel.BG, CreateRoot(UIRootLevel.BG));
            rootRepo.Add(UIRootLevel.Bottom, CreateRoot(UIRootLevel.Bottom));
            rootRepo.Add(UIRootLevel.Medium, CreateRoot(UIRootLevel.Medium));
            rootRepo.Add(UIRootLevel.Top, CreateRoot(UIRootLevel.Top));
            rootRepo.Add(UIRootLevel.Tips, CreateRoot(UIRootLevel.Tips));
        }

        Transform CreateRoot(UIRootLevel rootLevel) {
            var canvas = uiContext.Canvas;
            var root = new GameObject("ui_root_" + rootLevel.ToString());
            root.transform.SetParent(canvas.transform);
            var rect = root.AddComponent<RectTransform>();
            rect.transform.localScale = Vector3.one;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            root.AddComponent<CanvasRenderer>();
            return root.transform;
        }

        public T Open<T>() where T : IUIPanel {

            string key = typeof(T).Name;
            bool has = uiContext.AssetsCore.UIPanelAssets.TryGet(key, out GameObject prefab);
            if (!has) {
                DCLog.Error("UIPanelAssets not found: " + key);
                return default(T);
            }

            var go = GameObject.Instantiate(prefab);
            var panel = go.GetComponent<T>();
            if (panel == null) {
                DCLog.Error("UIPanel not found: " + key);
                return default(T);
            }

            var rootLevel = panel.RootLevel;
            has = uiContext.RootRepo.TryGet(rootLevel, out Transform root);
            if (!has) {
                DCLog.Error("UIRoot not found: " + rootLevel.ToString());
                return default(T);
            }
            go.transform.SetParent(root, false);

            var panelRepo = uiContext.PanelRepo;
            panelRepo.Add(typeof(T), panel);

            return panel;

        }

        public void Close<T>() where T : IUIPanel {
            var panelRepo = uiContext.PanelRepo;
            bool has = panelRepo.TryGet(typeof(T), out IUIPanel panel);
            if (!has) {
                DCLog.Error("UIPanel not found: " + typeof(T).Name);
                return;
            }

            if (panel.IsUnique) {
                panelRepo.RemoveUnique(typeof(T));
            } else {
                DCLog.Warning("TODO: UIPanel is not unique: " + typeof(T).Name);
            }

            panel.TearDown();

        }

    }

}