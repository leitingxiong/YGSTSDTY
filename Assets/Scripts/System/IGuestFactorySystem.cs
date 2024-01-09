using System;

// 定义客人接口
public interface IGuest
{
    void DisplayInfo();
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
    Random random = new Random();
    public IGuest CreateCustomer(string customerType)
    {//随机生成客人
        switch (random.Next(0,100))
        {
            case <20:
                return new WealthyCustomer(0,"Wealthy Customer",1000,1);
            case <40:
                return new CustomerWithItems(0,"Customer with Items",100,0);
            default:
                throw new ArgumentException($"Invalid customer type: {customerType}");
        }
    }
}

// 示例用法
class Program
{
    static void Main()
    {
        CustomerFactory customerFactory = new CustomerFactory();

        // 创建富有的客人
        IGuest wealthyCustomer = customerFactory.CreateCustomer("wealthy");
        wealthyCustomer.DisplayInfo();

        // 创建怪物客人
        IGuest monsterCustomer = customerFactory.CreateCustomer("monster");
        monsterCustomer.DisplayInfo();

        // 创建不干净的客人
        IGuest dirtyCustomer = customerFactory.CreateCustomer("dirty");
        dirtyCustomer.DisplayInfo();

        // ... 可以继续创建其他类型的客人
    }
}
