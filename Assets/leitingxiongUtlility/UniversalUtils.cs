#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace KoiroPkg_Universal
{
    public static class UniversalUtils
    {
        public static T? LazyGetComponent<T>(this Component behaviour, ref T? component)
        {
            if (component == null)
            {
                component = behaviour.GetComponent<T>();
            }

            if (component == null)
            {
                Debug.LogError("LazyGetComponent failed! Type: " + typeof(T));
            }

            return component;
        }

        public static T? LazyGetComponentInParent<T>(this Component behaviour, ref T? component)
        {
            if (component == null)
            {
                component = behaviour.GetComponentInParent<T>();
            }

            if (component == null)
            {
                Debug.LogError("LazyGetComponentInParent failed! Type: " + typeof(T));
            }

            return component;
        }

        public static T? LazyGetComponentInChildren<T>(this Component behaviour, ref T? component)
        {
            if (component == null)
            {
                component = behaviour.GetComponentInChildren<T>();
                if (component == null)
                {
                    Debug.LogError("LazyGetComponentInChildren failed! Type: " + typeof(T));
                }
            }

            return component;
        }

        public static T LazyFindObjectByType<T>(ref T comp) where T : Object
        {
            if (comp == null)
            {
                comp = Object.FindObjectOfType<T>();
                if (comp == null)
                {
                    Debug.LogError("LazyFindObjectByType failed! Type: " + typeof(T));
                }
            }

            return comp;
        }

        public static T LazyResource<T>(ref T obj, string path) where T : Object
        {
            if (obj == null)
            {
                obj = Resources.Load<T>(path);
            }

            return obj;
        }

        public static List<T> LazyList<T>(ref List<T> list)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            return list;
        }

        public static Vector2 XZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector2 XY(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector2Int XY(this Vector3Int vector3Int)
        {
            return new Vector2Int(vector3Int.x, vector3Int.y);
        }

        public static Vector3 XYZofY(this Vector2 vector2, float y = 0)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }

        public static Vector3 XYZofZ(this Vector2 vector2, float z = 0)
        {
            return new Vector3(vector2.x, vector2.y, z);
        }

        public static Vector3Int XYZofZ(this Vector2Int vector2Int, int z = 0)
        {
            return new Vector3Int(vector2Int.x, vector2Int.y, z);
        }

        public static Color ColorFrom255(int r, int g, int b, int a = 255)
        {
            return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f);
        }

        public static Color FromHtmlString(String color)
        {
            return Color.black;
        }

        public static void Shuffle<T>(List<T> list)
        {
            for (var i = 0; i != list.Count - 1; i++)
            {
                var index = Random.Range(i + 1, list.Count);
                Assert.IsTrue(index >= 0);
                (list[i], list[index]) = (list[index], list[i]);
            }
        }

        public static string GetIEnumerableString(IEnumerable iEnumerable)
        {
            string ret = "";
            foreach (var obj in iEnumerable)
            {
                ret += " / " + obj.ToString();
            }

            return ret;
        }

        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
#if UNITY_EDITOR

        public static T? ObjectField<T>(string name, Object obj) where T : Object
        {
            return EditorGUILayout.ObjectField(name, obj, typeof(T), obj) as T;
        }

        public static void MarkPrefabComponentDirty(Component self)
        {
            EditorUtility.SetDirty(self);
            PrefabUtility.RecordPrefabInstancePropertyModifications(self);
            EditorSceneManager.MarkSceneDirty(self.gameObject.scene);
        }
#endif
    }
}