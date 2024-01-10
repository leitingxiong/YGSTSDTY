using System;
using Imodel;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

public class GameManager : Architecture<GameManager>
{
    protected override void Init()
    {
        RegisterSystem<IRoomSystem>(new RoomSystem());
        RegisterSystem<IInnSystem>(new InnSystem());
        RegisterSystem<IBuffSystem>(new BuffSystem());
        RegisterSystem<IShopSystem>(new ShopSystem());
        RegisterSystem<IIventorySystem>(new IventorySystem());
        RegisterSystem<IDaytimeSystem>(new DaytimeSystem());
        RegisterSystem<IGuestSystem>(new GuestSystem());

        RegisterModel<IGameModel>(new GameModel());
        RegisterModel<IShopModel>(new ShopModel());
        RegisterModel<IBuffModel>(new BuffModel());

        RegisterUtility<IStorage>(new PlayerPrefsStorage());
    }
}