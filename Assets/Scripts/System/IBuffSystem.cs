using System.Collections.Generic;
using Imodel;
using QFramework;

namespace System
{
    public interface IBuffSystem : ISystem
    {
        void ClearBuff();
    }
    public class Buff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        // 其他属性...

        public Buff(int id, string name, int level)
        {
            ID = id;
            Name = name;
            Level = level;
            // 初始化其他属性...
        }
    }
    public class GoldBuff : Buff
    {
        public int Gold { get; set; }

        public GoldBuff(int id, string name, int level, int gold) : base(id, name, level)
        {
            Gold = gold;
        }
    }
    public class GuestBuff : Buff
    {
        public int Guest { get; set; }

        public GuestBuff(int id, string name, int level, int guest) : base(id, name, level)
        {
            
            Guest = guest;
        }
    }
    public class BuffPool
    {
        private Queue<Buff> buffPool = new();

        public Buff GetBuff(int id, string name, int level)
        {
            Buff buff;

            if (buffPool.Count > 0)
            {
                buff = buffPool.Dequeue();
                // 重新设置buff属性
                buff.ID = id;
                buff.Name = name;
                buff.Level = level;
                // 重新设置其他属性...
            }
            else
            {
                // 如果对象池为空，创建新的buff实例
                buff = new Buff(id, name, level);
            }

            return buff;
        }

        public void ReturnBuff(Buff buff)
        {
            // 将buff归还到对象池
            buffPool.Enqueue(buff);
        }
    }



    public class BuffSystem : AbstractSystem, IBuffSystem
    {
        private IGameModel mGameModel;
        private IBuffModel buffModel;
        protected override void OnInit()
        {
            mGameModel = this.GetModel<IGameModel>();
            buffModel = this.GetModel<IBuffModel>();
            buffModel.GoldBonus.Value = 0;
            buffModel.GuestBonus.Value = 0;
            buffModel.ItemBonus.Value = 0;
            this.RegisterEvent<OnBuffEvent>(e =>
            {
                mGameModel.Gold.Value += buffModel.GoldBonus.Value;
                mGameModel.GuestCount.Value += buffModel.GuestBonus.Value;
            });
            this.RegisterEvent<GetBuffEvent>(e =>
            {
                switch (e.BuffName)
                {
                    case "GoldBonus":
                        buffModel.GoldBonus.Value += 1;
                        break;
                    case "GuestBonus":
                        buffModel.GuestBonus.Value += 1;
                        break;
                    case "ItemBonus":
                        buffModel.ItemBonus.Value += 1;
                        break;
                }
            });
            this.RegisterEvent<NewMonthEvent>(e =>
            {
                ClearBuff();
            });
            
            
        }



        public void ClearBuff()
        {
            buffModel.GoldBonus.Value = 0;
            buffModel.GuestBonus.Value = 0;
            buffModel.ItemBonus.Value = 0;
        }

    }
}