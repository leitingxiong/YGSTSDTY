#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ReNameTool : EditorWindow
{
    private string targetName = "";
    private string startNum_str = "1";
    private int startNum_int = 0;

    #region 打开重命名窗口
    [MenuItem("游戏工具/重命名工具")]
    private static void OpenWindow()
    {
        GetWindow<ReNameTool>("重命名工具").Show();
    }
    #endregion

    private void OnGUI()
    {
        GUI.color = Color.cyan;
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        //居中对齐
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("重命名工具");

        GUI.skin.label.fontSize = 16;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.Space(20);

        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            {
                GUI.color = Color.yellow;
                GUILayout.Label("名称前缀：");
                GUILayout.FlexibleSpace();
                GUI.color = Color.white;
                targetName = GUILayout.TextField(targetName, GUILayout.Width(140));
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            {
                GUI.color = Color.yellow;
                GUILayout.Label("后缀起始数值：");
                GUILayout.FlexibleSpace();
                GUI.color = Color.white;
                startNum_str = GUILayout.TextField(startNum_str, GUILayout.Width(140));
                if (startNum_str != string.Empty) 
                {
                    try
                    {
                        startNum_int = int.Parse(startNum_str);
                    }
                    catch 
                    {
                        Debug.LogError("解析错误，请使用数值");
                        startNum_str = string.Empty;
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(30);
            //判断是否有选择对象
            bool hasObject = (Selection.objects.Length > 0);
            GUI.enabled = hasObject;
            GUILayout.FlexibleSpace();
            if (!hasObject)
            {
                GUI.color = Color.red;
                GUILayout.Button("没有选择任何对象！！！");
                GUI.color = Color.white;
            }
            else 
            {
                GUI.color = Color.green;
                if (GUILayout.Button("重命名"))
                    Rename(targetName, startNum_int);
                GUI.color = Color.white;
            }
        }
        GUILayout.EndVertical();
    }

    private void Rename(string prefixName,int suffixIndex) 
    {
        //去除头尾空白字符串
        string name = prefixName.Trim();
        int index = suffixIndex;
        if ((name + index) != string.Empty) 
        {
            Undo.RecordObjects(Selection.objects, "Cancel Rename");
            //判断是否是assets文件夹的资源
            bool isAssetObject = false;
            foreach (Object item in Selection.objects)
            {
                //获得选中对象的路径
                string assetPath = AssetDatabase.GetAssetPath(item);
                //查看路径后缀
                if (Path.GetExtension(assetPath) != "")
                {
                    //修改asset资源的名称
                    AssetDatabase.RenameAsset(assetPath, $"{name}_{index}");
                    if (!isAssetObject)
                    {
                        isAssetObject = true;
                    }
                }
                else 
                {
                    item.name = $"{name}_{index}";
                }
                index++;
            }
            //若是assets文件夹资源，则完成后保存并刷新
            if (isAssetObject) 
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif