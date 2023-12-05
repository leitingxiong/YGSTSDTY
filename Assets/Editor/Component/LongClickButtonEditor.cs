using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(LongClickButton))]
public class LongClickButtonEditor : ButtonEditor
{
    private LongClickButton theTarget;

    public void Awake()
    {
        theTarget = (LongClickButton)this.target;
    }

    public override void OnInspectorGUI()
    {
        int temporary = EditorGUILayout.IntField("设置长按时间（ms）", theTarget.longPressTime);
        if (temporary != theTarget.longPressTime)
            theTarget.longPressTime = temporary;
        base.OnInspectorGUI();
    }
}