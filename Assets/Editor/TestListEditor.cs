using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

public class TestListEditor : Editor
{
    //定义ReorderableList
    private ReorderableList colorList;
    private void OnEnable()
    {
        /*
         * 第三个参数控制是否可拖曳排序
         * 第四个参数控制是否显示Header
         * 第五个参数控制是否显示添加元素按钮
         * 第六个参数控制是否显示移除元素按钮
         */
        colorList = new ReorderableList(serializedObject, serializedObject.FindProperty("colorList"), true, true, true, true);
        //绘制元素
        colorList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty itemData = colorList.serializedProperty.GetArrayElementAtIndex(index);
            //设置元素绘制的原点Y坐标
            rect.y += 2;
            //设置绘制的高度
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, itemData, GUIContent.none);
        };
        //绘制表头
        colorList.drawHeaderCallback = (Rect rect) =>
        {
            //自定义列表表头的字符串
            GUI.Label(rect, "颜色");
        };
        //当移除元素时回调
        colorList.onRemoveCallback = (ReorderableList list) =>
        {
            //弹出一个对话框
            if (EditorUtility.DisplayDialog("警告", "是否确定删除该颜色", "是", "否"))
            {
                //当点击“是”，移除元素
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }     
        };
        //添加按钮回调
        colorList.onAddCallback = (ReorderableList list) =>
        {
            if (list.serializedProperty != null)
            {
                //列表大小增加一
                list.serializedProperty.arraySize++;
                //设置列表末尾的索引，索引从0开始所以新的索引是容量减一
                list.index = list.serializedProperty.arraySize - 1;
                //根据索引获取列表元素
                SerializedProperty itemData = list.serializedProperty.GetArrayElementAtIndex(list.index);
                itemData.colorValue = Color.red;
            }
            else
            {
                //执行添加操作
                ReorderableList.defaultBehaviours.DoAddButton(list);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        serializedObject.Update();
        //执行列表的绘制
        colorList.DoLayoutList();
        //应用属性修改
        serializedObject.ApplyModifiedProperties();
    }
}
