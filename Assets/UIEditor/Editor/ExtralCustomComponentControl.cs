using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using GameUtil;

public class ExtralCustomComponentControl
{
    #region 创建组件
    /// <summary>
    /// 创建双击组件
    /// </summary>
    public static GameObject CreateDoubleClickButton()
    {
        GameObject dualClickButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        dualClickButton.name = "DoubleClickButton";
        dualClickButton.transform.Find("Text").GetComponent<Text>().text = "DoubleClickButton";
        Object.DestroyImmediate(dualClickButton.GetComponent<Button>());
        dualClickButton.AddComponent<DoubleClickButton>();
        return dualClickButton;
    }

    /// <summary>
    /// 创建长按组件
    /// </summary>
    public static GameObject CreateLongClickButton()
    {
        GameObject longPressButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        longPressButton.name = "LongClickButton";
        longPressButton.transform.Find("Text").GetComponent<Text>().text = "LongClickButton";
        Object.DestroyImmediate(longPressButton.GetComponent<Button>());
        longPressButton.AddComponent<LongClickButton>();
        return longPressButton;
    }

    /// <summary>
    /// 重写Image组件的创建方法
    /// </summary>
    [MenuItem("GameObject/UI/Image")]
    static void CreatImage(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("Image", typeof(Image));
        go.GetComponent<Image>().raycastTarget = false;
        ExtralEditorUtility.SetUIElementRoot(go, menuCommand);
    }

    //重写Text创建方法
    [MenuItem("GameObject/UI/Text")]
    static void CreatText(MenuCommand menuCommand)
    {
        //新建Text对象  
        GameObject go = new GameObject("Text", typeof(Text));
        //将raycastTarget置为false  
        go.GetComponent<Text>().raycastTarget = false;
        //设置其父物体  
        ExtralEditorUtility.SetUIElementRoot(go, menuCommand);
    }

    //重写Raw Image创建方法
    [MenuItem("GameObject/UI/Raw Image")]
    static void CreatRawImage(MenuCommand menuCommand)
    {
        //新建Text对象  
        GameObject go = new GameObject("RawImage", typeof(RawImage));
        //将raycastTarget置为false  
        go.GetComponent<RawImage>().raycastTarget = false;
        //设置其父物体  
        ExtralEditorUtility.SetUIElementRoot(go, menuCommand);
    }
    #endregion

    #region 添加组件
    [MenuItem("GameObject/UI/Buttons/DoubleClickButton", false, 1)]
    public static void AddDoubleClickButton(MenuCommand menuCommand)
    {
        GameObject theButton = CreateDoubleClickButton();
        ExtralEditorUtility.SetUIElementRoot(theButton, menuCommand);
    }


    [MenuItem("GameObject/UI/Buttons/LongClickButton", false, 2)]
    public static void AddLongClickButton(MenuCommand menuCommand)
    {
        GameObject theButton = CreateLongClickButton();
        ExtralEditorUtility.SetUIElementRoot(theButton, menuCommand);
    }
    #endregion
}