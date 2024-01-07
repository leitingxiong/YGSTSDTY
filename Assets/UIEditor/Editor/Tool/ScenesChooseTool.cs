using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

public class ScenesChooseTool : EditorWindow
{
    private Vector2 startScrollPos = Vector2.zero;
    /// <summary>
    /// 场景名称列表
    /// </summary>
    private List<string> scenesName;
    /// <summary>
    /// 场景路径列表
    /// </summary>
    private List<string> scenesPath;
    private bool isInitOver = false;

    [MenuItem("游戏工具/场景选择工具",false,0)]
    public static void Open() 
    {
        ScenesChooseTool window = GetWindow<ScenesChooseTool>();
        window.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"))
        {
            text = "场景选择工具",
        };
        window.Init();
    }

    private void Init() 
    {
        scenesName = new List<string>();
        scenesPath = new List<string>();
        //实际遍历的路径
        List<string> paths = new List<string>()
        {
            Application.dataPath+"/Scenes",
        };

        foreach (string path in paths)
        {
            string[] files = FileUtility.GetFilesPaths(path, "*Unity");
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    scenesPath.Add(file);
                    scenesName.Add(FileUtility.GetFileName(file));
                }
            }
        }
        isInitOver = true;
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical("GroupBox");
        GUI.color = Color.green;
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("请选择场景");
        GUI.color = Color.white;

        startScrollPos = GUILayout.BeginScrollView(startScrollPos);
        if (isInitOver && scenesPath != null) 
        {
            for (int i = 0; i < scenesPath.Count; i++)
            {
                if (GUILayout.Button(scenesName[i])) 
                {
                    SceneUtility.OpenScene(scenesPath[i]);
                }
            }
        }
        GUI.skin.label.fontSize = 16;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
}
