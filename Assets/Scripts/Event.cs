using System;
using QFramework;

public class NewRoomEvent
{
}

public class GameStartEvent
{
}

public class UpLevelEvent
{
}

public class CleanEvent
{
}

public class NewDayEvent
{
}
public class NewMonthEvent
{
}
public class KillEvent
{
}
public class GetBuffEvent
{
    public BindableProperty<float> Buff { get; set; }
    public String BuffName { get; set; }
}
public class OnBuffEvent
{
}
public class RefreshShopEvent
{
}
public class BuyEvent
{
    public BindableProperty<int> Price { get; set; }
}
public class GetMoneyEvent
{
    public BindableProperty<int> Money { get; set; }
}
public class GetSoulEvent
{
    public BindableProperty<int> Soul { get; set; }
}