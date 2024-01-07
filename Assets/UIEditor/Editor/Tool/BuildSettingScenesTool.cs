using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

public class BuildSettingScenesTool
{
    /// <summary>
    /// 自动化操作的场景路径
    /// </summary>
    public static readonly string _SceneDir = Application.dataPath + "/Scenes";
    public const string _AssetSceneDir = "Assets/Scenes";
    public const string _FirstSceneName = "SampleScene.unity";

    #region MenuItem
    [MenuItem("游戏工具/BuildSettings/更新BuildSettings中的Scene", false, 1)]
    public static void AutoAddScenesToBuildForEditor()
    {
        AutoAddScenesToBuild(false);
    }

    //MenuItem第二个参数是true则是给该菜单项添加一个验证，需分别标记两个函数，true标记的函数作为false标记的函数能否启用并执行的验证
    [MenuItem("游戏工具/BuildSettings/更新BuildSettings中的Scene", true, 1)]
    public static bool CheckAutoAddScenesToBuildForEditor()
    {
        return !EditorApplication.isPlaying;
    }

    [MenuItem("游戏工具/BuildSettings/更新BuildSettings中的Scene至打包状态", false, 2)]
    public static void AutoAddScenesToBuildForPackage()
    {
        AutoAddScenesToBuild(true);
    }

    [MenuItem("游戏工具/BuildSettings/更新BuildSettings中的Scene至打包状态", true, 2)]
    public static bool CheckAutoAddScenesToBuildForPackage()
    {
        return !EditorApplication.isPlaying;
    }

    [MenuItem("游戏工具/BuildSettings/清空BuildSettings中的所有场景", false, 3)]
    public static void RemoveScenesToBuildForPackage()
    {
        RemoveAllScenesToBuild();
    }

    [MenuItem("游戏工具/BuildSettings/清空BuildSettings中的所有场景", true, 3)]
    public static bool CheckRemoveScenesToBuildForPackage()
    {
        return !EditorApplication.isPlaying;
    }
    #endregion

    /// <summary>
    /// 将场景添加到BuildSetting中
    /// </summary>
    /// <param name="isPackage">是否是打包状态</param>
    public static void AutoAddScenesToBuild(bool isPackage)
    {
        //匹配目录下的所有.unity文件
        string pattern = "*.unity";
        string[] files = FileUtility.GetFilesPaths(_SceneDir, pattern);
        EditorBuildSettingsScene[] buildSettingScene = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            string name = FileUtility.GetFileName(files[i], false);
            buildSettingScene[i] = GetBuidSettingScene(name, isPackage);
        }

        //设置场景
        EditorBuildSettings.scenes = buildSettingScene;
        Debug.Log("加载BuidSettingScene完成");
    }

    /// <summary>
    /// 清空BuildSetting中的所有场景
    /// </summary>
    public static void RemoveAllScenesToBuild()
    {
        EditorBuildSettings.scenes = null;
    }

    /// <summary>
    /// 获取BuildSettingScene
    /// </summary>
    /// <param name="sceneName">场景名称（需要包含扩展名称）</param>
    /// <param name="isPackage">是否是打包状态</param>
    /// <returns></returns>
    public static EditorBuildSettingsScene GetBuidSettingScene(string sceneName, bool isPackage)
    {
        //是否在 Build Settings 窗口中启用此场景
        bool enableScene = true;
        if (isPackage)
        {
            enableScene = (sceneName == _FirstSceneName);
        }

        string sceneAssetPath = _AssetSceneDir + "/" + sceneName;
        //EditorBuildSettingsScene的资源路径，必须包含扩展名
        return new EditorBuildSettingsScene(sceneAssetPath, enableScene);
    }
}
