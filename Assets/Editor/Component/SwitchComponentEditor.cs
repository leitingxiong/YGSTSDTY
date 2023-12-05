using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SwitchComponentEditor : Editor
{
    #region 替换Text或TextMeshPro组件
    #region Property
    private const string MainFontPath = "Assets/Res/Fonts/Main.ttf";
    private const string ArtFontPath = "Assets/Res/Fonts/Art.ttf";
    //Text Mesh Pro的字体资源
    private const string TMPMainFontPath = "Assets/Res/Fonts/TMPMain/MainSDF.asset";
    private const string TMPArtFontPath = "Assets/Res/Fonts/TMPArt/ArtSDF.asset";
    #endregion

    //[MenuItem("Tools/UI/Text/选中的预制将TMP转Text")]
    [MenuItem("GameObject/UI/SwitchComponent/TMP转Text", priority = 200)]
    public static void TMPToText()
    {
        Font mainFont = AssetDatabase.LoadAssetAtPath<Font>(MainFontPath);
        Font artFont = AssetDatabase.LoadAssetAtPath<Font>(ArtFontPath);
        GameObject[] selectObjects = Selection.gameObjects;
        foreach (GameObject theObject in selectObjects)
        {
            TextMeshProUGUI[] allTMP = theObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            if (allTMP.Length == 0)
            {
                EditorUtility.DisplayDialog("提示", theObject.name + "预制件下没有TMP组件", "确定");
                return;
            }
            else 
            {
                foreach (TextMeshProUGUI tmp in allTMP)
                {
                    GameObject tmpObj = tmp.gameObject;
                    string tmpObjName = tmpObj.name;
                    if (tmpObjName.Contains("enhanceTMP"))
                    {
                        string replaceName = tmpObjName.Replace("enhanceTMP", "enhanceText");
                        tmpObj.name = replaceName;
                    }

                    TMP_FontAsset tmpFontAsset = tmp.font;
                    string tmpText = tmp.text;
                    float tmpFontSize = tmp.fontSize;
                    //string tmpLangKey = tmp.languageKey;
                    Color tmpColor = tmp.color;
                    TextAlignmentOptions tmpAlignment = tmp.alignment;

                    TextAnchor textAnchor = TextAnchor.MiddleCenter;
                    switch (tmpAlignment)
                    {
                        case TextAlignmentOptions.Bottom:
                            textAnchor = TextAnchor.MiddleRight;
                            break;
                        case TextAlignmentOptions.BottomLeft:
                            textAnchor = TextAnchor.LowerLeft;
                            break;
                        case TextAlignmentOptions.BottomRight:
                            textAnchor = TextAnchor.LowerRight;
                            break;
                        case TextAlignmentOptions.Top:
                            textAnchor = TextAnchor.MiddleLeft;
                            break;
                        case TextAlignmentOptions.TopLeft:
                            textAnchor = TextAnchor.UpperLeft;
                            break;
                        case TextAlignmentOptions.TopRight:
                            textAnchor = TextAnchor.UpperRight;
                            break;
                    }

                    DestroyImmediate(tmp, true);

                    Text aorText = tmpObj.AddComponent<Text>();
                    if (tmpFontAsset.name == "MainSDF")
                        aorText.font = mainFont;
                    else if (tmpFontAsset.name == "ArtSDF")
                        aorText.font = artFont;
                    aorText.text = tmpText;
                    aorText.fontSize = Convert.ToInt32(tmpFontSize);
                    //aorText.languageKey = tmpLangKey;
                    aorText.color = tmpColor;
                    aorText.alignment = textAnchor;
                    aorText.raycastTarget = false;

                    EditorUtility.SetDirty(theObject);
                }
            }
        }
        //将所有未保存的资源更改写入磁盘
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("提示", "转换成功", "确定");
    }

    //[MenuItem("Tools/UI/Text/选中的预制将Text转TMP")]
    [MenuItem("GameObject/UI/SwitchComponent/Text转TMP", priority = 200)]
    public static void TextToTMP()
    {
        var mainFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(TMPMainFontPath);
        var artFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(TMPArtFontPath);
        GameObject[] selectObjects = Selection.gameObjects;
        foreach (GameObject theObject in selectObjects)
        {
            Text[] allText = theObject.GetComponentsInChildren<Text>(true);
            if (allText.Length == 0)
            {
                EditorUtility.DisplayDialog("提示", theObject.name + "预制件下没有Text组件", "确定");
                return;
            }
            else 
            {
                foreach (Text text in allText)
                {
                    GameObject textObj = text.gameObject;
                    string textObjName = textObj.name;
                    if (textObjName.Contains("enhanceText"))
                    {
                        string replaceName = textObjName.Replace("enhanceText", "enhanceTMP");
                        textObj.name = replaceName;
                    }

                    Font textFont = text.font;
                    string textText = text.text;
                    int tmpFontSize = text.fontSize;
                    //string tmpLangKey = text.languageKey;
                    Color tmpColor = text.color;
                    TextAnchor textAlignment = text.alignment;
                    TextAlignmentOptions tmpAnchor;
                    switch (textAlignment)
                    {
                        case TextAnchor.UpperLeft:
                            tmpAnchor = TextAlignmentOptions.TopLeft;
                            break;
                        case TextAnchor.UpperCenter:
                            tmpAnchor = TextAlignmentOptions.Top;
                            break;
                        case TextAnchor.UpperRight:
                            tmpAnchor = TextAlignmentOptions.TopRight;
                            break;
                        case TextAnchor.MiddleLeft:
                            tmpAnchor = TextAlignmentOptions.Left;
                            break;
                        case TextAnchor.MiddleCenter:
                            tmpAnchor = TextAlignmentOptions.Center;
                            break;
                        case TextAnchor.MiddleRight:
                            tmpAnchor = TextAlignmentOptions.Right;
                            break;
                        case TextAnchor.LowerLeft:
                            tmpAnchor = TextAlignmentOptions.BottomLeft;
                            break;
                        case TextAnchor.LowerCenter:
                            tmpAnchor = TextAlignmentOptions.Bottom;
                            break;
                        case TextAnchor.LowerRight:
                            tmpAnchor = TextAlignmentOptions.BottomRight;
                            break;
                        default:
                            tmpAnchor = TextAlignmentOptions.Center;
                            break;
                    }

                    DestroyImmediate(text, true);

                    TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
                    if (tmp == null)
                    {
                        tmp = textObj.AddComponent<TextMeshProUGUI>();
                    }

                    if (textFont.name == "Art")
                        tmp.font = artFont;
                    else
                        tmp.font = mainFont;

                    tmp.text = textText;
                    tmp.fontSize = tmpFontSize;
                    //tmp.languageKey = tmpLangKey;
                    tmp.color = tmpColor;
                    tmp.alignment = tmpAnchor;
                    tmp.raycastTarget = false;

                    EditorUtility.SetDirty(theObject);
                }
            }
        }
        //将所有未保存的资源更改写入磁盘
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("提示", "转换成功", "确定");
    }
    #endregion
}