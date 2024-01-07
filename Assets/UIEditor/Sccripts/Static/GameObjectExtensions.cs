using UnityEngine;

namespace ExtendsFunction
{
    // Unity GameObject的扩展功能
    public static class GameObjectExtensions
    {
        #region Instantiate 实例化
        public static GameObject InstantiateSelf(this GameObject target, Transform parent)
        {
            if (target == null)
            {
                return null;
            }
            if (parent != null)
            {
                return Object.Instantiate(target, Vector3.zero, Quaternion.identity, parent);
            }
            return Object.Instantiate(target, Vector3.zero, Quaternion.identity);
        }

        public static GameObject InstantiateSelf(this GameObject target, Component parent)
        {
            return InstantiateSelf(target, parent?.transform);
        }

        public static GameObject InstantiateSelf(this GameObject target, GameObject parent)
        {
            return InstantiateSelf(target, parent?.transform);
        }

        public static GameObject InstantiateSelf(this Component target, Component parent)
        {
            return InstantiateSelf(target?.gameObject, parent?.transform);
        }

        public static GameObject InstantiateSelf(this Component target, GameObject parent)
        {
            return InstantiateSelf(target?.gameObject, parent?.transform);
        }
        #endregion

        #region 销毁

        #region DestroyObject
        public static void DestroyGameObj(this GameObject target)
        {
            if (target == null)
            {
                return;
            }
            Object.Destroy(target);
        }

        public static void DestroyGameObj(this Component target)
        {
            DestroyGameObj(target?.gameObject);
        }
        #endregion

        #region DestroyGameObjDelay
        public static void DestroyGameObjDelay(this GameObject target, float time)
        {
            if (target == null)
            {
                return;
            }
            Object.Destroy(target, time);
        }

        public static void DestroyGameObjDelay(this Component target, float time)
        {
            DestroyGameObjDelay(target?.gameObject, time);
        }
        #endregion

        #region ClearChildren
        /// <summary>
        /// 清空子节点
        /// </summary>
        /// <param name="target"></param>
        /// <param name="index">清空到第几个子节点为止，默认为0，即全部清除</param>
        public static void ClearChildren(this Transform target, int index = 0)
        {
            if (target == null)
            {
                return;
            }
            int len = target.childCount;
            for (int i = len - 1; i >= index; i--)
            {
                Transform child = target.GetChild(i);
                Object.Destroy(child.gameObject);
            }
        }

        public static void ClearChildren(this Component target, int index = 0)
        {
            ClearChildren(target?.transform, index);
        }

        public static void ClearChildren(this GameObject target, int index = 0)
        {
            ClearChildren(target?.transform, index);
        }
        #endregion
        #endregion

        #region 添加组件
        /// <summary>
        /// 获取与添加组件
        /// </summary>
        /// <typeparam name="T">要获取或添加的组件名称</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            if (obj == null)
            {
                return null;
            }
            T t = obj.GetComponent<T>();
            if (t == null)
            {
                t = obj.AddComponent<T>();
            }
            return t;
        }

        public static T GetOrAddComponent<T>(this Component target) where T : Component
        {
            return GetOrAddComponent<T>(target?.gameObject);
        }

        public static Component GetOrAddComponent(this GameObject target, System.Type t)
        {
            if (target == null || t == null)
            {
                return null;
            }
            var tt = target.GetComponent(t);
            if (tt == null)
            {
                tt = target.AddComponent(t);
            }
            return tt;
        }

        public static Component GetOrAddComponent(this Component target, System.Type t)
        {
            return GetOrAddComponent(target?.gameObject, t);
        }
        #endregion

        #region GetChildCount 获取子节点数量
        /// <summary>
        /// 获取子节点数量
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int GetChildCountExtension(this Component target)
        {
            if (target == null)
            {
                return 0;
            }
            return target.transform.childCount;
        }
        /// <summary>
        /// 获取子节点数量
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int GetChildCountExtension(this GameObject target)
        {
            if (target == null)
            {
                return 0;
            }
            return target.transform.childCount;
        }
        #endregion

        #region Vector类型比较
        /// <summary>
        /// 二维向量a中的每一个分量是否都是小于b的
        /// </summary>
        /// <param name="a">二维向量a</param>
        /// <param name="b">二维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector2ALessThanB(this Vector2 a, Vector2 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return (a.x < b.x) && (a.y < b.y);
            else
                return (a.x < b.x) || (a.y < b.y);
        }

        /// <summary>
        /// 二维向量a中的每一个分量是否都是大于b的
        /// </summary>
        /// <param name="a">二维向量a</param>
        /// <param name="b">二维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector2AGreaterThanB(this Vector2 a, Vector2 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return a.x > b.x && a.y > b.y;
            else
                return a.x > b.x || a.y > b.y;
        }

        /// <summary>
        /// 三维向量a中的每一个分量是否都是小于b的
        /// </summary>
        /// <param name="a">三维向量a</param>
        /// <param name="b">三维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector3ALessThanB(this Vector3 a, Vector3 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return (a.x < b.x) && (a.y < b.y) && (a.z < b.z);
            else
                return (a.x < b.x) || (a.y < b.y) || (a.z < b.z);
        }

        /// <summary>
        /// 三维向量a中的每一个分量是否都是大于b的
        /// </summary>
        /// <param name="a">三维向量a</param>
        /// <param name="b">三维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector3AGreaterThanB(this Vector3 a, Vector3 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return a.x > b.x && a.y > b.y && a.z > b.z;
            else
                return a.x > b.x || a.y > b.y || a.z > b.z;
        }

        /// <summary>
        /// 四维向量a中的每一个分量是否都是小于b的
        /// </summary>
        /// <param name="a">四维向量a</param>
        /// <param name="b">四维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector4ALessThanB(this Vector4 a, Vector4 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return a.x < b.x && a.y < b.y && a.z < b.z && a.w < b.w;
            else
                return a.x < b.x || a.y < b.y || a.z < b.z || a.w < b.w;
        }

        /// <summary>
        /// 四维向量a中的每一个分量是否都是大于b的
        /// </summary>
        /// <param name="a">四维向量a</param>
        /// <param name="b">四维向量b</param>
        /// <param name="useAndOperate">是否对每一个分量都进行与运算（默认是与）</param>
        /// <returns></returns>
        public static bool Vector4AGreaterThanB(this Vector4 a, Vector4 b, bool useAndOperate = true)
        {
            if (useAndOperate)
                return a.x > b.x && a.y > b.y && a.z > b.z && a.w > b.w;
            else
                return a.x > b.x || a.y > b.y || a.z > b.z || a.w > b.w;
        }
        #endregion
    }
}
