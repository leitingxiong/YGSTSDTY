using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 参考图片添加工具
/// </summary>
//使用InitializeOnLoad特性，在编辑器启动的时候调用此类的构造函数
[InitializeOnLoad]
public class AddReferenceImageTool
{
    #region Normal Properties
    /// <summary>
    /// 默认查找的参考图放置节点名称
    /// </summary>
    private const string DefaultUIRefName = "参考图RawImage";
    /// <summary>
    /// 没有默认放置节点，新创建的节点名称
    /// </summary>
    private const string NewUIRefName = "New-参考图RawImage";

    private const string UseGUIReinforceMenuItemPath = "游戏工具/GUI增强/开启Scene视图右键菜单";
    private const string AddRefImgInProjectPath = "Assets/设置为参考图";
    private const string AddRefImgMenuItemPath = "GameObject/参考图";

    /// <summary>
    /// 是否是鼠标右键点击
    /// </summary>
    private static bool enableRightClick = false;

    //选择参考图的文件过滤
    private static string[] refImageFilter = new string[]
    {
        "图片文件", "png,jpg,jpeg,gif"
    };

    //允许的参考图后缀
    private static List<string> enableRefPictureSuffix = new List<string>()
    {
        "png", "jpg", "jpeg", "gif"
    };
    #endregion

    #region EditorPrefs Properties
    //是否开启Scene右键菜单的key
    private const string SceneGenericMenuKey = "EnableSceneGenericMenu";
    private static int enableSceneGenericMenu = -1;
    /// <summary>
    /// 使用属性对变量enableSceneGenericMenu进行封装
    /// </summary>
    internal static bool EnableSceneGenericMenu
    {
        get
        {
            if (enableSceneGenericMenu == -1)
            {
                //根据关键字获取Unity editor偏好设置文件中的值，偏好设置文件中不存在此关键字就返回默认值
                enableSceneGenericMenu = EditorPrefs.GetInt(SceneGenericMenuKey, 1);
            }
            return enableSceneGenericMenu == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            //设置的新值和旧值不相等时，我们才执行赋值操作
            if (newValue != enableSceneGenericMenu)
            {
                enableSceneGenericMenu = newValue;
                EditorPrefs.SetInt(SceneGenericMenuKey, enableSceneGenericMenu);
            }
        }
    }

    //上一次选择的参考图路径，默认为空
    private static string lastChoosePath = null;
    private const string LastChoosePathKey = "RefImgLastChoosePath";
    /// <summary>
    /// 使用属性对变量lastChoosePath进行封装
    /// </summary>
    private static string LastChoosePath
    {
        get
        {
            //上次的选择路径为空是，我们就根据关键字去获取一下Unity editor偏好设置文件中的值
            if (lastChoosePath == null)
                lastChoosePath = EditorPrefs.GetString(LastChoosePathKey, Application.dataPath);
            return lastChoosePath;
        }
        set
        {
            //设置的新值和旧值不相等时，我们才执行赋值操作
            if (!lastChoosePath.Equals(value))
            {
                lastChoosePath = value;
                EditorPrefs.SetString(LastChoosePathKey, lastChoosePath);
            }
        }
    }
    #endregion

    //无参构造函数
    static AddReferenceImageTool()
    {
        //订阅duringSceneGui事件，可以在 Scene 视图每次调用 OnGUI 方法时执行对应的事件处理器
        SceneView.duringSceneGui += OnSceneViewGUI;
    }

    //析构函数，析构函数的名称与类名相同，不过需要在名称的前面加上一个波浪号~作为前缀
    ~AddReferenceImageTool()
    {
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }

    /// <summary>
    /// 检测是否在预制体里或者处于游戏运行状态
    /// </summary>
    /// <returns></returns>
    private static bool CheckIsPlayingAndIsInPrefab()
    {
        //return !(UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null && !Application.isPlaying);
        return !(UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null && !Application.isPlaying);
    }

    #region 添加与移除参考图的逻辑代码
    private static void OnSceneViewGUI(SceneView sceneView)
    {
        Event curEvent = Event.current;
        //根据将被处理的当前事件类型，去执行不同的行为
        switch (curEvent.type)
        {
            case EventType.MouseDown:
                //点击鼠标右键
                enableRightClick = (curEvent.button == 1);
                break;
            case EventType.MouseUp:
                if (EnableSceneGenericMenu && enableRightClick)
                {
                    if (!CheckIsPlayingAndIsInPrefab())
                    {
                        //使用GenericMenu 我们可以创建自定义上下文菜单和下拉菜单，这里用于Scene视图右键菜单扩展
                        GenericMenu menu = new GenericMenu();
                        //添加一个菜单项，第一个参数为显示的文本，第三个参数为要调用的函数
                        menu.AddItem(new GUIContent("添加参考图"), false, AddRefImage);
                        menu.AddItem(new GUIContent("撤销参考图"), false, RemoveRefImage);
                        //右键单击时在鼠标下显示菜单。
                        menu.ShowAsContext();
                        curEvent.Use();
                    }
                    else
                    {
                        Debug.LogWarning("Prefab视图无法打开右键菜单");
                    }
                }
                break;
            case EventType.MouseDrag:
                //拖动鼠标后不显示菜单
                enableRightClick = false;
                break;
        }
    }

    #region 添加图片
    /// <summary>
    /// 从磁盘中读取图片
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="width">纹理的宽度</param>
    /// <param name="height">纹理的高度</param>
    /// <returns></returns>
    private static Texture2D LoadImage(string path, int width, int height)
    {
        if (path == null || path.Length == 0)
        {
            Debug.LogError("待加载的图片路径为空!");
            return null;
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //将文件的指针移动到开头。
        fs.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fs.Length];
        //读取文件
        fs.Read(bytes, 0, (int)fs.Length);
        fs.Close();
        //使用Dispose销毁对象并释放资源
        fs.Dispose();

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.LoadImage(bytes);
        return tex;
    }

    /// <summary>
    /// 加载并添加参考图
    /// </summary>
    /// <param name="path">路径</param>
    private static void LoadAndAddRefImage(string path)
    {
        Texture2D tex = LoadImage(path, 1280, 720);

        if (tex != null)
        {
            //先找NewUIRefName，为空则找UIRefDefaultName
            GameObject root = GameObject.Find(NewUIRefName);
            if (null == root)
            {
                if ((root = GameObject.Find(DefaultUIRefName)) == null)
                {
                    //在场景中查找Canvas
                    Canvas canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
                    if (canvas == null)
                    {
                        Debug.LogError("场景中不存在Canvas组件!");
                        return;
                    }
                    //新建一个GameObject，后面的参数为在创建时要添加到 GameObject 的 Components 列表。
                    root = new GameObject(NewUIRefName, typeof(RectTransform), typeof(RawImage));
                    root.transform.SetParent(canvas.gameObject.transform);
                    //前面已经明确了，组件里有RectTransform，因此就不需要使用TryGetComponent了
                    RectTransform rect = root.GetComponent<RectTransform>();
                    //设置描点位置
                    rect.anchorMin = new Vector2(0.5f, 0.5f);
                    rect.anchorMax = new Vector2(0.5f, 0.5f);
                    rect.anchoredPosition = new Vector2(0.5f, 0.5f);
                    //设置 RectTransform 相对于锚点之间距离的大小
                    rect.sizeDelta = new Vector2(1920, 1080);
                    rect.transform.localPosition = Vector3.zero;
                    rect.transform.localScale = Vector3.one;
                }
            }
            RawImage rawImage;
            root.TryGetComponent<RawImage>(out rawImage);
            if (rawImage == null)
            {
                Debug.Log(string.Format("未找到RawImage或AorRawImage组件: {0}，将添加RawImage组件!", root.name));
                rawImage = root.AddComponent<RawImage>();
            }
            //设置纹理的各向异性过滤级别，值范围为 1 到 9，其中 1 等于不应用过滤，9 等于应用完全过滤。
            tex.anisoLevel = 8;
            //设置纹理的过滤模式
            tex.filterMode = FilterMode.Point;
            Undo.RecordObject(rawImage, "新增参考图像名称：" + rawImage.name);
            rawImage.texture = tex;
            rawImage.color = new Color(1, 1, 1, 1);
            rawImage.enabled = true;
            //将rawImage的大小设置为纹理的大小
            rawImage.SetNativeSize();
            root.SetActive(true);
        }
        else
        {
            Debug.LogError("图片读取失败: " + path);
        }
    }
    #endregion

    #region 移除参考图
    /// <summary>
    /// 从节点上移除参考图
    /// </summary>
    /// <param name="root">要执行移除行为的节点</param>
    private static void RemoveTexture(GameObject root)
    {
        if (root == null)
            return;
        RawImage rawImage;
        //尝试获取对象里的RawImage组件
        root.TryGetComponent<RawImage>(out rawImage);
        if (rawImage == null)
        {
            Debug.Log(string.Format("未找到RawImage组件: {0}，参考图移除失败!", root.name));
            return;
        }
        Undo.RecordObject(rawImage, "取消设置rawImage" + rawImage.name);
        Texture tex = rawImage.texture;
        rawImage.texture = null;
        rawImage.color = new Color(1, 1, 1, 0);
        rawImage.enabled = false;
    }
    #endregion
    #endregion

    #region Editor Menu Item

    /// <summary>
    /// 检查参考图的文件后缀
    /// </summary>
    /// <param name="guids">GUID数组</param>
    /// <returns></returns>
    private static bool CheckRefImageSuffix(string[] guids)
    {
        //每次只允许选择一张图
        if (guids.Length != 1)
        {
            Debug.LogWarning("每次只允许选择一张图");
            return false;
        }
        string[] splitedPath = AssetDatabase.GUIDToAssetPath(guids[0]).Split('.');
        return enableRefPictureSuffix.Contains(splitedPath[splitedPath.Length - 1]);
    }

    #region 设置是否启用Scene视图右键菜单
    [MenuItem(UseGUIReinforceMenuItemPath, false)]
    public static void UseSceneGenericMenu()
    {
        EnableSceneGenericMenu = !EnableSceneGenericMenu;
    }

    [MenuItem(UseGUIReinforceMenuItemPath, true)]
    public static bool SetUseSceneGenericMenu()
    {
        //设置菜单的勾选状态
        Menu.SetChecked(UseGUIReinforceMenuItemPath, EnableSceneGenericMenu);
        return true;
    }
    #endregion

    #region 添加参考图
    [MenuItem(AddRefImgMenuItemPath + "/添加", priority = 25, validate = false)]
    private static void AddRefImage()
    {
        //使用OpenFilePanelWithFilters“打开文件”对话框并返回所选的路径名称。
        string path = EditorUtility.OpenFilePanelWithFilters("选择参考图", LastChoosePath, refImageFilter);
        if (path == null || path.Length == 0)
        {
            return;
        }
        LastChoosePath = path;
        LoadAndAddRefImage(path);
    }

    //validate设为ture则点击菜单前就会进行调用，主要用于检测按钮是否要显示
    [MenuItem(AddRefImgMenuItemPath + "/添加", priority = 25, validate = true)]
    private static bool SetAddRefImageItemState()
    {
        return !CheckIsPlayingAndIsInPrefab();
    }
    #endregion

    #region 在project视图里选择图像作为参考图
    [MenuItem(AddRefImgInProjectPath, priority = 25, validate = false)]
    private static void AddRefImageInProject()
    {
        string[] guids = Selection.assetGUIDs;
        if (CheckRefImageSuffix(guids))
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            LoadAndAddRefImage(path);
        }
    }

    [MenuItem(AddRefImgInProjectPath, priority = 25, validate = true)]
    private static bool SetAddRefImageInProjectItemState()
    {
        string[] guids = Selection.assetGUIDs;
        return !CheckIsPlayingAndIsInPrefab() && CheckRefImageSuffix(guids);
    }
    #endregion

    #region 移除参考图
    [MenuItem(AddRefImgMenuItemPath + "/移除", priority = 25, validate = false)]
    private static void RemoveRefImage()
    {
        //在场景中查找节点
        GameObject root = GameObject.Find(DefaultUIRefName);
        GameObject newRoot = GameObject.Find(NewUIRefName);
        if (root == null && newRoot == null)
        {
            Debug.Log(string.Format("未找到默认的UI参考图根节点: {0}或{1}!", DefaultUIRefName, NewUIRefName));
            return;
        }
        RemoveTexture(root);
        RemoveTexture(newRoot);
    }

    [MenuItem(AddRefImgMenuItemPath + "/移除", priority = 25, validate = true)]
    private static bool SetRemoveRefImageItemState()
    {
        return !CheckIsPlayingAndIsInPrefab();
    }
    #endregion
    #endregion
}
