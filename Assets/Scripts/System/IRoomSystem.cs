using Imodel;
using QFramework;

namespace System
{
    public interface IRoomSystem : ISystem
    {
        
    }

    public class RoomSystem : AbstractSystem,IRoomSystem
    {
        protected override void OnInit()
        {
            var gameModel = this.GetModel<IGameModel>();
            
            this.RegisterEvent<UpLevelEvent>(e =>
            {
                if (gameModel.Gold.Value>1000)
                {
                    gameModel.Gold.Value -= 1000;
                }
            });
            this.RegisterEvent<CleanEvent>(e =>
            {
                gameModel.Cleanliness.Value += 10;
                gameModel.ActionPoint.Value -= 1;
            });
            this.RegisterEvent<NewDayEvent> (e =>
            {
                
                gameModel.GuestCount.Value = 0;
                gameModel.GuestCountLimit.Value = 5;
            });
        }
    }
}