using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
//用于调试应用程序并跟踪代码的执行
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEditor;
using GameUtil;
using ExtendsFunction;
using UnityEngine.SceneManagement;

//因为前面引用了Diagnostics，这里必须指明使用的是哪一个的Debug
using Debug = UnityEngine.Debug;

//使用InitializeOnLoad特性，在编辑器启动的时候调用此类的构造函数
[InitializeOnLoad]
public class CustomHierarchy
{
    #region NormalProperties
    private static GUIStyle iconStyle;
    private static GUIStyle colorLabelStyle;

    private static CustomHierarchyConfig customConfig;
    /// <summary>
    /// 图标Rect大小
    /// </summary>
    private static int iconRectSize = 18;
    /// <summary>
    /// 第一个(最右侧)图标离右侧间距
    /// </summary>
    private static int firstSpacing = 20;
    /// <summary>
    /// 图标之间间距
    /// </summary>
    private static int iconSpacing = 26;
    /// <summary>
    /// 图标最小显示宽度
    /// </summary>
    private static float minIconShowWidth = 170;
    /// <summary>
    /// 最小显示宽度
    /// </summary>
    private static float minShowWidth = 120;

    //颜色字体偏移
    private static float colorFontOffsetX = 18f;
    private static float colorFontOffsetY = 0f;

    /// <summary>
    /// 颜色区块应用时，是否忽略掉文字中的空格
    /// </summary>
    private static bool ignoreSpace = false;
    /// <summary>
    /// HSV数值在0-1之间H为色相，S为饱和度，V为明度
    /// </summary>
    private static float H, S, V;

    /// <summary>
    /// Hierarchy窗口原始背景的颜色
    /// </summary>
    private static Color baseColor;
    /// <summary>
    /// 自定义Hierarchy的配置路径
    /// </summary>
    private const string CustomConfigPath = "/CustomHierarchyConfig.asset";
    /// <summary>
    /// 配置文件夹路径
    /// </summary>
    private const string ConfigFolderPath = "Assets/Editor/GUI";

    /// <summary>
    /// 自定义组件颜色高亮列表
    /// </summary>
    private static List<IconInfoModel> iconInfoList = new List<IconInfoModel>();

    /// <summary>
    /// 区块颜色的数据字典，主要是将原本的数组转为字典，便于根据关键字去查找
    /// </summary>
    private static Dictionary<string, AreaBlockStyle> areaColorDic = new Dictionary<string, AreaBlockStyle>();
    /// <summary>
    /// 已完成添加的区块名称
    /// </summary>
    private static List<string> alreadyAdditionBlockName = new List<string>();
    /// <summary>
    /// 子物体颜色表
    /// </summary>
    private static Dictionary<int, AreaBlockStyle> subColorDic = new Dictionary<int, AreaBlockStyle>();
    /// <summary>
    /// 配置表是否初始化完成
    /// </summary>
    private static bool configIsInit = false;
    /// <summary>
    /// 缩放为0时使用的颜色
    /// </summary>
    private static Color32 scaleZeroColor = new Color(0.48627f, 0.48627f, 0.48627f, 1);
    
    /// <summary>
    /// 对象的实例化id数据字典
    /// </summary>
    private static Dictionary<int, InstanceInfo> instanceIdDic = new Dictionary<int, InstanceInfo>();
    /// <summary>
    /// 区块颜色刷新是否完成
    /// </summary>
    private static bool blockAreaRefreshComplete = false;
    /// <summary>
    /// 场景中的所有对象缓存
    /// </summary>
    private static GameObject[] sceneAllObject = null;
    #region DividerParam
    /// <summary>
    /// 分割线的厚度
    /// </summary>
    private static float dividerThickness;
    /// <summary>
    /// 分割线的颜色
    /// </summary>
    private static Color dividerColor;
    #endregion

    #region TreeLine
    /// <summary>
    /// 树形结构线基础颜色
    /// </summary>
    private static Color treeLineBaseColor;
    /// <summary>
    /// 树形结构线层级颜色
    /// </summary>
    private static Color[] treeLineLevelColor;
    /// <summary>
    /// 树形结构线间距
    /// </summary>
    private static float treeLineSpacing;
    /// <summary>
    /// 树形结构线厚度
    /// </summary>
    private static float treeLineThickness;
    /// <summary>
    /// 最多显示几层树形线
    /// </summary>
    private static int treeLineMaxShow;
    private static GameObject[] sceneRoots;
    #endregion
    #endregion

    #region EditorProperties
    //是否开启自定义Hierarchy视图
    private const string CustomHierarchyKey = "EnableCustomHierarchy";
    private static int enableCustomHierarchy = -1;
    internal static bool EnableCustomHierarchy
    {
        get
        {
            if (enableCustomHierarchy == -1)
            {
                enableCustomHierarchy = EditorPrefs.GetInt(CustomHierarchyKey, 1);
            }
            return enableCustomHierarchy == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomHierarchy)
            {
                enableCustomHierarchy = newValue;
                EditorPrefs.SetInt(CustomHierarchyKey, enableCustomHierarchy);
            }
        }
    }

    //是否开启自定义Hierarchy颜色高亮
    private const string CustomHierarchyColorKey = "EnableCustomHierarchyColor";
    private static int enableCustomHierarchyColor = -1;
    internal static bool EnableCustomHierarchyColor
    {
        get
        {
            if (enableCustomHierarchyColor == -1)
            {
                enableCustomHierarchyColor = EditorPrefs.GetInt(CustomHierarchyColorKey, 1);
            }
            return enableCustomHierarchyColor == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomHierarchyColor)
            {
                enableCustomHierarchyColor = newValue;
                EditorPrefs.SetInt(CustomHierarchyColorKey, enableCustomHierarchyColor);
            }
        }
    }

    //是否开启自定义Hierarchy颜色区块
    private const string CustomHierarchyAreaKey = "EnableCustomHierarchyArea";
    private static int enableCustomHierarchyArea = -1;
    internal static bool EnableCustomHierarchyArea
    {
        get
        {
            if (enableCustomHierarchyArea == -1)
            {
                enableCustomHierarchyArea = EditorPrefs.GetInt(CustomHierarchyAreaKey, 1);
            }
            return enableCustomHierarchyArea == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomHierarchyArea)
            {
                enableCustomHierarchyArea = newValue;
                EditorPrefs.SetInt(CustomHierarchyAreaKey, enableCustomHierarchyArea);
            }
        }
    }

    //是否开启自定义颜色区块名称高亮
    private const string CustomHierarchyAreaNameKey = "EnableCustomHierarchyAreaName";
    private static int enableCustomHierarchyAreaName = -1;
    internal static bool EnableCustomHierarchyAreaName
    {
        get
        {
            if (enableCustomHierarchyAreaName == -1)
            {
                enableCustomHierarchyAreaName = EditorPrefs.GetInt(CustomHierarchyAreaNameKey, 1);
            }
            return enableCustomHierarchyAreaName == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomHierarchyAreaName)
            {
                enableCustomHierarchyAreaName = newValue;
                EditorPrefs.SetInt(CustomHierarchyAreaNameKey, enableCustomHierarchyAreaName);
            }
        }
    }

    //区块效果是否影响子节点
    private const string CustomHierarchyAreaEffectContainChildKey = "EnableCustomHierarchyAreaEffectContainChild";
    private static int enableAreaEffectContainChild = -1;
    internal static bool EnableAreaEffectContainChild
    {
        get
        {
            if (enableAreaEffectContainChild == -1)
            {
                enableAreaEffectContainChild = EditorPrefs.GetInt(CustomHierarchyAreaEffectContainChildKey, 0);
            }
            return enableAreaEffectContainChild == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableAreaEffectContainChild)
            {
                enableAreaEffectContainChild = newValue;
                EditorPrefs.SetInt(CustomHierarchyAreaEffectContainChildKey, enableAreaEffectContainChild);
            }
        }
    }

    //区块效果是否只对场景中的第一个同名对象有用
    private const string CustomHierarchyOnlyFindOneKey = "EnableCustomHierarchyOnlyFindOne";
    private static int enableOnlyFindOneSameNameGameObject = -1;
    internal static bool EnableOnlyFindOneSameNameGameObject
    {
        get
        {
            if (enableOnlyFindOneSameNameGameObject == -1)
            {
                enableOnlyFindOneSameNameGameObject = EditorPrefs.GetInt(CustomHierarchyOnlyFindOneKey, 0);
            }
            return enableOnlyFindOneSameNameGameObject == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableOnlyFindOneSameNameGameObject)
            {
                enableOnlyFindOneSameNameGameObject = newValue;
                EditorPrefs.SetInt(CustomHierarchyOnlyFindOneKey, enableOnlyFindOneSameNameGameObject);
            }
        }
    }

    //每次修改数据后回到Hierarchy后，都立即获取最新的公用数据
    private const string CustomHierarchyUpdateCommonConfigKey = "EnableCustomHierarchyUpdateCommonConfig";
    private static int enableUpdateCommonConfig = -1;
    internal static bool EnableUpdateCommonConfig
    {
        get
        {
            if (enableUpdateCommonConfig == -1)
            {
                enableUpdateCommonConfig = EditorPrefs.GetInt(CustomHierarchyUpdateCommonConfigKey, 0);
            }
            return enableUpdateCommonConfig == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableUpdateCommonConfig)
            {
                enableUpdateCommonConfig = newValue;
                EditorPrefs.SetInt(CustomHierarchyUpdateCommonConfigKey, enableUpdateCommonConfig);
            }
        }
    }

    //每次修改数据后回到Hierarchy后，都立即获取最新的颜色区块数据
    private const string CustomHierarchyUpdateBlockConfigKey = "EnableCustomHierarchyUpdateBlockConfig";
    private static int enableUpdateBlockConfig = -1;
    internal static bool EnableUpdateBlockConfig
    {
        get
        {
            if (enableUpdateBlockConfig == -1)
            {
                enableUpdateBlockConfig = EditorPrefs.GetInt(CustomHierarchyUpdateBlockConfigKey, 0);
            }
            return enableUpdateBlockConfig == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableUpdateBlockConfig)
            {
                enableUpdateBlockConfig = newValue;
                EditorPrefs.SetInt(CustomHierarchyUpdateBlockConfigKey, enableUpdateBlockConfig);
            }
        }
    }

    //每次修改数据后回到Hierarchy后，都立即获取最新的高亮组件显示数据
    private const string CustomHierarchyUpdateHighlightConfigKey = "EnableCustomHierarchyUpdateHighlightConfig";
    private static int enableUpdateHighlightConfig = -1;
    internal static bool EnableUpdateHighlightConfig
    {
        get
        {
            if (enableUpdateHighlightConfig == -1)
            {
                enableUpdateHighlightConfig = EditorPrefs.GetInt(CustomHierarchyUpdateHighlightConfigKey, 0);
            }
            return enableUpdateHighlightConfig == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableUpdateHighlightConfig)
            {
                enableUpdateHighlightConfig = newValue;
                EditorPrefs.SetInt(CustomHierarchyUpdateHighlightConfigKey, enableUpdateHighlightConfig);
            }
        }
    }

    //开启自定义分割线
    private const string EnableCustomDividerLineKey = "EnableCustomDividerLine";
    private static int enableCustomDividerLine = -1;
    internal static bool EnableCustomDividerLine
    {
        get
        {
            if (enableCustomDividerLine == -1)
            {
                //默认开启
                enableCustomDividerLine = EditorPrefs.GetInt(EnableCustomDividerLineKey, 1);
            }
            return enableCustomDividerLine == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomDividerLine)
            {
                enableCustomDividerLine = newValue;
                EditorPrefs.SetInt(EnableCustomDividerLineKey, enableCustomDividerLine);
            }
        }
    }

    //开启树形结构线
    private const string EnableCustomTreeLineKey = "EnableCustomTreeLine";
    private static int enableCustomTreeLine = -1;
    internal static bool EnableCustomTreeLine
    {
        get
        {
            if (enableCustomTreeLine == -1)
            {
                //默认开启
                enableCustomTreeLine = EditorPrefs.GetInt(EnableCustomTreeLineKey, 1);
            }
            return enableCustomTreeLine == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableCustomTreeLine)
            {
                enableCustomTreeLine = newValue;
                EditorPrefs.SetInt(EnableCustomTreeLineKey, enableCustomTreeLine);
            }
        }
    }
    #endregion

    #region 结构
    /// <summary>
    /// 组件名字高亮，图标
    /// </summary>
    private struct IconInfoModel
    {
        public Type type;
        public bool changeColor;
        public Color color;

        public IconInfoModel(Type type) : this(type, Color.white, false) { }

        public IconInfoModel(Type type, Color color) : this(type, color, true) { }

        private IconInfoModel(Type type, Color color, bool changeColor) 
        {
            this.type = type;
            this.color = color;
            this.changeColor = changeColor;
        }
    }

    /// <summary>
    /// 颜色区块结构体
    /// </summary>
    private struct AreaBlockStyle
    {
        public Color blockColor;
        public FontStyle fontStyle;
        public Color areaTextColor;
        public CustomFontAnchor areaTextAlignment;

        public AreaBlockStyle(Color blockColor, FontStyle fontStyle)
            : this(blockColor, fontStyle, Color.clear, CustomFontAnchor.Center)
        {}

        public AreaBlockStyle(Color blockColor, FontStyle fontStyle, Color customblockColor, CustomFontAnchor customFontStyle)
        {
            this.blockColor = blockColor;
            this.fontStyle = fontStyle;
            this.areaTextColor = customblockColor;
            this.areaTextAlignment = customFontStyle;
        }
    }

    /// <summary>
    /// 实例化对象的信息
    /// </summary>
    [Serializable]
    private struct InstanceInfo
    {
        /// <summary>
        /// 对象的名称
        /// </summary>
        public string name;
        /// <summary>
        /// 树形结构级别
        /// </summary>
        public int treeLevel;
        /// <summary>
        /// 是否具有子节点
        /// </summary>
        public bool hasChilds;
    }
    #endregion

    #region 构造函数与析构函数
    static CustomHierarchy()
    {
        //hierarchy窗口中的每个可见物体的OnGUI触发时，就会派发hierarchyWindowItemOnGUI事件，然后就可以去执行对应的事件处理器了
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowOnGUIHandler;
        //Hierarchy窗口变化回调（创建对象以及对其进行重命名、重定父级或销毁，以及加载、卸载、重命名或重新排序已加载的场景） 
        EditorApplication.hierarchyChanged += HierarchyChangedHandler;

        colorLabelStyle = new GUIStyle();
        iconStyle = new GUIStyle();
        iconStyle.fixedWidth = iconRectSize - 2;
        iconStyle.fixedHeight = iconRectSize - 2;

        baseColor = GUI.backgroundColor;
        configIsInit = false;
        //加载配置表
        customConfig = (CustomHierarchyConfig)AssetDatabase.LoadAssetAtPath<ScriptableObject>(ConfigFolderPath + CustomConfigPath);

        InitConfigAndData();
    }

    ~CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowOnGUIHandler;
        EditorApplication.hierarchyChanged -= HierarchyChangedHandler;

        GUI.backgroundColor = baseColor;
    }
    #endregion

    #region 配置加载
    /// <summary>
    /// 重置初始化标记
    /// </summary>
    public static void RestConfigInitMark()
    {
        configIsInit = false;
        customConfig = null;
    }

    /// <summary>
    /// 初始化配置
    /// </summary>
    public static void InitConfigAndData(int instanceId = 0)
    {
        if (customConfig != null)
        {
            UpdateCommonConfig();
            UpdateBlockColorConfig();
            UpdateHighlightConfig();
            UpdateAreaColorList(instanceId);
            UpdateDividerConfig();
            UpdateTreeLineConfig();
            configIsInit = true;
        }
        else
        {
            //使用关键字“t”去基于类型查找
            string[] dataArray = AssetDatabase.FindAssets("t:CustomHierarchyConfig");

            if (dataArray.Length >= 1)
            {
                string path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
                customConfig = AssetDatabase.LoadAssetAtPath<CustomHierarchyConfig>(path);
                InitConfigAndData();
                Debug.LogWarning("您的资源实际路径似乎和代码中设置的静态路径不同，请修改代码路径或资源实际位置");
                return;
            }
            else 
            {
                //在项目中进行查找后依旧不存在配置，提升是否创建对应的配置文件
                if (EditorUtility.DisplayDialog("Custom Hierarchy", "是否创建自定义Hierarchy的资源配置?", "是", "否"))
                {
                    CreateAsset();
                    InitConfigAndData();
                }
                else
                {
                    Debug.LogError("您开启了Hierarchy增强，但是却没有对应的自定义配置");
                }
            }
            
        }
    }

    /// <summary>
    /// 创建Asset资源文件
    /// </summary>
    static void CreateAsset()
    {
        //不存在对应的文件夹，就创建文件夹
        if (!AssetDatabase.IsValidFolder(ConfigFolderPath))
            AssetDatabase.CreateFolder("Assets/Editor", "GUI");
        try
        {
            //创建Asset资源
            CustomHierarchyConfig asset = ScriptableObject.CreateInstance<CustomHierarchyConfig>();
            AssetDatabase.CreateAsset(asset, ConfigFolderPath + "/CustomHierarchyConfig.asset");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        AssetDatabase.SaveAssets();
        Debug.Log("自定义Hierarchy配置文件已经创建完成");
    }

    /// <summary>
    /// 更新公用配置
    /// </summary>
    public static void UpdateCommonConfig()
    {
        if (!configIsInit || EnableUpdateCommonConfig)
        {
            iconRectSize = customConfig.iconRectSize;
            firstSpacing = customConfig.firstSpacing;
            iconSpacing = customConfig.iconSpacing;
            minIconShowWidth = customConfig.minIconShowWidth;
            minShowWidth = customConfig.minShowWidth;
            colorFontOffsetX = customConfig.fontColorOffsetX;
            colorFontOffsetY = customConfig.fontColorOffsetY;
            ignoreSpace = customConfig.ignoreSpace;
            if (customConfig.scaleZeroColor != Color.clear)
                scaleZeroColor = customConfig.scaleZeroColor;
        }
    }

    /// <summary>
    /// 更新分割线数据
    /// </summary>
    public static void UpdateDividerConfig() 
    {
        if (EnableCustomDividerLine) 
        {
            dividerThickness = customConfig.dividerThickness;
            dividerColor = customConfig.dividerColor;
        }
    }

    /// <summary>
    /// 更新树形结构线配置
    /// </summary>
    public static void UpdateTreeLineConfig()
    {
        treeLineBaseColor = customConfig.treeLineBaseColor;
        treeLineLevelColor = customConfig.treeLineLevelColor;
        treeLineSpacing = customConfig.treeLineSpacing;
        treeLineThickness = customConfig.treeLineThickness;
        treeLineMaxShow = customConfig.treeLineMaxShow - 1;
    }

    /// <summary>
    /// 更新颜色区块数据
    /// </summary>
    public static void UpdateBlockColorConfig()
    {
        if (!configIsInit || EnableUpdateBlockConfig)
        {
            areaColorDic.Clear();
            CustomHierarchyConfig.AreaColorModel[] blockModels = customConfig.blockModels;
            for (int i = 0; i < blockModels.Length; i++)
            {
                if (blockModels[i].targetName != null && !blockModels[i].targetName.Equals(""))
                {
                    string temporaryStr = blockModels[i].targetName;
                    if (ignoreSpace)
                        temporaryStr = OtherUtility.ReplaceSpaceByFormat(temporaryStr);
                    AreaBlockStyle temporaryAreaBlockStyle =
                        new AreaBlockStyle(blockModels[i].blockColor, blockModels[i].fontStyle, blockModels[i].customAreaTextColor,
                        blockModels[i].customAreaTextAlignment);
                    areaColorDic.Add(temporaryStr, temporaryAreaBlockStyle);
                }
            }
        }
    }

    /// <summary>
    /// 更新高亮组件数据
    /// </summary>
    public static void UpdateHighlightConfig()
    {
        if (!configIsInit || EnableUpdateHighlightConfig)
        {
            iconInfoList.Clear();
            CustomHierarchyConfig.HighlightColorModel[] highlightModels = customConfig.highlightModels;
            for (int i = 0; i < highlightModels.Length; i++)
            {
                if (highlightModels[i].assemblyName != null && !highlightModels[i].assemblyName.Equals(""))
                {
                    Assembly assembly;
                    try
                    {
                        //尝试加载程序集
                        assembly = Assembly.Load(highlightModels[i].assemblyName);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.StackTrace + "\n" + e.Message);
                        continue;
                    }

                    if (assembly == null)
                    {
                        Debug.LogWarning("未找到程序集: " + highlightModels[i].assemblyName);
                        continue;
                    }

                    CustomHierarchyConfig.HighlightColorItem[] highlightItems = highlightModels[i].highlightItems;
                    for (int j = 0; j < highlightItems.Length; j++)
                    {
                        if (highlightItems[j].componentName != null && !highlightItems[j].componentName.Equals(""))
                        {
                            //利用反射，从程序集中获取组件类型
                            Type type = assembly.GetType(highlightModels[i].assemblyName + "." + highlightItems[j].componentName);
                            if (type == null && (type = assembly.GetType(highlightItems[j].componentName)) == null)
                            {
                                Debug.LogWarning($"程序集{highlightModels[i].assemblyName}中未包含: {highlightItems[j].componentName}，无法设置组件颜色高亮");
                                continue;
                            }
                            iconInfoList.Add(new IconInfoModel(type, highlightItems[j].color));
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region 图标绘制相关
    //根据index排序获取对应Rect大小
    private static Rect CreateRect(Rect selectionRect, ref int index)
    {
        Rect rect = new Rect(selectionRect);
        rect.x += rect.width - firstSpacing - (iconSpacing * index);
        rect.width = iconRectSize;
        index++;
        return rect;
    }

    //根据类型绘制图标
    private static void DrawComponentIcon(Rect rect, Type type, GUIStyle customStyle = null)
    {
        //获得Unity内置的图标
        Texture icon = EditorGUIUtility.ObjectContent(null, type).image;
        if (icon == null)
        {
            return;
        }
        if (customStyle != null)
        {
            GUI.Label(rect, icon, customStyle);
        }
        else
        {
            Color origin = GUI.color;
            GUI.color = GUI.backgroundColor;
            GUI.Label(rect, icon, iconStyle);
            GUI.color = origin;
        }
    }

    /// <summary>
    /// 根据类型绘制对应图标和颜色
    /// </summary>
    /// <param name="type">组件类型</param>
    /// <param name="selectionRect"></param>
    /// <param name="go">对象</param>
    /// <param name="order"></param>
    /// <returns></returns>
    private static bool Draw(Type type, Rect selectionRect, GameObject go, ref int order)
    {
        if (!HasComponent(go, type, false))
        {
            return false;
        }
        Rect rect = CreateRect(selectionRect, ref order);
        DrawComponentIcon(rect, type);
        return true;
    }
    #endregion

    #region 区块颜色相关
    /// <summary>
    /// 更新区域颜色列表
    /// </summary>
    private static void UpdateAreaColorList(int instanceId = 0)
    {
        if (!EnableCustomHierarchyArea || instanceIdDic.ContainsKey(instanceId) || blockAreaRefreshComplete)
            return;
        subColorDic.Clear();
        alreadyAdditionBlockName.Clear();
        //获取场景中所有的游戏对象，FindObjectsOfType获取的是无序的，使用OrderBy进行排序
        sceneAllObject = GameObject.FindObjectsOfType<GameObject>()
            .OrderBy(obj => obj.transform.GetSiblingIndex()).ToArray();

        if (sceneAllObject.Length != 0)
        {
            foreach (GameObject obj in sceneAllObject)
            {
                string objName = obj.name;
                if (ignoreSpace)
                    objName = OtherUtility.ReplaceSpaceByFormat(objName);
                //检测对象的名称是否包含在区块中
                if (areaColorDic.ContainsKey(objName))
                {
                    //只在Hierarchy中查找第一个具有相同名称的对象
                    if (EnableOnlyFindOneSameNameGameObject && alreadyAdditionBlockName.Contains(objName))
                    {
                        continue;
                    }
                    AddAreaColorChilds(obj, areaColorDic[objName]);
                }
            }
        }
        blockAreaRefreshComplete = true;
    }

    /// <summary>
    /// 将对象下的所有子节点都添加进字典中，后续会将所有的子节点都设置为同一颜色
    /// </summary>
    /// <param name="targetObject">目标对象</param>
    /// <param name="blockStyle">在Hierarchy窗口中显示的区块样式</param>
    private static void AddAreaColorChilds(GameObject targetObject, AreaBlockStyle blockStyle)
    {
        //将对象的实例化ID作为Key添加到字典中
        subColorDic.Add(targetObject.GetInstanceID(), blockStyle);
        string objName = targetObject.name;
        if (ignoreSpace)
            objName = OtherUtility.ReplaceSpaceByFormat(objName);
        if (!alreadyAdditionBlockName.Contains(objName))
        {
            alreadyAdditionBlockName.Add(objName);
        }

        //如果区块效果影响的范围涉及子节点
        if (EnableAreaEffectContainChild)
        {
            int childCount = targetObject.transform.childCount;
            if (childCount != 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    AddAreaColorChilds(targetObject.transform.GetChild(i).gameObject, blockStyle);
                }
            }
        }
    }
    #endregion

    #region 绘制HierarchyTree
    /// <summary>
    /// 分析并添加实例化数据
    /// </summary>
    /// <param name="instanceObj">实例化游戏对象</param>
    /// <param name="treeLevel">层级结构</param>
    private static void AnalyzeAndAdditionInstanceInfo(GameObject instanceObj, int treeLevel) 
    {
        int instanceId = instanceObj.GetInstanceID();
        int childCount = instanceObj.GetChildCountExtension();
        //数据字典中未包含该实例化ID就将其添加进去
        if (!instanceIdDic.ContainsKey(instanceId))
        {
            blockAreaRefreshComplete = false;
            InstanceInfo theInstanceInfo = new InstanceInfo();
            theInstanceInfo.name = instanceObj.name;
            theInstanceInfo.hasChilds = childCount > 0;
            theInstanceInfo.treeLevel = treeLevel;
            instanceIdDic.Add(instanceId, theInstanceInfo);
        }
        for (int index = 0; index < childCount; index++)
        {
            AnalyzeAndAdditionInstanceInfo(instanceObj.transform.GetChild(index).gameObject, treeLevel + 1);
        }
    }

    /// <summary>
    /// 获取树形结构的起始横坐标
    /// </summary>
    /// <param name="treeLevel">层级</param>
    /// <returns></returns>
    private static float GetTreeLineStartX(int treeLevel)
    {
        return 37 + treeLineSpacing * treeLevel;    
    }

    /// <summary>
    /// 绘制完整的垂直线段
    /// </summary>
    /// <param name="originalRect">HierarchyItem原始的Rect</param>
    /// <param name="treeLevel">层级</param>
    private static void DrawVerticalLine(Rect originalRect, int treeLevel)
    {
        DrawHalfVerticalLine(originalRect, true, treeLevel);
        DrawHalfVerticalLine(originalRect, false, treeLevel);
    }

    private static void DrawHalfVerticalLine(Rect originalRect, bool startsOnTop, int treeLevel)
    {
        if (treeLineLevelColor.Length <= 0 || treeLevel > (treeLineLevelColor.Length - 1) || treeLevel > treeLineMaxShow)
            return;
        DrawHalfVerticalLine(originalRect, startsOnTop, treeLevel, treeLineLevelColor[treeLevel]);
    }

    /// <summary>
    /// 绘制垂直线段
    /// </summary>
    /// <param name="originalRect">HierarchyItem原始的Rect</param>
    /// <param name="drawTopHalfLine">绘制的是否是上半部分线段</param>
    /// <param name="treeLevel">层级</param>
    /// <param name="color">线段颜色</param>
    private static void DrawHalfVerticalLine(Rect originalRect, bool drawTopHalfLine, int treeLevel, Color color)
    {
        EditorGUI.DrawRect(
            new Rect(GetTreeLineStartX(treeLevel),drawTopHalfLine ? originalRect.y : (originalRect.y + originalRect.height / 2f),
                treeLineThickness, originalRect.height / 2f),color
                );
    }

    /// <summary>
    /// 绘制水平线段
    /// </summary>
    /// <param name="originalRect">HierarchyItem原始的Rect</param>
    /// <param name="treeLevel">层级</param>
    /// <param name="hasChilds">是否具有子节点</param>
    private static void DrawHorizontalLine(Rect originalRect, int treeLevel, bool hasChilds)
    {
        if (treeLineLevelColor.Length <= 0 || treeLevel > (treeLineLevelColor.Length - 1) || treeLevel > treeLineMaxShow)
            return;

        EditorGUI.DrawRect(
            //在Item高度一半的位置处画水平线段
            new Rect(GetTreeLineStartX(treeLevel),originalRect.y + originalRect.height / 2f,
                originalRect.height + (hasChilds ? -5 : 2), treeLineThickness),treeLineLevelColor[treeLevel]
            );
    }

    /// <summary>
    /// 绘制树形结构线
    /// </summary>
    /// <param name="instanceId">对象的实例化id</param>
    /// <param name="originalRect">HierarchyItem原始的Rect</param>
    private static void DrawTreeLine(int instanceId, Rect originalRect)
    {
        InstanceInfo theInstanceInfo = instanceIdDic[instanceId];
        //在Hierarchy中进行搜索时，需要禁止绘制树形结构线
        if (originalRect.x >= 60)
        {
            //层级是0就使用基础颜色
            if (theInstanceInfo.treeLevel == 0 && !theInstanceInfo.hasChilds)
            {
                DrawHalfVerticalLine(originalRect, true, 0, treeLineBaseColor);
                DrawHalfVerticalLine(originalRect, false, 0, treeLineBaseColor);
            }
            else
            {
                //为每个先前的嵌套级别绘制一条垂直线
                for (int level = 0; level <= theInstanceInfo.treeLevel; level++)
                {
                    DrawVerticalLine(originalRect, level);
                }

                DrawHorizontalLine(originalRect, theInstanceInfo.treeLevel, theInstanceInfo.hasChilds);
            }

        }
    }
    #endregion

    #region 绘制分割线
    /// <summary>
    /// 绘制分割线
    /// </summary>
    /// <param name="selectionRect">原始的Rect</param>
    private static void DrawDividerLine(Rect selectionRect) 
    {
        if (EnableCustomDividerLine && dividerThickness > 0)
        {
            Rect boldGroupRect = new Rect(32, selectionRect.yMax, selectionRect.width + selectionRect.x,dividerThickness);
            EditorGUI.DrawRect(boldGroupRect, dividerColor);
        }
    }
    #endregion

    #region 事件处理器
    /// <summary>
    /// 当Hierarchy发生更新时，执行的事件处理器
    /// </summary>
    public static void HierarchyChangedHandler()
    {
        if (!EnableCustomHierarchy)
            return;
        instanceIdDic.Clear();
        //Stopwatch可以测量一个时间间隔的运行时间，也可以测量多个时间间隔的总运行时间。一般用来测量代码执行所用的时间或者计算性能数据
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        //每次Hierarchy发生改变是，我们就必须检查一次是否有新的区块颜色被添加进去
        UpdateAreaColorList();

        stopWatch.Stop();
        long expendTime = stopWatch.ElapsedMilliseconds;
        if (expendTime > 10)
        {
            Debug.LogWarning($"Hierarchy Changed耗时较长: {stopWatch.ElapsedMilliseconds}ms");
        }

        if (EnableCustomTreeLine) 
        {
            UnityEditor.SceneManagement.PrefabStage thePrefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (thePrefabStage != null)
            {
                //预制件的根节点
                AnalyzeAndAdditionInstanceInfo(UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage().prefabContentsRoot, -1);
                return;
            }

            Scene tempScene;
            //遍历所有的已加载场景，如果不在这里遍历，那么就有可能导致树形结构线出错
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                tempScene = SceneManager.GetSceneAt(i);
                if (tempScene.isLoaded)
                {
                    //获取场景的根节点
                    sceneRoots = tempScene.GetRootGameObjects();
                    for (int j = 0; j < sceneRoots.Length; j++)
                    {
                        AnalyzeAndAdditionInstanceInfo(sceneRoots[j], 0);
                    }
                }
            }
        }
    }

    //绘制自定义的Hiercrchy
    private static void HierarchyWindowOnGUIHandler(int instanceId, Rect selectionRect)
    {
        if (!EnableCustomHierarchy)
            return;
        //将实例化id转换为对象
        GameObject instanceObj = (GameObject)EditorUtility.InstanceIDToObject(instanceId);
        Color customFontColor = Color.clear;
        if (instanceObj == null)
            return;
        try
        {
            int insID = instanceId;
            //图标排序的索引
            int index = 0;
            float curHalfWidth = EditorGUIUtility.currentViewWidth / 2;
            InitConfigAndData(insID);

            if (EnableCustomTreeLine) 
            {
                //当处于Prefab编辑阶段时，初始层级就不再是0了。
                if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
                    AnalyzeAndAdditionInstanceInfo(instanceObj, -1);
                else
                    AnalyzeAndAdditionInstanceInfo(instanceObj, 0);
                DrawTreeLine(insID, selectionRect);
            }

            GUI.backgroundColor = baseColor;
            //显示区块颜色
            if (EnableCustomHierarchyArea)
            {
                if (subColorDic.ContainsKey(insID))
                {
                    //启用了分割线，就得重新调整绘制的矩形区域
                    if (EnableCustomDividerLine) 
                    {
                        Rect adjustRect = new Rect(selectionRect.x, selectionRect.y + dividerThickness + 0.1f,
                        selectionRect.width, selectionRect.height - dividerThickness - 0.1f);
                        EditorGUI.DrawRect(adjustRect, subColorDic[insID].blockColor);
                    }
                    else
                        EditorGUI.DrawRect(selectionRect, subColorDic[insID].blockColor);
                }
            }

            //大于最小宽度时候的显示
            if (minShowWidth < curHalfWidth)
            {
                //Active勾选框
                bool activeToggle = GUI.Toggle(CreateRect(selectionRect, ref index), instanceObj.activeSelf, string.Empty);
                if (activeToggle != instanceObj.activeSelf)
                {
                    instanceObj.SetActive(activeToggle);
                    if (EditorApplication.isPlaying == false)
                    {
                        //场景标脏
                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(instanceObj.scene);
                        EditorUtility.SetDirty(instanceObj);
                    }
                }

                //静态对象标记显示
                if (instanceObj.isStatic)
                {
                    Rect rectIcon = CreateRect(selectionRect, ref index);
                    GUI.Label(rectIcon, "S");
                }
            }

            //绘制图标和颜色名称，小于最小图标绘制宽度时只改变名称颜色
            if (curHalfWidth > minIconShowWidth)
            {
                foreach (IconInfoModel model in iconInfoList)
                {
                    if (Draw(model.type, selectionRect, instanceObj, ref index))
                    {
                        if (model.changeColor)
                        {
                            customFontColor = model.color;
                        }
                    }
                }
            }
            else
            {
                foreach (IconInfoModel model in iconInfoList)
                {
                    if (HasComponent(instanceObj, model.type, false))
                    {
                        if (model.changeColor)
                        {
                            customFontColor = model.color;
                        }
                    }
                }
            }

            Rect targetRect = selectionRect;
            targetRect.x += colorFontOffsetX;
            targetRect.y += colorFontOffsetY;
            bool scaleZeroMark = instanceObj.transform.localScale.magnitude == 0;
            //缓存当前的样式设置，避免因为后续的操作导致样式无法还原
            Color originColor = colorLabelStyle.normal.textColor;
            FontStyle originCustomFontStyle = colorLabelStyle.fontStyle;
            TextAnchor originCustomAlignment = TextAnchor.UpperLeft;

            if (scaleZeroMark && !subColorDic.ContainsKey(insID))
            {
                colorLabelStyle.normal.textColor = scaleZeroColor;
                GUI.Label(targetRect, instanceObj.name, colorLabelStyle);
                colorLabelStyle.normal.textColor = originColor;
            }
            else
            {
                //开启了增强显示，且在Hierarchy窗口中是处于激活状态，就可以执行自定义颜色
                if (EnableCustomHierarchyAreaName && subColorDic.ContainsKey(insID))
                {
                    AreaBlockStyle temporary = subColorDic[insID];
                    if (temporary.areaTextColor == Color.clear || temporary.areaTextColor.a == 0)
                    {
                        //利用色相设置区块字体的颜色显示
                        Color.RGBToHSV(temporary.blockColor, out H, out S, out V);
                        H -= 0.43f;
                        if (H < 0)
                            H = 1 + H;
                        S = 1;
                        V = 1;
                        colorLabelStyle.normal.textColor = Color.HSVToRGB(H, S, V);
                    }
                    else
                        colorLabelStyle.normal.textColor = temporary.areaTextColor;
                    //自定义的字体对齐方式是居中时调整Label的Rect位置
                    if (temporary.areaTextAlignment == CustomFontAnchor.Center && minShowWidth < curHalfWidth)
                        targetRect.x -= colorFontOffsetX;
                    //将自定义的枚举类型转换为int索引，然后再将索引转换为另一个Enum
                    colorLabelStyle.alignment = (TextAnchor)(int)temporary.areaTextAlignment;
                    colorLabelStyle.fontStyle = temporary.fontStyle;
                    GUI.Label(targetRect, instanceObj.name, colorLabelStyle);
                    colorLabelStyle.normal.textColor = originColor;
                    colorLabelStyle.fontStyle = originCustomFontStyle;
                    colorLabelStyle.alignment = originCustomAlignment;
                }
                else if (EnableCustomHierarchyColor && (customFontColor != Color.clear) && instanceObj.activeInHierarchy)
                {
                    colorLabelStyle.normal.textColor = customFontColor;
                    GUI.Label(targetRect, instanceObj.name, colorLabelStyle);
                    colorLabelStyle.normal.textColor = originColor;
                }
            }

            if (scaleZeroMark)
            {
                Rect rectIcon = CreateRect(selectionRect, ref index);
                GUI.Label(rectIcon, "Z");
            }
            DrawDividerLine(selectionRect);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n" + HighlightStackTrace(e.StackTrace));
        }
    }
    #endregion

    #region DebugStack
    /// <summary>
    /// 调用栈追踪文件名高亮
    /// </summary>
    /// <param name="stackTrace"></param>
    /// <returns></returns>
    public static string HighlightStackTrace(string stackTrace)
    {
        if (stackTrace == null || stackTrace.Length == 0)
        {
            return stackTrace;
        }
        string[] splited = stackTrace.Split('\n');
        StringBuilder strBuilder = new StringBuilder();
        foreach (string str in splited)
        {
            string[] temp = str.Split('\\');
            if (temp.Length > 0)
            {
                strBuilder.Append(str.Replace(temp[temp.Length - 1], string.Format("<color=yellow>{0}</color>\n", temp[temp.Length - 1])));
            }
            else
            {
                strBuilder.Append(str);
            }
        }

        return strBuilder.ToString();
    }
    #endregion

    #region 检测对象中是否含有组件
    /// <summary>
    /// 检测对象上是否含有对应组件
    /// </summary>
    /// <typeparam name="T">泛型组件类型</typeparam>
    /// <param name="go">检测的对象</param>
    /// <param name="checkChildren">是否检测子层级</param>
    /// <returns>是否含有期望组件</returns>
    public static bool HasComponent<T>(GameObject go, bool checkChildren) where T : Component
    {
        if (!checkChildren)
        {
            return go.GetComponent<T>();
        }
        else
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }

    /// <summary>
    /// 检测对象上是否含有对应组件
    /// </summary>
    /// <param name="go">检测的对象</param>
    /// <param name="type">检查的组件类型</param>
    /// <param name="checkChildren">是否检测子层级</param>
    /// <returns>是否含有期望组件</returns>
    public static bool HasComponent(GameObject go, Type type, bool checkChildren)
    {
        if (!checkChildren)
        {
            return go.GetComponent(type) != null;
        }
        else
        {
            return go.GetComponentsInChildren(type).FirstOrDefault() != null;
        }
    }
    #endregion
}

/// <summary>
/// 自定义Hierarchy增强显示开关
/// </summary>
public class EnableCustomHierarchy : EditorWindow
{
    #region ConstProperty
    private const string ToggleTitle = "游戏工具/GUI增强/Hierarchy自定义/开启Hierarchy自定义增强显示";
    private const string ColorToggleTitle = "游戏工具/GUI增强/Hierarchy自定义/开启Hierarchy组件颜色高亮";
    private const string AreaToggleTitle = "游戏工具/GUI增强/Hierarchy自定义/颜色区块设置/开启Hierarchy颜色区块";
    private const string AreaNameToggleTitle = "游戏工具/GUI增强/Hierarchy自定义/颜色区块设置/开启Hierarchy颜色区块名称高亮";
    private const string AreaEffectContainChild = "游戏工具/GUI增强/Hierarchy自定义/颜色区块设置/Hierarchy颜色区块同时影响其子节点";
    private const string OnlyFindOneSameNameGameObject = "游戏工具/GUI增强/Hierarchy自定义/颜色区块设置/Hierarchy颜色区块只影响第一个同名的对象";
    private const string UpdateCommonConfig = "游戏工具/GUI增强/Hierarchy自定义/配置设置/每次修改数据后都立即更新公用配置";
    private const string UpdateBlockConfig = "游戏工具/GUI增强/Hierarchy自定义/配置设置/每次修改数据后都立即更新颜色区块配置";
    private const string UpdateHighlightConfig = "游戏工具/GUI增强/Hierarchy自定义/配置设置/每次修改数据后都立即更新高亮配置";
    private const string DividerLineToggle = "游戏工具/GUI增强/Hierarchy自定义/开启分割线";
    private const string TreeLineToggle = "游戏工具/GUI增强/Hierarchy自定义/开启树形结构线";
    private const string InfoTitle = "游戏工具/GUI增强/Hierarchy自定义/说明";
    #endregion
    #region MenuItem
    #region IconToggle
    //是否开启自定义Hierarchy增强视图
    [MenuItem(ToggleTitle, false, 100)]
    public static void IconToggle()
    {
        CustomHierarchy.EnableCustomHierarchy = !CustomHierarchy.EnableCustomHierarchy;
        if (CustomHierarchy.EnableCustomHierarchy)
        {
            CustomHierarchy.RestConfigInitMark();
            CustomHierarchy.InitConfigAndData();
        }
    }
    [MenuItem(ToggleTitle, true)]
    public static bool SetIconToggle()
    {
        Menu.SetChecked(ToggleTitle, CustomHierarchy.EnableCustomHierarchy);
        return true;
    }
    #endregion

    #region ColorToggle
    //自定义Hierarchy颜色高亮开关
    [MenuItem(ColorToggleTitle, false, 101)]
    public static void ColorToggle()
    {
        CustomHierarchy.EnableCustomHierarchyColor = !CustomHierarchy.EnableCustomHierarchyColor;
        CustomHierarchy.HierarchyChangedHandler();
    }
    [MenuItem(ColorToggleTitle, true, 101)]
    public static bool SetColorToggle()
    {
        Menu.SetChecked(ColorToggleTitle, CustomHierarchy.EnableCustomHierarchyColor);
        return true;
    }
    #endregion

    #region BlockAreaToggle
    //自定义Hierarchy颜色区块开关
    [MenuItem(AreaToggleTitle, false, 102)]
    public static void AreaToggle()
    {
        CustomHierarchy.EnableCustomHierarchyArea = !CustomHierarchy.EnableCustomHierarchyArea;
        CustomHierarchy.HierarchyChangedHandler();
    }
    [MenuItem(AreaToggleTitle, true, 102)]
    public static bool SetAreaToggle()
    {
        Menu.SetChecked(AreaToggleTitle, CustomHierarchy.EnableCustomHierarchyArea);
        return true;
    }
    #endregion

    #region BlockAreaNameToggle
    //自定义颜色区块名称高亮开关
    [MenuItem(AreaNameToggleTitle, false, 103)]
    public static void AreaNameToggle()
    {
        CustomHierarchy.EnableCustomHierarchyAreaName = !CustomHierarchy.EnableCustomHierarchyAreaName;
        CustomHierarchy.HierarchyChangedHandler();
    }
    [MenuItem(AreaNameToggleTitle, true, 103)]
    public static bool SetAreaNameToggle()
    {
        Menu.SetChecked(AreaNameToggleTitle, CustomHierarchy.EnableCustomHierarchyAreaName);
        return true;
    }
    #endregion

    #region BlockAreaEffectContainChildToggle
    //颜色区块同时影响其子节点开关
    [MenuItem(AreaEffectContainChild, false, 104)]
    public static void AreaEffectContainChildToggle()
    {
        CustomHierarchy.EnableAreaEffectContainChild = !CustomHierarchy.EnableAreaEffectContainChild;
        CustomHierarchy.HierarchyChangedHandler();
    }
    [MenuItem(AreaEffectContainChild, true, 104)]
    public static bool SetAreaEffectContainChildToggle()
    {
        Menu.SetChecked(AreaEffectContainChild, CustomHierarchy.EnableAreaEffectContainChild);
        return true;
    }
    #endregion

    #region OnlyFindOneToggle
    //是否只对查找场景中的第一个同名对象应用效果
    [MenuItem(OnlyFindOneSameNameGameObject, false, 105)]
    public static void OnlyFindOneToggle()
    {
        CustomHierarchy.EnableOnlyFindOneSameNameGameObject = !CustomHierarchy.EnableOnlyFindOneSameNameGameObject;
        CustomHierarchy.HierarchyChangedHandler();
    }
    [MenuItem(OnlyFindOneSameNameGameObject, true, 105)]
    public static bool SetOnlyFindOneToggle()
    {
        Menu.SetChecked(OnlyFindOneSameNameGameObject, CustomHierarchy.EnableUpdateCommonConfig);
        return true;
    }
    #endregion

    #region UpdateCommonConfigToggle
    //是否在每次绘制HierarchyGUI时都更新公用配置数据
    [MenuItem(UpdateCommonConfig, false, 106)]
    public static void UpdateCommonConfigToggle()
    {
        CustomHierarchy.EnableUpdateCommonConfig = !CustomHierarchy.EnableUpdateCommonConfig;
        CustomHierarchy.UpdateCommonConfig();
    }
    [MenuItem(UpdateCommonConfig, true, 106)]
    public static bool SetUpdateCommonConfigToggle()
    {
        Menu.SetChecked(UpdateCommonConfig, CustomHierarchy.EnableUpdateCommonConfig);
        return true;
    }
    #endregion

    #region UpdateBlockConfigToggle
    //是否在每次绘制HierarchyGUI时都更新颜色区块配置数据
    [MenuItem(UpdateBlockConfig, false, 107)]
    public static void UpdateBlockConfigToggle()
    {
        CustomHierarchy.EnableUpdateBlockConfig = !CustomHierarchy.EnableUpdateBlockConfig;
        CustomHierarchy.UpdateBlockColorConfig();
    }
    [MenuItem(UpdateBlockConfig, true, 107)]
    public static bool SetUpdateBlockConfigToggle()
    {
        Menu.SetChecked(UpdateBlockConfig, CustomHierarchy.EnableUpdateBlockConfig);
        return true;
    }
    #endregion

    #region UpdateHighlightConfigToggle
    //是否在每次绘制HierarchyGUI时都更新高亮组件配置数据
    [MenuItem(UpdateHighlightConfig, false, 108)]
    public static void UpdateHighlightConfigToggle()
    {
        CustomHierarchy.EnableUpdateHighlightConfig = !CustomHierarchy.EnableUpdateHighlightConfig;
        CustomHierarchy.UpdateHighlightConfig();
    }
    [MenuItem(UpdateHighlightConfig, true, 108)]
    public static bool SetUpdateHighlightConfigToggle()
    {
        Menu.SetChecked(UpdateHighlightConfig, CustomHierarchy.EnableUpdateHighlightConfig);
        return true;
    }
    #endregion

    #region DividerLine
    //是否启用自定义分割线
    [MenuItem(DividerLineToggle, false, 109)]
    public static void UpdateDividerLineToggle()
    {
        CustomHierarchy.EnableCustomDividerLine = !CustomHierarchy.EnableCustomDividerLine;
        CustomHierarchy.UpdateDividerConfig();
    }
    [MenuItem(DividerLineToggle, true, 109)]
    public static bool SetDividerLineToggle()
    {
        Menu.SetChecked(DividerLineToggle, CustomHierarchy.EnableCustomDividerLine);
        return true;
    }
    #endregion

    #region TreeLine
    //是否启用自定义树形结构线
    [MenuItem(TreeLineToggle, false, 109)]
    public static void UpdateTreeLineToggle()
    {
        CustomHierarchy.EnableCustomTreeLine = !CustomHierarchy.EnableCustomTreeLine;
        CustomHierarchy.UpdateTreeLineConfig();
    }
    [MenuItem(TreeLineToggle, true, 109)]
    public static bool SetTreeLineToggle()
    {
        Menu.SetChecked(TreeLineToggle, CustomHierarchy.EnableCustomTreeLine);
        return true;
    }
    #endregion

    #region InfoTitle
    //说明面板
    [MenuItem(InfoTitle, false, 109)]
    public static void InfoPanel()
    {
        GetWindow<CustomHierarchyInfo>("自定义Hierarchy说明");
    }
    #endregion
    #endregion
}

#region 自定义Hierarchy说明面板
/// <summary>
/// 自定义Hierarchy说明面板
/// </summary>
class CustomHierarchyInfo : EditorWindow
{
    private const string info = @"
    >该工具可以在Hierarchy窗口显示物体上所带有的组件图标以及该物体是否为static/active等状态信息，
    且可以根据物体所带有的组件或其名称来改变物体名称的颜色等。

    >如果想显示更多信息，可在CustomHierarchy.cs的HierarchyWindowOnGUI方法中添加。

    >组件图标显示和组件颜色高亮可在<color=#FFE604>Scripts/Game/Editor/GUI/CustomHierarchyConfig.asset</color>中配置
    例如希望显示Canvas的图标并使带有Canvas组件的物体名显示为某种颜色，则在<color=#04FF6D>Highlight Models</color>下程序集为
    UnityEngine的Items中填入Canvas并设置对应颜色即可

    >颜色区块可以使某一名称的物体包括其子物体显示为某一颜色
    具体可以在<color=#FFE604>Scripts/Game/Editor/GUI/CustomHierarchyConfig.asset</color>中配置
    例如希望MainLayer及其子物体显示为黄色，则在<color=#04FF6D>BlockModels</color>下填入<color=#FFE604>MainLayer</color>然后选中对应颜色即可。

    物体名称的颜色变化优先级低于上面组件的颜色，名称高亮可在设置中关闭
    >同时也在菜单项中分割出了不同的选项设置，这些选项设置可以帮助用户更好的自定义增强显示的内容。
    >在启用Hierarchy时读取一次配置信息。我们尽可能的只在开启使用时读取一次配置，后续的更改就尽可能的不让配置实时修改，因为这会消耗掉一部分性能。
    也就是说，<color=#FF3300>尽可能的不要开启配置设置中的内容！！！</color>";

    private static GUIStyle style;

    private void Awake()
    {
        style = new GUIStyle("label");
        style.fontSize = 16;
        style.richText = true;
        minSize = new Vector2(45 * style.fontSize, 50);
    }

    private void OnGUI()
    {
        GUILayout.Label(info, style);
    }
}
#endregion