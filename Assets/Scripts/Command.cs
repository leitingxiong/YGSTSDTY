using Imodel;
using QFramework;
using UnityEngine;

public class StartGameCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        // 重置数据
        var gameModel = this.GetModel<IGameModel>();
        gameModel.Day.Value = 1;
        gameModel.Gold.Value = 1000;
        gameModel.GuestCount.Value = 0;
        gameModel.GuestCountLimit.Value = 50;
        this.SendEvent<GameStartEvent>();
    }
}
public class NewRoomCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        gameModel.GuestCountLimit.Value += 5;
        gameModel.ActionPoint.Value -= 1;
        
        this.SendEvent<NewRoomEvent>();
    }
}
public class RoomCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        gameModel.GuestCount.Value = 0;
        gameModel.GuestCountLimit.Value = 5;
    }
}
public class UpLevelRoomCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        if (gameModel.Gold.Value >= 1000)
        {
            gameModel.Gold.Value -= 1000;
            gameModel.ActionPoint.Value -= 1;
            this.SendEvent<UpLevelEvent>();
        }
    }
}
public class CleanCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        gameModel.Cleanliness.Value += 10;
        gameModel.ActionPoint.Value -= 1;
        this.SendEvent<CleanEvent>();
    }
}
public class NewDayCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        gameModel.Cleanliness.Value -= gameModel.GuestCount.Value;
        gameModel.Day.Value += 1;
        gameModel.ActionPoint.Value = 2;
        gameModel.GuestCount.Value = gameModel.GuestCountLimit.Value/2;
        this.SendEvent<NewDayEvent>();
    }
}

public class KillCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var gameModel = this.GetModel<IGameModel>();
        gameModel.GuestCount.Value -= 1;
        this.SendEvent<KillEvent>();
    }
}

public class GetBuffCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var buffModel = this.GetModel<IBuffModel>();
        buffModel.GoldBonus.Value += 2;
    }
}
public class OnBuffCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.SendEvent<OnBuffEvent>();
    }
}

