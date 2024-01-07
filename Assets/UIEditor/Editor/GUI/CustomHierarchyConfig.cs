using UnityEngine;
using System;
using UnityEditor;

/// <summary>
/// 自定义区块字体的锚点枚举
/// </summary>
public enum CustomFontAnchor
{
    Left = TextAnchor.MiddleLeft,
    Center = TextAnchor.MiddleCenter,
}

[CreateAssetMenu(menuName = "配置创建/创建自定义Hierarchy配置", fileName = "CustomHierarchyConfig")]
public class CustomHierarchyConfig : ScriptableObject
{
    /// <summary>
    /// 图标Rect大小(图标尺寸再-2)
    /// </summary>
    [Minimal("图标大小",18)]
    [Space()]
    public int iconRectSize = 18;

    /// <summary>
    /// 第一个(最右侧)图标离右侧距离
    /// </summary>
    [Minimal("最右侧图标离右侧距离", 20)]
    [Space()]
    public int firstSpacing = 20;

    /// <summary>
    /// 图标之间间距
    /// </summary>
    [Minimal("图标间距", 20)]
    [Space()]
    public int iconSpacing = 26;

    /// <summary>
    /// insID距离右侧距离
    /// </summary>
    [Label("InsID距右侧距离")]
    [Space()]
    public float insIDRightMargin = 200;

    /// <summary>
    /// 图标最小显示宽度
    /// </summary>
    [Minimal("图标最小显示宽度", 150)]
    [Space()]
    public float minIconShowWidth = 150;

    /// <summary>
    /// 最小显示宽度
    /// </summary>
    [Minimal("最小显示宽度", 130)]
    [Space()]
    public float minShowWidth = 130;

    /// <summary>
    /// 字体颜色偏移X
    /// </summary>
    [Label("字体颜色偏移X"), Tooltip("默认值为18"), Space()]
    public float fontColorOffsetX = 18f;

    /// <summary>
    /// 字体颜色Y偏移量
    /// </summary>
    [Label("字体颜色Y偏移量"), Tooltip("默认值为0"), Space()]
    public float fontColorOffsetY = .0f;

    [Label("颜色区块是否忽略空格")]
    public bool ignoreSpace = false;
    [Label("缩放为0时的字体颜色"),Tooltip("颜色清空时，则是使用代码中设置的默认颜色")]
    public Color scaleZeroColor = Color.clear;

    [Label("分割线厚度")]
    public float dividerThickness = 1;
    [Label("分割线颜色")]
    public Color dividerColor = new Color(0,0,0,0.4f);

    #region 树形结构线
    [Header("树形结构颜色"), Label("树形结构基础颜色")]
    public Color treeLineBaseColor = Color.white;
    public Color[] treeLineLevelColor = {
        new Color(0.93f, 1, 0.42f, 1),new Color(1, 0.75f, 0.42f, 1),new Color(1, 0.46f, 0.31f, 1),new Color(1, 0.35f, 0.34f, 1),
        new Color(1, 0.1f, 0.9f, 1),new Color(0.85f, 0.5f,1, 1),new Color(0.45f, 0.5f,1, 1),new Color(0.1f, 0.6f,1, 1),
        new Color(0.1f, 0.8f,1, 1),new Color(0, 1, 0.65f, 1),new Color(0.6f, 0.7f, 0.2f, 1),new Color(0.8f, 0.4f, 0, 1),
        new Color(0.63f, 0.22f, 0, 1),new Color(0.83f, 0.1f, 0, 1),new Color(0.93f, 0.46f, 0.48f, 1),
    };
    [Minimal("最多显示几层树形线", 1)]
    public int treeLineMaxShow = 5;
    [Minimal("结构线间距", 0)]
    public float treeLineSpacing = 14;
    [Minimal("结构线宽度", 1)]
    public float treeLineThickness = 2;
    #endregion


    #region 区块颜色
    /// <summary>
    /// 区块颜色模块
    /// </summary>
    [Header("区块颜色")]
    public AreaColorModel[] blockModels =
    {
        new AreaColorModel("MainLayer", Color.yellow, FontStyle.BoldAndItalic),
        new AreaColorModel("NormalLayer", Color.cyan, FontStyle.BoldAndItalic),
        new AreaColorModel("ConstUILayer", new Color(0.5454343f,0.5745714f,1,1), FontStyle.BoldAndItalic),
        new AreaColorModel("VolumeLayer", new Color(1,0.3632075f,0.5598798f,1), FontStyle.BoldAndItalic),
        new AreaColorModel("EnvironmentLayer", new Color(0.5f,0.9f,0.2f,1), FontStyle.BoldAndItalic),
        new AreaColorModel("MainCamera", new Color(1f,0f,0.07f,1), FontStyle.BoldAndItalic),
        new AreaColorModel("Launcher", new Color(1f,0.4661931f,0,1), FontStyle.BoldAndItalic),
    };

    /// <summary>
    /// 区域颜色模块
    /// </summary>
    [Serializable]
    public class AreaColorModel
    {
        [Label("目标物体名")]
        public string targetName;
        [Label("区块颜色")]
        public Color blockColor;
        [Label("字体样式")]
        public FontStyle fontStyle;
        [Label("自定义区块字体颜色")]
        public Color customAreaTextColor;
        [Label("自定义对齐方式")]
        public CustomFontAnchor customAreaTextAlignment;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AreaColorModel()
        {
            this.targetName = "";
            this.blockColor = Color.white;
            this.fontStyle = FontStyle.Normal;
            this.customAreaTextColor = Color.clear;
            this.customAreaTextAlignment = CustomFontAnchor.Center;
        }

        public AreaColorModel(string inputName, Color inputColor) 
            : this(inputName, inputColor, FontStyle.Normal, Color.clear, CustomFontAnchor.Center)
        {}

        public AreaColorModel(string inputName, Color inputColor, FontStyle customFontStyle) 
            : this(inputName, inputColor, customFontStyle, Color.clear, CustomFontAnchor.Center)
        {}

        public AreaColorModel(string inputName, Color inputColor, FontStyle customFontStyle, Color customAreaTextColor) 
            : this(inputName, inputColor, customFontStyle, customAreaTextColor, CustomFontAnchor.Center)
        {}

        public AreaColorModel(string inputName, Color inputColor, FontStyle customFontStyle, Color customAreaTextColor, CustomFontAnchor customAreaTextAlignment)
        {
            this.targetName = inputName;
            this.blockColor = inputColor;
            this.fontStyle = customFontStyle;
            this.customAreaTextColor = customAreaTextColor;
            this.customAreaTextAlignment = customAreaTextAlignment;
        }
    }
    #endregion

    #region 设置在Inpector上显示的组件图标以及字体颜色
    /// <summary>
    /// 组件颜色高亮模块
    /// </summary>
    [Header("组件图标及颜色高亮")]
    public HighlightColorModel[] highlightModels =
    {
        new HighlightColorModel("UnityEngine",
            new HighlightColorItem[]
            { new HighlightColorItem("Canvas",Color.cyan),
              new HighlightColorItem("Camera",Color.red),
              new HighlightColorItem("SpriteRenderer",new Color(0.7254f,0.4f,0.8862f,1)),
              new HighlightColorItem("Light",new Color(0.83f,0.73f,0.1f,1)),
              new HighlightColorItem("ReflectionProbe",Color.white),
            }),
        new HighlightColorModel("UnityEngine.UI",
            new HighlightColorItem[]
            {
              new HighlightColorItem("Button",new Color(0,1,0.6f,1)),
              new HighlightColorItem("InputField",Color.white),
              new HighlightColorItem("ScrollRect",Color.white),
              new HighlightColorItem("Scrollbar",Color.white),
              new HighlightColorItem("Toggle",Color.white),
              new HighlightColorItem("ToggleGroup",Color.white),
              new HighlightColorItem("HorizontalLayoutGroup",Color.white),
              new HighlightColorItem("VerticalLayoutGroup",Color.white),
              new HighlightColorItem("RawImage",new Color(1f,0,0.866f,1)),
              new HighlightColorItem("Image",new Color(1,0.4f,0.95f,1)),
              new HighlightColorItem("Text",new Color(0.88f,0.56f,0.14f,1)),
            }),
        new HighlightColorModel("Assembly-CSharp",
            new HighlightColorItem[0]),
    };

    /// <summary>
    /// 组件颜色高亮模块
    /// </summary>
    [Serializable]
    public struct HighlightColorModel
    {
        [Label("程序集名")]
        public string assemblyName;
        public HighlightColorItem[] highlightItems;

        public HighlightColorModel(string inputName, HighlightColorItem[] inputArray) 
        {
            assemblyName = inputName;
            highlightItems = inputArray;
        }
    }

    [Serializable]
    public class HighlightColorItem
    {
        [Label("组件名")]
        public string componentName;
        [Label("名称颜色")]
        public Color color;

        public HighlightColorItem()
        {
            color = Color.white;
        }

        public HighlightColorItem(string inputName, Color inputColor)
        {
            componentName = inputName;
            color = inputColor;
        }
    }
    #endregion
}