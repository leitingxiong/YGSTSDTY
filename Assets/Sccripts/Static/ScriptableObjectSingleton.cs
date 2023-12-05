using UnityEditor;
using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T so_Instance;
    public static T Instance
    {
        get
        {
            if (so_Instance == null)
            {
                //查找资源
                string[] findAssets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
                if (findAssets == null || findAssets.Length == 0)
                    Debug.LogError($"请先创建一个类型为： {typeof(T)} 的ScriptableObject");
                else if (findAssets.Length > 1)
                    Debug.LogError($"类型为： {typeof(T)}的ScriptableObject 在项目中存在多个");
                else
                    so_Instance = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(findAssets[0]));
            }
            return so_Instance;
        }
    }
}
