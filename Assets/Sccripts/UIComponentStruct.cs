using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentStruct : MonoBehaviour
{
    //当前操作的对象
    private static GameObject CurGo;
    //后缀对应的组件类型
    public static Dictionary<string, string> typeMap = new Dictionary<string, string>()
    {
        { "sp", typeof(Sprite).Name },
        { "txt", typeof(Text).Name },
        { "btn", typeof(Button).Name },
        { "go", typeof(GameObject).Name},
    };

    //脚本模版
    private static UIStructTemplate info;

    //在Project窗口下，选中要导出的界面，然后点击GameObject/导出脚本
    [MenuItem("GameObject/导出脚本")]
    public static void CreateSpriteAction()
    {
        GameObject[] gameObjects = Selection.gameObjects;
        //保证只有一个对象
        if (gameObjects.Length == 1)
        {
            info = new UIStructTemplate();
            CurGo = gameObjects[0];
            ReadChild(CurGo.transform);
            info.classname = CurGo.name + "UIPanel";
            info.WtiteClass();
            info = null;
            CurGo = null;
        }
        else
        {
            EditorUtility.DisplayDialog("警告", "你只能选择一个GameObject", "确定");
        }
    }
    //遍历所有子对象，GetChild方法只能获取第一层子对象。
    public static void ReadChild(Transform tf)
    {
        foreach (Transform child in tf)
        {
            string[] typeArr = child.name.Split('_');
            if (typeArr.Length > 1)
            {
                string typeKey = typeArr[typeArr.Length - 1];
                if (typeMap.ContainsKey(typeKey))
                {
                    info.evenlist.Add(new UIInfo(child.name, typeKey, buildGameObjectPath(child).Replace(CurGo.name + "/", "")));
                }

            }
            if (child.childCount > 0)
            {
                ReadChild(child);
            }
        }
    }
    //获取路径，这个路径是带当前对象名的，需要用Replace替换掉头部
    private static string buildGameObjectPath(Transform obj)
    {
        var buffer = new StringBuilder();

        while (obj != null)
        {
            if (buffer.Length > 0)
                buffer.Insert(0, "/");
            buffer.Insert(0, obj.name);
            obj = obj.parent;
        }
        return buffer.ToString();
    }
}


//导出脚本的模版
public class UIStructTemplate
{
    public string classname;
    public string template = @"
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class @ClassName 
{   
    @fields
}
";
    //缓存的所有子对象信息
    public List<UIInfo> evenlist = new List<UIInfo>();
    /// <summary>
    /// 把拼接好的脚本写到本地。
    /// （自己可以个窗口支持改名和选择路径，真实工程里是带这些功能的）
    /// </summary>
    public void WtiteClass()
    {
        bool flag = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(flag, throwOnInvalidBytes);
        bool append = false;
        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + classname + ".cs", append, encoding);
        writer.Write(GetClasss());
        writer.Close();
        AssetDatabase.Refresh();
    }
    //脚本拼接
    public string GetClasss()
    {
        StringBuilder fields = new StringBuilder();
        for (int i = 0; i < evenlist.Count; i++)
        {
            fields.AppendLine("\t" + evenlist[i].field);
        }
        template = template.Replace("@ClassName", classname).Trim();
        template = template.Replace("@fields", fields.ToString()).Trim();
        return template;
    }
}

//子对象信息
public struct UIInfo
{
    public string field;
    public string body1;
    public string body2;
    public UIInfo(string name, string typeKey, string path)
    {
        field = string.Format("public {0} {1};", UIComponentStruct.typeMap[typeKey], name);
        if (typeKey == "go")
        {
            body1 = string.Format("{0} = viewGO.transform.Find(\"{1}\").gameObject;", name, path, UIComponentStruct.typeMap[typeKey]);
        }
        else
        {
            body1 = string.Format("{0} = viewGO.transform.Find(\"{1}\").GetComponent<{2}>();", name, path, UIComponentStruct.typeMap[typeKey]);
        }
        body2 = string.Format("{0} = null;", name);
    }
}