using UnityEngine;
using System.Collections.Generic;

namespace UniversalPool 
{
    [DisallowMultipleComponent]//不能在一个对象上重复添加该脚本。
    [AddComponentMenu("GameObject/PoolResourceManager")]
    public class PoolResourceManager : MonoBehaviour
    {
        //使用数据字典为对象池添加关键字
        private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

        private static PoolResourceManager instance = null;

        public static PoolResourceManager Instance
        {
            get
            {
                if (instance == null)//不存在实例就创建一个
                {
                    GameObject go = new GameObject("ResourceManager", typeof(PoolResourceManager));
                    go.transform.localPosition = new Vector3(0, 0, 0);
                    instance = go.GetComponent<PoolResourceManager>();

                    if (Application.isPlaying)
                    {
                        DontDestroyOnLoad(instance.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("[ResourceManager] 您最好在编辑器模式下忽略ResourceManager");
                    }
                }

                return instance;
            }
        }
        #region 初始化对象池
        public void InitPool(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE,bool useStack = true)
        {
            if (poolDict.ContainsKey(poolName))//数据字典中是否已有关键字
            {
                return;
            }
            else
            {
                GameObject Prefab = Resources.Load<GameObject>(poolName);//在Resources文件夹中获取
                if (Prefab == null)
                {
                    Debug.LogError("[ResourceManager] Invalide prefab name for pooling :" + poolName);
                    return;
                }
                poolDict[poolName] = new Pool(poolName, Prefab, gameObject, size, type, useStack);
            }
        }

        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="cell">需要初始化的对象</param>
        /// <param name="size">对象数量</param>
        /// <param name="type">膨胀类型</param>
        public void InitPool(GameObject cell, int size, PoolInflationType type = PoolInflationType.DOUBLE, bool useStack = true)
        {
            if (poolDict.ContainsKey(cell.name))
            {
                return;
            }
            else
            {
                poolDict[cell.name] = new Pool(cell.name, cell, gameObject, size, type, useStack);
            }
        }
        #endregion

        /// <summary>
        /// 获取池中的可用对象,如果池中没有可用的对象则为null。
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="autoActive">是否自动激活</param>
        /// <param name="autoCreate">自动创建对象数量</param>
        public GameObject GetObjectFromPool(string poolName, bool autoActive = true, int autoCreate = 0, bool useStack = true)
        {
            GameObject result = null;

            if (!poolDict.ContainsKey(poolName) && autoCreate > 0)
            {
                InitPool(poolName, autoCreate, PoolInflationType.INCREASE);
            }

            if (poolDict.ContainsKey(poolName))
            {
                Pool pool = poolDict[poolName];
                result = pool.NextAvailableObject(autoActive, useStack);
#if UNITY_EDITOR
                if (result == null)//对象池中找不到可用对象
                {
                    Debug.LogWarning("[ResourceManager]:No object available in " + poolName);
                }
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("[ResourceManager]:Invalid pool name specified: " + poolName);
            }
#endif
            return result;
        }

        /// <summary>
        /// 将GameObject返回对象池
        /// </summary>
        /// <param name="go"></param>
        public void ReturnObjectToPool(GameObject go, bool useStack = true)
        {
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("指定的对象不是对象池实例: " + go.name);
#endif
            }
            else
            {
                Pool pool = null;
                //out关键字是只出不进
                if (poolDict.TryGetValue(po.poolName, out pool))//TryGetValue获取与指定键关联的值
                {
                    pool.ReturnObjectToPool(po, useStack);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning("没有名称可用的对象池: " + po.poolName);
                }
#endif
            }
        }

        /// <summary>
        /// 将对象返回对象池
        /// </summary>
        /// <param name="t"></param>
        public void ReturnTransformToPool(Transform t, bool useStack = true)
        {
            if (t == null)
            {
#if UNITY_EDITOR
                Debug.LogError("[ResourceManager] try to return a null transform to pool!");
#endif
                return;
            }
            ReturnObjectToPool(t.gameObject, useStack);
        }


        //判断该对象是否有对象池
        public bool HasPool(GameObject cell)
        {
            return poolDict.ContainsKey(cell.name);
        }
    }
}