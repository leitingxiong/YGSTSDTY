using QFramework;

namespace System
{
    public interface IDaytimeSystem : ISystem {
    
        
    }
    public class DaytimeSystem :AbstractSystem, IDaytimeSystem 
    {
        protected override void OnInit()
        {
            // var gameModel = this.GetModel<IGameModel>();
            // this.RegisterEvent<NewDayEvent>(e =>
            // {
            //     gameModel.Day.Value += 1;
            //     gameModel.ActionPoint.Value = 1;
            //     gameModel.Cleanliness.Value -= 10;
            //     gameModel.Gold.Value += 100;
            //     gameModel.GuestCount.Value = 10;
            // });
        }
    }
}