using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
///һ������װ�� Unity ���ñ༭�����͵ı༭�����࣬���÷����ȡԭʼ���ֺ�����
/// </summary>
public abstract class DecoratorEditor : Editor
{
	// ����ʹ�÷�����÷����Ŀ�����
	private static readonly object[] EMPTY_ARRAY = new object[0];

	#region Editor Fields

	/// <summary>
	/// װ�� Unity�Ļ�������
	/// </summary>
	private System.Type decoratedEditorType;

	/// <summary>
	/// �༭����������Ӧ�Ķ���
	/// </summary>
	private System.Type editedObjectType;

	private Editor editorInstance;

	#endregion

	private static Dictionary<string, MethodInfo> decoratedMethods = new Dictionary<string, MethodInfo>();

	private static Assembly editorAssembly = Assembly.GetAssembly(typeof(Editor));

	protected Editor EditorInstance
	{
		get
		{
			if (editorInstance == null && targets != null && targets.Length > 0)
			{
				editorInstance = Editor.CreateEditor(targets, decoratedEditorType);
			}

			if (editorInstance == null)
			{
				Debug.LogError("Could not create editor !");
			}

			return editorInstance;
		}
	}

	public DecoratorEditor(string editorTypeName)
	{
		this.decoratedEditorType = editorAssembly.GetTypes().Where(t => t.Name == editorTypeName).FirstOrDefault();

		Init();

		//����Զ���༭������
		System.Type originalEditedType = GetCustomEditorType(decoratedEditorType);

		if (originalEditedType != editedObjectType)
		{
			throw new System.ArgumentException(
				string.Format("Type {0} does not match the editor {1} type {2}",
						  editedObjectType, editorTypeName, originalEditedType));
		}
	}

	/// <summary>
	/// ��ȡ�Զ���༭������
	/// </summary>
	/// <param name="type">����</param>
	/// <returns></returns>
	private System.Type GetCustomEditorType(System.Type type)
	{
		BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

		CustomEditor[] attributes = type.GetCustomAttributes(typeof(CustomEditor), true) as CustomEditor[];
		FieldInfo field = attributes.Select(editor => editor.GetType().GetField("m_InspectedType", flags)).First();

		return field.GetValue(attributes[0]) as System.Type;
	}

	/// <summary>
	/// ��ʼ��
	/// </summary>
	private void Init()
	{
		BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

		CustomEditor[] attributes = this.GetType().GetCustomAttributes(typeof(CustomEditor), true) as CustomEditor[];
		FieldInfo field = attributes.Select(editor => editor.GetType().GetField("m_InspectedType", flags)).First();

		editedObjectType = field.GetValue(attributes[0]) as System.Type;
	}

	void OnDisable()
	{
		if (editorInstance != null)
		{
			DestroyImmediate(editorInstance);
		}
	}

	/// <summary>
	/// ���ݷ�������ȥ��ȡ��Ӧ�ķ���
	/// </summary>
	protected void CallInspectorMethod(string methodName)
	{
		MethodInfo method = null;

		// �� MethodInfo ��ӵ������ֵ���
		if (!decoratedMethods.ContainsKey(methodName))
		{
			BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

			method = decoratedEditorType.GetMethod(methodName, flags);

			if (method != null)
			{
				decoratedMethods[methodName] = method;
			}
			else
			{
				Debug.LogError(string.Format("Could not find method {0}", method));
			}
		}
		else
		{
			method = decoratedMethods[methodName];
		}

		if (method != null)
		{
			method.Invoke(EditorInstance, EMPTY_ARRAY);
		}
	}

	public void OnSceneGUI()
	{
		CallInspectorMethod("OnSceneGUI");
	}

	protected override void OnHeaderGUI()
	{
		CallInspectorMethod("OnHeaderGUI");
	}

	public override void OnInspectorGUI()
	{
		EditorInstance.OnInspectorGUI();
	}

	public override void DrawPreview(Rect previewArea)
	{
		EditorInstance.DrawPreview(previewArea);
	}

	public override string GetInfoString()
	{
		return EditorInstance.GetInfoString();
	}

	public override GUIContent GetPreviewTitle()
	{
		return EditorInstance.GetPreviewTitle();
	}

	public override bool HasPreviewGUI()
	{
		return EditorInstance.HasPreviewGUI();
	}

	public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
	{
		EditorInstance.OnInteractivePreviewGUI(r, background);
	}

	public override void OnPreviewGUI(Rect r, GUIStyle background)
	{
		EditorInstance.OnPreviewGUI(r, background);
	}

	public override void OnPreviewSettings()
	{
		EditorInstance.OnPreviewSettings();
	}

	public override void ReloadPreviewInstances()
	{
		EditorInstance.ReloadPreviewInstances();
	}

	public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
	{
		return EditorInstance.RenderStaticPreview(assetPath, subAssets, width, height);
	}

	public override bool RequiresConstantRepaint()
	{
		return EditorInstance.RequiresConstantRepaint();
	}

	public override bool UseDefaultMargins()
	{
		return EditorInstance.UseDefaultMargins();
	}
}