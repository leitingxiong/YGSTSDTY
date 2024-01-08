using QFramework;
namespace System
{
    public interface IIventorySystem : ISystem
    {
        
    }
    public class IventorySystem : AbstractSystem, IIventorySystem
    {
        protected override void OnInit()
        {
        }
        
    }
}