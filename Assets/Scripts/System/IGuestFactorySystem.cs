using System;

// 定义客人接口
public interface IGuest
{
    void DisplayInfo(){}
}

public class Guest : IGuest
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Gold { get; set; }
    public int Soul { get; set; }
    //构造函数
    public Guest(int id, string name, int gold, int soul)
    {
        id=0;
        name = "Guest";
        gold = 50;
        soul = 0;
    }
    public  void DisplayInfo()
    {
    }
}

// 富有的客人
public class WealthyCustomer : Guest, IGuest
{
    public WealthyCustomer(int id, string name, int gold, int soul) : base(id, name, gold, soul)
    {
        id = 1;
        name = "Wealthy Customer";
        gold = 1000;
        soul = 1;
    }
    public void DisplayInfo()
    {
        Console.WriteLine("Wealthy Customer");
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
    public void DisplayInfo()
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
    public void DisplayInfo()
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
    public void DisplayInfo()
    {
        Console.WriteLine("Monster Customer");
    }
}

// 不干净的客人
public class DirtyCustomer : Guest,IGuest
{
    public void DisplayInfo()
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
        CustomerFactory customerFactory = new CustomerFactory();
        for (int i = 0; i < num; i++)
        {
            IGuest customer = customerFactory.CreateCustomer(random.Next(0,100));
            customer.DisplayInfo();
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

class Program
{
    void Main()
    {
        Random random = new Random();
        CustomerFactory customerFactory = new CustomerFactory();
        
        
        // 创建富有的客人
        IGuest wealthyCustomer = customerFactory.CreateCustomer(random.Next(0,100));
        wealthyCustomer.DisplayInfo();

        // 创建怪物客人
        IGuest monsterCustomer = customerFactory.CreateCustomer(random.Next(0,100));
        monsterCustomer.DisplayInfo();

        // 创建不干净的客人
        IGuest dirtyCustomer = customerFactory.CreateCustomer(random.Next(0,100));
        dirtyCustomer.DisplayInfo();

    }
    //根据需要的数量生成随机客人
   
}
