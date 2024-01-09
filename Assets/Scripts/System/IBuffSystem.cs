using Imodel;
using QFramework;

namespace System
{
    public interface IBuffSystem : ISystem
    {
        void ClearBuff();
        //计算新旧值差值
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