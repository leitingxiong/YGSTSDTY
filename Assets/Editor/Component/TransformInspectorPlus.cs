using UnityEngine;
using UnityEditor;

//CustomEditor特性表明这是一个自定义的Editor脚本
[CustomEditor(typeof(Transform))]
public class TransformInspectorPlus : Editor
{
    /// <summary>
    /// 显示的是否是世界坐标
    /// </summary>
    private bool useWorldSystem = false;
    /// <summary>
    /// 是否使用统一的缩放比
    /// </summary>
    private bool uniformScale = true;

    private Transform theTarget;

    private void Awake()
    {
        theTarget = (Transform)this.target;
        uniformScale = theTarget.localScale.x == theTarget.localScale.y && theTarget.localScale.y == theTarget.localScale.z;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        {
            #region 上方的功能选项
            GUILayout.BeginHorizontal("Toolbar");
            {
                useWorldSystem = GUILayout.Toggle(useWorldSystem, "使用世界坐标", "toolbarbutton");
                uniformScale = GUILayout.Toggle(uniformScale, "统一缩放比", "toolbarbutton");
            }
            #endregion

            GUILayout.EndHorizontal();
            Vector3 vector;
            if (useWorldSystem)
            {
                GUILayout.BeginHorizontal();
                {
                    vector = EditorGUILayout.Vector3Field("Position:", theTarget.position);
                    if (vector != theTarget.position)
                    {
                        Undo.RecordObject(theTarget, "Modify Position");
                        theTarget.position = vector;
                    }

                    GUI.color = Color.green;
                    if (GUILayout.Button("Reset To 0", "toolbarbutton", GUILayout.MaxWidth(80)))
                    {
                        Undo.RecordObject(theTarget, "Modify Position");
                        theTarget.position = Vector3.zero;
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    vector = EditorGUILayout.Vector3Field("EulerAngles:", theTarget.eulerAngles);
                    if (vector != theTarget.eulerAngles)
                    {
                        Undo.RecordObject(target, "Modify Rotation");
                        theTarget.eulerAngles = vector;
                    }

                    GUI.color = Color.green;
                    if (GUILayout.Button("Reset To 0", "toolbarbutton", GUILayout.MaxWidth(80)))
                    {
                        Undo.RecordObject(theTarget, "Modify Rotation");
                        theTarget.rotation = Quaternion.identity;
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();
                {
                    vector = EditorGUILayout.Vector3Field("Local Position:", theTarget.localPosition);
                    if (vector != theTarget.localPosition)
                    {
                        Undo.RecordObject(target, "Modify Position");
                        theTarget.localPosition = vector;
                    }

                    GUI.color = Color.green;
                    if (GUILayout.Button("Reset To 0", "toolbarbutton", GUILayout.MaxWidth(80)))
                    {
                        Undo.RecordObject(theTarget, "Modify Position");
                        theTarget.localPosition = Vector3.zero;
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    vector = EditorGUILayout.Vector3Field("Local EulerAngles:", theTarget.localEulerAngles);
                    if (vector != theTarget.localEulerAngles)
                    {
                        Undo.RecordObject(theTarget, "Modify Rotation");
                        theTarget.localEulerAngles = vector;
                    }

                    GUI.color = Color.green;
                    if (GUILayout.Button("Reset To 0", "toolbarbutton", GUILayout.MaxWidth(80)))
                    {
                        Undo.RecordObject(theTarget, "Modify Rotation");
                        theTarget.localRotation = Quaternion.identity;
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            {
                if (uniformScale)
                {
                    float v = EditorGUILayout.FloatField("Scale:", theTarget.localScale.x);
                    if (theTarget.localScale.x != v)
                    {
                        Undo.RecordObject(theTarget, "ScaleIsUniform");
                        theTarget.localScale = new Vector3(v, v, v);
                    }
                }
                else
                {
                    Vector3 tmporary = EditorGUILayout.Vector3Field("Scale:", theTarget.localScale);
                    if (tmporary != theTarget.localScale)
                    {
                        Undo.RecordObject(theTarget, "ScaleIsUniform");
                        theTarget.localScale = tmporary;
                    }
                }

                GUI.color = Color.green;
                if (GUILayout.Button("To 1", "toolbarbutton", GUILayout.MaxWidth(40)))
                {
                    Undo.RecordObject(theTarget, "scaleIsUniform to one");
                    theTarget.localScale = Vector3.one;
                }
                if (GUILayout.Button("To 0", "toolbarbutton", GUILayout.MaxWidth(40)))
                {
                    Undo.RecordObject(theTarget, "scaleIsUniform to zero");
                    theTarget.localScale = Vector3.zero;
                }
                GUI.color = Color.white;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

        }
        GUILayout.EndVertical();

        #region 下方的按钮
        GUI.color = Color.yellow;
        //设置按钮中的字体对齐方式为居中对齐。
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("复制世界坐标", GUILayout.MaxWidth(300)))
        {
            //使用GUIUtility.systemCopyBuffer获取系统粘贴板的权限，实现“复制和粘贴”功能
            GUIUtility.systemCopyBuffer = theTarget.transform.position.ToString();
        }
        if (GUILayout.Button("复制世界空间下的欧拉角", GUILayout.MaxWidth(300)))
        {
            GUIUtility.systemCopyBuffer = theTarget.transform.eulerAngles.ToString();
        }
        if (GUILayout.Button("复制局部坐标", GUILayout.MaxWidth(300)))
        {
            GUIUtility.systemCopyBuffer = theTarget.transform.localPosition.ToString();
        }
        if (GUILayout.Button("复制局部空间下的欧拉角", GUILayout.MaxWidth(300)))
        {
            GUIUtility.systemCopyBuffer = theTarget.transform.localEulerAngles.ToString();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUI.color = Color.white;
        #endregion
    }
}