using QFramework;
namespace System
{
    public interface IShopSystem : ISystem
    {
        
    }
    public class ShopSystem : AbstractSystem, IShopSystem
    {
        protected override void OnInit()
        {
        }
        
    }
}