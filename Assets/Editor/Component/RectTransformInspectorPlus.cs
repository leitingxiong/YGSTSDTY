using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

[CustomEditor(typeof(RectTransform))]
public class RectTransformInspectorPlus : DecoratorEditor
{
	//RectTransformEditor这个类不是一个对外公开的类。所以不能继承它，我们也就无法调用它的OnInspectorGUI()方法
	public RectTransformInspectorPlus() : base("RectTransformEditor") { }
	private static RectTransform theTarget;
	private static bool showExtraButton = false;

	private ButtonHandler[] buttonHandlerArray = {
		new ButtonHandler("Reset Position To Zero",ResetPosition),
        new ButtonHandler("Reset RotationTo Zero",ResetRotation),
        new ButtonHandler("Reset Scale To One",ResetScale),
		new ButtonHandler("Reset Scale To Zero",ResetScaleToZero),
		new ButtonHandler("Copy Hierarchy Path",CopyHierarchyPath),
	};

	

	private void Awake()
	{
		theTarget = (RectTransform)this.target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GUI.color = Color.yellow;
		showExtraButton = EditorGUILayout.Foldout(showExtraButton, "额外功能按钮");
		if (showExtraButton)
		{
			EditorGUILayout.BeginHorizontal();
			for (int i = 0; i < buttonHandlerArray.Length; i++)
			{
				ButtonHandler temporaryButtonHandler = buttonHandlerArray[i];
				if (GUILayout.Button(temporaryButtonHandler.showDescription, "toolbarbutton"))//, GUILayout.MaxWidth(150)
				{
					temporaryButtonHandler.onClickCallBack();
				}
				GUILayout.Space(5);
				if ((i + 1) % 2 == 0 || i + 1 == buttonHandlerArray.Length)
				{
					EditorGUILayout.EndHorizontal();
					if (i + 1 < buttonHandlerArray.Length)
					{
						GUILayout.Space(5);
						EditorGUILayout.BeginHorizontal();
					}
				}
			}
		}
		GUI.color = Color.white;
	}

	private static void ResetPosition() 
	{
		Undo.RecordObject(theTarget, "ResetPosition To Zero");
		theTarget.localPosition = Vector3.zero;
	}

	private static void ResetRotation()
	{
		Undo.RecordObject(theTarget, "ResetRotation To Zero");
		theTarget.localRotation = Quaternion.identity;
	}

	private static void ResetScale()
	{
		Undo.RecordObject(theTarget, "ResetScale To One");
		theTarget.localScale = Vector3.one;
	}
	private static void ResetScaleToZero() 
	{
		Undo.RecordObject(theTarget, "ResetScale To Zero");
		theTarget.localScale = Vector3.zero;
	}

	private static void CopyHierarchyPath()
	{
		ExtralEditorUtility.GetGameObjectHierarchyPath(theTarget);
	}
}