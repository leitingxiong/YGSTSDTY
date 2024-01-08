using QFramework;

namespace System
{
    public interface IInnSystem : ISystem
    {
    }

    public class InnSystem : AbstractSystem, IInnSystem
    {
        protected override void OnInit()
        {
            // var gameModel = this.GetModel<IGameModel>();
            // this.RegisterEvent<NewRoomEvent>(e=>
            // {
            //     gameModel.GuestCountLimit.Value += 5;
            //     gameModel.ActionPoint.Value -= 1;
            // });

        }
    }
}