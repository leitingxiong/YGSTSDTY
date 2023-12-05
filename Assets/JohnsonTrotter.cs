using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Algorithm
{
    [Serializable]
    public enum ElementDirection
    {
        Right,
        Left
    }

    [Serializable]
    public class NumberStruct
    {
        public ElementDirection direction;
        public int number;
        public int index;

        //结构体没有无参构造函数
        public NumberStruct(int value)
        {
            direction = ElementDirection.Left;
            number = value;
            index = value - 1;
        }

        //重载 > 和 < 运算符
        public static bool operator >(NumberStruct a, NumberStruct b)
        {
            return a.number > b.number;
        }

        public static bool operator <(NumberStruct a, NumberStruct b)
        {
            return a.number < b.number;
        }
    }

    public class JohnsonTrotter : MonoBehaviour
    {
        public int totalNumber = 0;
        [SerializeField] private List<NumberStruct> numberList = new List<NumberStruct>();
        [SerializeField] private List<int> arrangementList = new List<int>();

        private void Start()
        {
            for (int i = 0; i < totalNumber; i++)
            {
                numberList.Add(new NumberStruct(i + 1));
            }
            arrangementList = JohnsonTrotterAlgorithm(numberList);
        }

        public List<int> JohnsonTrotterAlgorithm(List<NumberStruct> list)
        {
            AddToArrangement(list, ref this.arrangementList);
            int temparyIndex = FindMaxMobileIndex(list);
            Debug.Log(temparyIndex);
            //只要还存在可移动的元素就将继续
            while (temparyIndex != -1)
            {
                SwapElement(ref list, ref temparyIndex,false);
            }
            return arrangementList;
        }

        /// <summary>
        /// 检测当前元素是否可移动
        /// </summary>
        /// <param name="list">元素列表</param>
        /// <param name="index">元素索引</param>
        /// <returns></returns>
        private bool CanMobile(List<NumberStruct> list, int index)
        {
            if (list[index].direction == ElementDirection.Right)
            {
                //方向向右时，索引一旦等于总元素个数-1，则表示当前是最后一个元素，则其右边一定没有元素
                if (index < list.Count - 1)
                {
                    return list[index] > list[index + 1];//当左侧的元素大于右侧时，该元素为  可移动元素
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //方向向左时，索引一旦等于0，则表示当前是第一个元素，则其左边一定没有元素
                if (index > 0)
                {
                    return list[index] > list[index - 1];//当左侧的元素小于右侧时，该元素为  可移动元素
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 查找当前最大的可移动元素的索引下标
        /// </summary>
        /// <param name="list">元素列表</param>
        /// <returns>有可移动元素，返回最大的可移动元素的索引下标；没有就返回-1</returns>
        private int FindMaxMobileIndex(List<NumberStruct> list)
        {
            List<NumberStruct> tempary = new List<NumberStruct>();
            for (int index = 0; index < list.Count; index++)
            {
                if (CanMobile(list, index))
                {
                    tempary.Add(list[index]);//将所有的可移动元素添加进临时列表
                }
            }
            if (tempary.Count == 0)//没有任何可移动元素
            {
                return -1;
            }
            else
            {
                int maxIndex = -1;//默认第一个是最大的，这个返回的是原始列表的当前下标
                for (int i = 0; i < tempary.Count - 1; i++)
                {
                    if (tempary[i] < tempary[i + 1])
                    {
                        maxIndex = tempary[i + 1].index;
                    }
                    else
                    {
                        maxIndex = tempary[i].index;
                    }
                }
                return maxIndex;//返回最大的可移动元素的索引下标
            }
        }

        /// <summary>
        /// 将当前排列添加进排列表中
        /// </summary>
        /// <param name="list"></param>
        private void AddToArrangement(List<NumberStruct> list, ref List<int> arrangementList)
        {
            string tempary = "";
            for (int i = 0; i < list.Count; i++)
            {
                tempary += list[i].number.ToString();//将list中的数值转化为字符串
            }
            arrangementList.Add(int.Parse(tempary));//int.Parse将数字的字符串表示形式转换为它的等效 32 位有符号整数。
        }

        /// <summary>
        /// 交换NumberStruct List中的元素，不是交换里面的值
        /// </summary>
        /// <param name="list">NumberStruct类型的表</param>
        /// <param name="currentIndex">当前最大可移动元素的索引下标</param>
        private void SwapElement(ref List<NumberStruct> list, ref int currentIndex,bool changDirection)
        {
            switch (list[currentIndex].direction)
            {
                case ElementDirection.Right:
                    {
                        //方向向右且不是最后一个元素
                        if (currentIndex < list.Count - 1)
                        {
                            //方向向右时，临时存储的就是右边的那个元素的值
                            NumberStruct tempary = list[currentIndex];
                            list[currentIndex] = list[currentIndex + 1];
                            list[currentIndex].index = tempary.index;//现在currentIndex里的值已被改变，我们需要将原本的下标赋予它
                            tempary.index = list[currentIndex + 1].index;//现在将临时的index进行修改，令其等于后一个元素的index
                            list[currentIndex + 1] = tempary;
                            AddToArrangement(list, ref this.arrangementList);
                        }
                        else//当元素走到尽头
                        {
                            //int temparyNumber = -1;
                            if (FindMaxMobileIndex(list) != -1) //我们依旧可以找到一个可移动的元素
                            {
                                //移动一次那个当前最大的可移动元素
                                currentIndex = FindMaxMobileIndex(list);
                                SwapElement(ref list, ref currentIndex, true);
                            }
                            else
                            {
                                currentIndex = -1;
                                return;
                            }
                        }
                        break;
                    }
                case ElementDirection.Left:
                    {
                        Debug.Log("wwwwwwwwwwwwwwwww");
                        if (currentIndex > 0)//方向向左且不是第一个元素
                        {
                            //方向向左时，临时存储的就是左边的那个元素的值
                            NumberStruct tempary = list[currentIndex - 1];
                            list[currentIndex - 1] = list[currentIndex];
                            list[currentIndex - 1].index = tempary.index;//现在currentIndex里的值已被改变，我们需要将原本的下标赋予它
                            tempary.index = list[currentIndex].index;//现在将临时的index进行修改，令其等于前一个元素的index
                            list[currentIndex] = tempary;
                            AddToArrangement(list, ref this.arrangementList);
                            
                        }
                        else//当元素走到尽头
                        {
                            //int temparyNumber = -1;
                            if (FindMaxMobileIndex(list) != -1) //我们依旧可以找到一个可移动的元素
                            {
                                //移动一次那个当前最大的可移动元素
                                currentIndex = FindMaxMobileIndex(list);
                                SwapElement(ref list,ref currentIndex, true);
                            }
                            else
                            {
                                currentIndex = -1;
                                return;
                            }
                        }
                        break;
                    }
            }

            if (changDirection)
            {
                ChangeAllGreaterElementDirection(ref list, currentIndex);
                currentIndex = FindMaxMobileIndex(list);//交换完成后查找下一个最大的可移动元素索引下标
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.Log("================================================");
                    Debug.Log(list[i].direction);
                    Debug.Log(list[i].number);
                    Debug.Log(list[i].index);
                    Debug.Log("================================================");
                }
                currentIndex = FindMaxMobileIndex(list);//交换完成后查找下一个最大的可移动元素索引下标
                Debug.Log(currentIndex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">NumberStruct类型的表</param>
        /// <param name="currentIndex">当前的索引下标</param>
        private void ChangeAllGreaterElementDirection(ref List<NumberStruct> list, int currentIndex)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i == currentIndex || list[i] < list[currentIndex])
                {
                    continue;//使用continue结束本次循环
                }
                if (list[i].direction == ElementDirection.Right)
                {
                    list[i].direction = ElementDirection.Left;
                }
                else
                {
                    list[i].direction = ElementDirection.Right;
                }
            }
        }
    }
}