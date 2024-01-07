using UnityEngine;
/*
泛型参数的约束

在定义泛型类时，可以对客户端代码能够在实例化类时用于类型参数的类型种类施加限制。如果客户端代码尝试使用某个约束所不允许的类型来实例化类，则会产生编译时错误。这些限制称为约束。约束是使用 where 上下文关键字指定的。下表列出了六种类型的约束：

where T: struct
类型参数必须是值类型。可以指定除 Nullable 以外的任何值类型。有关更多信息，请参见使用可以为 null 的类型（C# 编程指南）。
where T : class
类型参数必须是引用类型；这一点也适用于任何类、接口、委托或数组类型。
where T：new()
类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时，new() 约束必须最后指定。
where T：<基类名>
类型参数必须是指定的基类或派生自指定的基类。
where T：<接口名称>
类型参数必须是指定的接口或实现指定的接口。可以指定多个接口约束。约束接口也可以是泛型的。
where T：U
为 T 提供的类型参数必须是为 U 提供的参数或派生自为 U 提供的参数。
 */
public class SingleComponent<T> : MonoBehaviour where T : SingleComponent<T>
{
    //设置为私有字段的变量，在派生类中是无法访问的
    private static T instance;
    public static T Instance//[单例模式]通过关键字Static 和其他语句之生成对象的一个实例，确保该脚本在场景中的唯一性
    {
        get 
        {
            return instance;
        }
    }

    //关键字Virtual，使此函数变为一个虚函数，让其可以在一个或多个派生类中被重新定义。
    protected virtual void Awake()
    {
        if (instance != null)//场景中的实例不唯一时
        {
            Destroy(instance.gameObject);
        }
        else 
        {
            instance = this as T;
        }
    }
    //设置为protected类型的变量，派生类可以访问，非派生类无法直接访问
    protected virtual void OnDestroy()
    {
        Destroy(instance.gameObject);
    }

    //如果遇到报错：Some objects were not cleaned up when closing the scene. (Did you spawn new GameObjects from OnDestroy?)
    //请在OnDestory调用单例的地方使用这个判断一下
    /// <summary>
    /// 单例是否已初始化
    /// </summary>
    public static bool IsInitialized
    {
        get
        {
            return instance != null;//返回的是一个bool值，即该实例是否为空
        }
    }
}