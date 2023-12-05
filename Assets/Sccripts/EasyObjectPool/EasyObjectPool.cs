using UnityEngine;
using System.Collections.Generic;
using System;

namespace UniversalPool
{
    [DisallowMultipleComponent]//不能在一个对象上重复添加该脚本。
    [AddComponentMenu("GameObject/PoolObject")]
    public class PoolObject : MonoBehaviour
    {
        public string poolName;//对象池名称
        public bool isPooled; //定义对象是在池中等待还是在使用中
    }

    public enum PoolInflationType//对象池膨胀类型
    {
        INCREASE,// 当对象池膨胀时，将一个对象添加到池中。
        DOUBLE// 当对象池膨胀时，将池的大小增加一倍
    }

    class Pool
    {
        private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();//Stack 栈是一个先进后出（LIFO）表
        private Queue<PoolObject> availableObjQueue = new Queue<PoolObject>();//Queue 队列是先进先出(FIFO)

        //未使用对象的根节点
        private GameObject rootObj;
        private PoolInflationType inflationType;
        private string poolName;
        private int objectsInUse = 0;

        //构造函数
        public Pool(string poolName, GameObject poolObjectPrefab, GameObject rootPoolObj, int initialCount, PoolInflationType type,bool useStack = true)
        {
            if (poolObjectPrefab == null)
            {
#if UNITY_EDITOR
                Debug.LogError("[ObjPoolManager] 对象池预制体为空 !");
#endif
                return;
            }
            this.poolName = poolName;
            this.inflationType = type;
            this.rootObj = new GameObject(poolName + "Pool");//为对象池命名
            this.rootObj.transform.SetParent(rootPoolObj.transform, false);//worldPositionStays如果为 true，则修改父相对位置、缩放和旋转，以使对象保持与以前相同的世界空间位置、旋转和缩放。

            GameObject go = GameObject.Instantiate(poolObjectPrefab);
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                po = go.AddComponent<PoolObject>();//为对象添加PoolObject组件
            }
            po.poolName = poolName;//设置对象所属对象池名称
            AddObjectToPool(po, useStack);//确保对象池中至少存在一个对象

            //填充对象池
            FillPool(Mathf.Max(initialCount, 1), useStack);
        }

        //添加到栈对象池,o(1)
        private void AddObjectToPool(PoolObject po,bool useStack = true)
        {
            po.gameObject.SetActive(false);
            po.gameObject.name = poolName;
            if (useStack)
            {
                availableObjStack.Push(po);//入栈
            }
            else 
            {
                availableObjQueue.Enqueue(po);//入队列,将对象添加到。
            }
            po.isPooled = true;//对象是在对象池中
            //添加到根节点
            po.gameObject.transform.SetParent(rootObj.transform, false);
        }

        /// <summary>
        /// 填充对象池到指定数量
        /// </summary>
        /// <param name="initialCount">填充的数量</param>
        private void FillPool(int initialCount, bool useStack = true)
        {
            for (int index = 0; index < initialCount; index++)
            {
                PoolObject po = null;
                if (useStack)
                {
                    po = GameObject.Instantiate(availableObjStack.Peek());//Peek()返回位于栈顶的对象但不将其移除
                }
                else 
                {
                    po = GameObject.Instantiate(availableObjQueue.Peek());//Peek()返回位于队首的对象但不将其移除
                }
                AddObjectToPool(po, useStack);
            }
        }

        //栈中的下一个可用对象，O(1)
        public GameObject NextAvailableObject(bool autoActive,bool useStack = true)
        {
            PoolObject po = null;
            if (useStack ? availableObjStack.Count > 1 : availableObjQueue.Count > 1)//判断当前是否有足够可使用对象
            {
                //有就出栈，Pop()删除并返回 Stack 顶部的对象，Dequeue删除并返回队首的对象
                po = useStack ? availableObjStack.Pop() : availableObjQueue.Dequeue();
            }
            else
            {
                int increaseSize = 0;
                switch (inflationType)
                {
                    case PoolInflationType.INCREASE:
                        increaseSize = 1;
                        break;
                    case PoolInflationType.DOUBLE:
                        {
                            increaseSize = useStack ? availableObjStack.Count : availableObjQueue.Count + Mathf.Max(objectsInUse, 0);
                            break;
                        }
                }
#if UNITY_EDITOR
                Debug.Log(string.Format("Growing pool {0}: {1} populated", poolName, increaseSize));
#endif
                if (increaseSize > 0)
                {
                    FillPool(increaseSize, useStack);//填充对象池
                    if (useStack)
                    {
                        po = availableObjStack.Pop();//有就出栈，Pop()删除并返回 Stack 顶部的对象
                    }
                    else 
                    {
                        po = availableObjQueue.Dequeue();//有就出队列，Dequeue删除并返回队首的对象
                    }
                }
            }

            GameObject result = null;
            if (po != null)
            {
                objectsInUse++;//已使用对象数增加
                po.isPooled = false;
                result = po.gameObject;
                if (autoActive)
                {
                    result.SetActive(true);
                }
            }

            return result;
        }

        //将对象返回到对象池中，O(1)
        public void ReturnObjectToPool(PoolObject po,bool useStack = true)
        {
            if (poolName.Equals(po.poolName))//Equals(String)确定此实例是否与另一个指定的 String 对象具有相同的值
            {
                objectsInUse--;//已使用对象减少
                /* 我们本可以使用Stack.Contains(Object)来检查该对象是否在对象池中。
                 * 但是会使此方法的时间复杂度变为O（n）*/
                if (po.isPooled)
                {
#if UNITY_EDITOR
                    Debug.LogWarning(po.gameObject.name + " 已经在对象池里了。 您为什么要再次返回？ 请检查用法。");
#endif
                }
                else
                {
                    AddObjectToPool(po, useStack);
                }
            }
            else
            {
                Debug.LogError(string.Format("试图将对象添加到不正确的对象池中 {0} {1}", po.poolName, poolName));
            }
        }
    }
}