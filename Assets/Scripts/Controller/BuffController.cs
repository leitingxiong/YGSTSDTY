using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BuffController : MonoBehaviour
    {
        // 人物的属性...
        private List<Buff> activeBuffs = new List<Buff>();
        private BuffPool buffPool = new BuffPool();

        public void GainBuff(int id, string name, int level)
        {
            // 获取buff
            Buff newBuff = buffPool.GetBuff(id, name, level);

            // 移除低等级的buff
            activeBuffs.RemoveAll(buff => buff.ID == id && buff.Level < level);

            // 将新的buff添加到活动buff列表
            activeBuffs.Add(newBuff);

            // 将buff提供的属性加成赋予人物
            ApplyBuffEffects(newBuff);
        }

        private void ApplyBuffEffects(Buff buff)
        {
            // 实现根据buff属性对人物进行加成的逻辑
            // 例如，根据buff的攻击属性增加人物的攻击力等
        }
    }
}