using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Reflection;

public class UIAdaptationTool : EditorWindow
{
    private static UIAdaptationTool Instance = null;
    /// <summary>
    /// 当前滚动位置
    /// </summary>
    private Vector2 scrollViewPosition = Vector2.zero;
    private object gameViewSizesInstance;
    private MethodInfo gameViewGetGroup;
    private int gameViewProfilesCount;
    /// <summary>
    /// 显示的文本
    /// </summary>
    private string[] displayTextArray;

    [MenuItem("游戏工具/分辨率调整工具", false, 101)]
    public static void OpenWindow()
    {
        Instance = Instance ?? EditorWindow.GetWindow<UIAdaptationTool>("调整分辨率");
        Instance.Show();
    }

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        Instance = null;
    }

    void OnGUI()
    {
        //开启自动布局滚动视图,using 语句意味着不需要 BeginScrollView 和 EndScrollView。
        using (GUILayout.ScrollViewScope scrollViewScope = new GUILayout.ScrollViewScope(scrollViewPosition))
        {
            scrollViewPosition = scrollViewScope.scrollPosition;
            EditorGUILayout.BeginVertical();
            DisplayAdaptationButton();
            EditorGUILayout.EndVertical();
        }
    }

    /// <summary>
    /// 显示分辨率选择按钮
    /// </summary>
    private void DisplayAdaptationButton()
    {
        InitUIAdaptation();
        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        for (int i = 0; i < gameViewProfilesCount; i++)
        {
            int index = i;
            string screenResolutionText = displayTextArray[i];

            if (GUILayout.Button(screenResolutionText, GUILayout.Width(160), GUILayout.Height(20)))
            {
                SetSize(index);
            }

            if ((i + 1) % 2 == 0 || i + 1 == gameViewProfilesCount)
            {
                EditorGUILayout.EndHorizontal();
                if (i + 1 < gameViewProfilesCount)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                }
            }
        }
    }

    /// <summary>
    /// 初始化屏幕适配
    /// </summary>
    private void InitUIAdaptation()
    {
        if (gameViewSizesInstance == null)
        {
            Assembly assembly = typeof(Editor).Assembly;
            Type sizesType = assembly.GetType("UnityEditor.GameViewSizes");
            Type singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
            PropertyInfo instanceProperty = singleType.GetProperty("instance");
 
            //在公共方法、非公共方法、实例中搜索名为GetGroup的方法
            gameViewGetGroup = sizesType.GetMethod("GetGroup",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            gameViewSizesInstance = instanceProperty.GetValue(null, null);
            GameViewSizeGroupType gameViewSizeGroupType = GameViewSizeGroupType.Android;
#if UNITY_ANDROID
                gameViewSizeGroupType = GameViewSizeGroupType.Android;
#elif UNITY_IPHONE
                gameViewSizeGroupType = GameViewSizeGroupType.iOS;
#elif UNITY_STANDALONE_WIN
            gameViewSizeGroupType = GameViewSizeGroupType.Standalone;
#endif
            var group = GetGroup(gameViewSizeGroupType);
            MethodInfo displayTextsContent = group.GetType().GetMethod("GetDisplayTexts");
            displayTextArray = displayTextsContent.Invoke(group, null) as string[];
            gameViewProfilesCount = displayTextArray.Length;

            for (int i = 0; i < gameViewProfilesCount; i++)
            {
                int index = i;
                string display = displayTextArray[i];
                //获取第一个（的索引下标，如果未在此实例中找到该字符或字符串，则此方法返回 -1
                int prefix = display.IndexOf('(');
                if (prefix != -1)
                    //截取头部到pren-1前面的字符串
                    display = display.Substring(0, prefix - 1);
                displayTextArray[i] = display;
            }
        }
    }

    /// <summary>
    /// 设置分辨率
    /// </summary>
    /// <param name="index"></param>
    private void SetSize(int index)
    {
        Assembly assembly = typeof(EditorWindow).Assembly;
        Type gameViewType = assembly.GetType("UnityEditor.GameView");
        EditorWindow gameWindow = GetWindow(gameViewType);
        //在公共方法、非公共方法、实例中搜索名为SizeSelectionCallback的方法
        MethodInfo SizeSelectionCallback = gameViewType.GetMethod("SizeSelectionCallback",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        SizeSelectionCallback.Invoke(gameWindow, new object[] { index, null });
    }

    /// <summary>
    /// 获取界面的分辨率组
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static object GetGroup(GameViewSizeGroupType type)
    {
        return Instance.gameViewGetGroup.Invoke(Instance.gameViewSizesInstance, new object[] { (int)type });
    }

    public static int FindSize(GameViewSizeGroupType sizeGroupType, int width, int height)
    {
        object group = GetGroup(sizeGroupType);
        Type groupType = group.GetType();
        MethodInfo getBuiltinCount = groupType.GetMethod("GetBuiltinCount");
        MethodInfo getCustomCount = groupType.GetMethod("GetCustomCount");
        int sizesCount = (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
        MethodInfo getGameViewSize = groupType.GetMethod("GetGameViewSize");
        Type gameViewSizeType = getGameViewSize.ReturnType;
        //获取width属性
        PropertyInfo widthProperty = gameViewSizeType.GetProperty("width");
        //获取height属性
        PropertyInfo heightProperty = gameViewSizeType.GetProperty("height");
        object[] indexValue = new object[1];
        for (int i = 0; i < sizesCount; i++)
        {
            indexValue[0] = i;
            var size = getGameViewSize.Invoke(group, indexValue);
            int sizeWidth = (int)widthProperty.GetValue(size, null);
            int sizeHeight = (int)heightProperty.GetValue(size, null);
            if (sizeWidth == width && sizeHeight == height)
                return i;
        }
        return -1;
    }
}