using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameUtil
{
    public static class ExtralEditorUtility
    {
        /// <summary>
        /// UI对象的Layer层级名称
        /// </summary>
        private const string UI_LAYER_NAME = "UI";

        /// <summary>
        /// 设置UI元素的父节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="menuCommand">执行该菜单命令</param>
        public static void SetUIElementRoot(GameObject element, MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            //父节点为空或者父节点（包括爷爷辈）无Canvas
            if (parent == null || parent.GetComponentInParent<Canvas>() == null)
                parent = GetOrCreateCanvasGameObject();
            //确保新的子游戏对象与层级视图中的同级相比具有唯一的名称
            string uniqueName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
            element.name = uniqueName;

            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);

            GameObjectUtility.SetParentAndAlign(element, parent);
            //判断父节点是否是执行该菜单命令的对象，不是就将其在 SceneView 中居中
            if (parent != menuCommand.context)
                SetPositionInSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());
            Selection.activeGameObject = element;
        }

        /// <summary>
        /// 设置在场景视图中的位置
        /// </summary>
        /// <param name="canvasRTransform"></param>
        /// <param name="itemTransform"></param>
        public static void SetPositionInSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
        {
            //获得最近激活的场景
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView == null && SceneView.sceneViews.Count > 0)
                sceneView = SceneView.sceneViews[0] as SceneView;

            // 找不到场景视图就不设置位置
            if (sceneView == null || sceneView.camera == null)
                return;

            // 在画布位置创建位于世界空间的Plane
            Vector2 localPlanePosition;
            Camera camera = sceneView.camera;
            Vector3 position = Vector3.zero;
            //将一个屏幕空间点转换为 RectTransform 的本地空间，Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2)是计算当前分辨率下的屏幕中心点
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
            {
                //调整画布的pivot
                localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

                localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
                localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

                //调整画布的anchor
                position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

                Vector3 minLocalPosition;
                minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

                Vector3 maxLocalPosition;
                maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

                position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
                position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
            }

            itemTransform.anchoredPosition = position;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }

        /// <summary>
        /// 获取或者创建Canvas（有Canvas则是获取，无就是创建）
        /// </summary>
        /// <param name="globalFind">当前组件及其父物体上没有Canvas时，是否进行全局查找</param>
        /// <returns></returns>
        public static GameObject GetOrCreateCanvasGameObject(bool globalFind = true)
        {
            GameObject selectedGo = Selection.activeGameObject;
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (canvas != null && canvas.gameObject.activeInHierarchy)
                return canvas.gameObject;
            //全局查找一次
            if (globalFind) 
            {
                canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
                if (canvas != null && canvas.gameObject.activeInHierarchy)
                    return canvas.gameObject;
            }
            return CreateNewUI();
        }

        /// <summary>
        /// 创建新的UI画布
        /// </summary>
        /// <returns></returns>
        public static GameObject CreateNewUI()
        {
            GameObject canvas = new GameObject();
            //确保新的子 GameObject 与其在层次结构中的兄弟姐妹相比具有唯一的名称。
            canvas.name = GameObjectUtility.GetUniqueNameForSibling(null, "Canvas");
            canvas.layer = LayerMask.NameToLayer(UI_LAYER_NAME);
            canvas.AddComponent<Canvas>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            //创建新对象时需要将其注册到撤销堆栈中
            Undo.RegisterCreatedObjectUndo(canvas, "Create" + canvas.name);
            CreateEventSystem();
            return canvas;
        }

        /// <summary>
        /// 创建一个EventSystem
        /// </summary>
        /// <param name="select">是否选中对象</param>
        /// <param name="parent">父节点</param>
        public static void CreateEventSystem(bool select = false, GameObject parent = null)
        {
            //全局查找是否有EventSystem存在
            EventSystem globalEventSystem = Object.FindObjectOfType<EventSystem>();
            if (globalEventSystem == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                //将子对象设置到父节点下，并确保子对象的Layer个Position是一致的
                GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                globalEventSystem = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();

                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && globalEventSystem != null)
            {
                //设置选中对象为当前创建的对象
                Selection.activeGameObject = globalEventSystem.gameObject;
            }
        }

        /// <summary>
        /// 自定义Label
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="color">GUI颜色</param>
        /// <param name="space">间距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void DisplayDIYGUILable(string content, Color color, float space = 0, float width = 150.0f, float height = 20.0f)
        {
            Color originalcolor = GUI.color;
            GUI.color = color;
            GUILayout.Space(space);
            GUILayout.Label(content, "Box", GUILayout.Width(width), GUILayout.Height(height));
            //将GUI的颜色还原
            GUI.color = originalcolor;
        }

        #region 获取对象的Hierarchy路径
        /// <summary>
        /// 获取对象的Hierarchy路径
        /// </summary>
        /// <param name="target">GameObject</param>
        public static void GetGameObjectHierarchyPath(GameObject target)
        {
            Transform targetTransform = target.transform;
            GetGameObjectHierarchyPath(targetTransform);
        }

        /// <summary>
        /// 获取对象的Hierarchy路径，针对Transform组件
        /// </summary>
        /// <param name="target">Transform</param>
        //public static void GetGameObjectHierarchyPath(Transform target)
        //{
        //    /*因为String 对象不可变。每次使用String中的方法，都要在内存中新建字符串对象，这就需要为新对象分配新的内存空间。
        //    使用StringBuilder可提升性能，简单的连击两个字符串时使用string直接连接更好*/
        //    StringBuilder resultPath = new StringBuilder(target.name);
        //    //一直遍历到没有父节点为止
        //    while (target.parent != null)
        //    {
        //        target = target.parent;
        //        //将字符串插入到当前StringBuilder的头部。
        //        resultPath.Insert(0, target.name + "/");
        //    }
        //    GUIUtility.systemCopyBuffer = resultPath.ToString();
        //}

        /// <summary>
        /// 获取对象的Hierarchy路径，针对Transform组件
        /// </summary>
        /// <param name="target">Transform</param>
        public static void GetGameObjectHierarchyPath(Transform target)
        {
            string resultPath = target.name;
            //一直遍历到没有父节点为止
            while (target.parent != null)
            {
                target = target.parent;
                resultPath = target.name + "/" + resultPath;
            }
            GUIUtility.systemCopyBuffer = resultPath;
        }
        #endregion
    }
}