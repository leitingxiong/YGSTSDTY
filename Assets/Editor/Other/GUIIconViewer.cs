using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
public class GUIIconViewer : EditorWindow
{
    private static List<GUIContent> icons;

    private Vector2 scrollPos;

    [MenuItem("游戏工具/GUIInnerArt/GUIIconViewer", false, 100)]
    private static void OpenWindow()
    {
        icons = new List<GUIContent>();

        GetWindow<GUIIconViewer>("图标");

        Texture2D[] textures = Resources.FindObjectsOfTypeAll<Texture2D>();
        foreach (Texture2D texture in textures)
        {
            GUIContent icon = EditorGUIUtility.IconContent(texture.name, $"|{texture.name}");
            if (icon != null && icon.image != null) icons.Add(icon);
        }
    }

    void DrawIcon()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < icons.Count; i += 35)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 35; j++)
            {
                if (i + j < icons.Count)
                    GUILayout.Button(icons[i + j], GUILayout.Width(35), GUILayout.Height(35));
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }

    void OnGUI()
    {
        DrawIcon();
    }
}
