using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CheckUIRaycastTarget : EditorWindow
{
    private static CheckUIRaycastTarget Instance = null;
    /// <summary>
    /// Graphic数组
    /// </summary>
    private MaskableGraphic[] graphicsArray;
    /// <summary>
    /// 是否隐藏不带有raycastTarget的对象
    /// </summary>
    private bool hideWithoutRaycastTarget = false;
    /// <summary>
    /// 是否额外绘制当前选中的对象
    /// </summary>
    private bool extraDrawSelect = false;
    /// <summary>
    /// 显示边框
    /// </summary>
    private bool showBorder = true;
    /// <summary>
    /// 边框颜色（默认红色）
    /// </summary>
    private Color borderColor = Color.red;
    private Vector2 scrollViewPosition = Vector2.zero;

    public bool ExtraDrawSelect
    {
        get { return extraDrawSelect; }
    }

    [MenuItem("游戏工具/检测UI中的RaycastTarget")]
    private static void OpenWindow()
    {
        //如果左操作数的值不为 null，则?? 返回该值；否则，它会计算右操作数并返回其结果。
        Instance = Instance ?? EditorWindow.GetWindow<CheckUIRaycastTarget>("检测RaycastTarget");
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
        if (!Selection.activeGameObject)
        {
            return;
        }

        GameObject go = Selection.activeGameObject;
        //获取选中对象下，所有包含MaskableGraphic的子节点
        graphicsArray = go.GetComponentsInChildren<MaskableGraphic>(true);

        GUILayout.Space(10.0f);
        //在该元素内呈现的所有控件都将依次水平放置。using 语句意味着不需要 BeginHorizontal 和 EndHorizontal。
        using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
        {
            showBorder = EditorGUILayout.Toggle("Show Gizmos", showBorder, GUILayout.Width(200.0f));
            borderColor = EditorGUILayout.ColorField(borderColor);
        }

        GUILayout.BeginHorizontal();
        hideWithoutRaycastTarget = EditorGUILayout.Toggle("HideWithoutRaycastTarget", hideWithoutRaycastTarget);
        if (GUILayout.Button("Remove All Excep Button", GUILayout.MaxWidth(200)))
        {
            for (int i = 0, iMax = graphicsArray.Length; i < iMax; ++i)
            {
                MaskableGraphic graphic = graphicsArray[i];
                if (graphic.GetComponent<Button>() == null)
                {
                    graphic.raycastTarget = false;
                }
            }
        }
        GUILayout.EndHorizontal();
        extraDrawSelect = EditorGUILayout.Toggle("额外绘制选中", extraDrawSelect);


        GUILayout.Space(10.0f);
        Rect rect = GUILayoutUtility.GetLastRect();
        GUI.color = new Color(0.0f, 0.0f, 0.0f, 0.25f);
        GUI.DrawTexture(new Rect(0.0f, rect.yMin + 6.0f, Screen.width, 4.0f), EditorGUIUtility.whiteTexture);
        GUI.DrawTexture(new Rect(0.0f, rect.yMin + 6.0f, Screen.width, 1.0f), EditorGUIUtility.whiteTexture);
        GUI.DrawTexture(new Rect(0.0f, rect.yMin + 9.0f, Screen.width, 1.0f), EditorGUIUtility.whiteTexture);
        GUI.color = Color.white;

        //开启自动布局滚动视图,using 语句意味着不需要 BeginScrollView 和 EndScrollView。
        using (GUILayout.ScrollViewScope scrollViewScope = new GUILayout.ScrollViewScope(scrollViewPosition))
        {
            scrollViewPosition = scrollViewScope.scrollPosition;

            for (int i = 0, length = graphicsArray.Length; i < length; ++i)
            {
                MaskableGraphic graphic = graphicsArray[i];
                //是否显示不带有raycastTarget的对象
                if (!hideWithoutRaycastTarget || graphic.raycastTarget)
                {
                    DrawElement(graphic);
                }
            }
        }

        Repaint();
    }

    /// <summary>
    /// 绘制元素
    /// </summary>
    /// <param name="graphic"></param>
    private void DrawElement(MaskableGraphic graphic)
    {
        using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
        {
            Undo.RecordObject(graphic, "Modify RaycastTarget");

            graphic.raycastTarget = EditorGUILayout.Toggle(graphic.raycastTarget, GUILayout.Width(20));

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField(graphic, typeof(MaskableGraphic), true);
            EditorGUI.EndDisabledGroup();
        }
    }

    /// <summary>
    /// 使用了DrawGizmo特性，特性中的参数表示吧不管物体有没有被选中，都绘制Gizmos
    /// </summary>
    /// <param name="source"></param>
    /// <param name="gizmoType"></param>
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    private static void DrawGizmos(MaskableGraphic source, GizmoType gizmoType)
    {
        if (Instance != null && Instance.showBorder == true && source.raycastTarget == true)
        {
            Vector3[] corners = new Vector3[4];
            //获取RectTransform组件在世界空间中的各个角坐标。（返回数组是顺时针的：从左下开始，旋转到左上， 然后到右上，最后到右下）
            source.rectTransform.GetWorldCorners(corners);
            //设置Gizmos的颜色值
            Gizmos.color = Instance.borderColor;

            for (int i = 0; i < 4; ++i)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            }

            if (Selection.activeGameObject == source.gameObject && Instance.ExtraDrawSelect)
            {
                Gizmos.DrawLine(corners[0], corners[2]);
                Gizmos.DrawLine(corners[1], corners[3]);
            }
        }
        //重新绘制每一个处于打开状态的SceneView
        SceneView.RepaintAll();
    }
}