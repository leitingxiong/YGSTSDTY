using System;
using System.Collections.Generic;
using Imodel;
using QFramework;
using UnityEngine;
using Random = System.Random;

public interface IGuestSystem : ISystem
{
}
public class GuestSystem : AbstractSystem, IGuestSystem
{        
    CustomerFactory customerFactory = new();
    private IGameModel gameModel;

    protected override void OnInit()
    {
        customerFactory.Createrandom(10);
        this.RegisterEvent<NewDayEvent>(e =>
        {
            customerFactory.Createrandom(gameModel.GuestCount.Value);
        });
    }
}
// 定义客人接口
public interface IGuest
{
    //实例化客人
    void Instance()
    {
        
    }
}
[CreateAssetMenu(fileName = "Guest", menuName = "Guest/Guest", order = 0),Serializable]
public class Guest : ScriptableObject,IGuest
{
    public int id;
    public string name;
    public int gold;
    public int soul;
    //构造函数
    public Guest(int id, string name, int gold, int soul)
    {
        id=0;
        name = "Guest";
        gold = 50;
        soul = 0;
    }
    public  void Instance()
    {
    }
}

// 富有的客人
[CreateAssetMenu(fileName = "WealthyCustomer", menuName = "Guest/WealthyCustomer", order = 1)]
public class WealthyCustomer : Guest, IGuest
{
    public WealthyCustomer(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
        id = 1;
        name = "Wealthy Customer";
        gold = 1000;
        soul = 1;
    }
    public void Instance()
    {
        Resources.Load<GameObject>("Prefabs/Guests/WealthyCustomer");
    }
}

// 携带道具的客人
public class CustomerWithItems : Guest,IGuest
{
    public CustomerWithItems(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
        id = 2;
        name = "Customer with Items";
        gold = 100;
        soul = 0;
    }
    public void Instance()
    {
        Console.WriteLine("Customer with Items");
    }
}

// 有更多灵魂的客人
public class SoulfulCustomer : Guest,IGuest
{
    public SoulfulCustomer(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
    }
    public void Instance()
    {
        Console.WriteLine("Soulful Customer");
    }
}

// 怪物客人
public class MonsterCustomer : Guest,IGuest
{
    public MonsterCustomer(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
    }
    public void Instance()
    {
        Console.WriteLine("Monster Customer");
    }
}

// 不干净的客人
public class DirtyCustomer : Guest,IGuest
{
    public void Instance()
    {
        Console.WriteLine("Dirty Customer");
    }

    public DirtyCustomer(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
    }
}

// 客人工厂
public class CustomerFactory
{
    public void Createrandom(int num)
    {
        Random random = new Random();
        for (int i = 0; i < num; i++)
        {
            IGuest customer = CreateCustomer(random.Next(0,100));
            customer.Instance();
        }
    }
    public IGuest CreateCustomer(int random)
    {//随机生成客人
        switch (random)
        {
            case <20:
                return new WealthyCustomer(0,"Wealthy Customer",1000,1);
            case <40:
                return new CustomerWithItems(0,"Customer with Items",100,0);
            default:
                throw new ArgumentException($"Invalid customer type: {random}");
        }
    }
}

