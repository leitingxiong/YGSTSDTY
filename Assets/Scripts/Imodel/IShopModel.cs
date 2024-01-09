using QFramework;

namespace Imodel
{
    public interface IShopModel : IModel
    {
        public BindableProperty<int> price { get; }
        public BindableProperty<int> attempt { get; }
        public BindableProperty<int> Diamond { get; }
    }

    public class ShopModel : AbstractModel, IShopModel
    {
        public BindableProperty<int> price { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> attempt { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> Diamond { get; } = new()
        {
            Value = 0
        };


        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            price.Value = storage.LoadInt(nameof(price), 0);
            price.Register(v => storage.SaveInt(nameof(price), v));

            attempt.Value = storage.LoadInt(nameof(attempt), 0);
            attempt.Register(v => storage.SaveInt(nameof(attempt), v));

            Diamond.Value = storage.LoadInt(nameof(Diamond), 0);
            Diamond.Register(v => storage.SaveInt(nameof(Diamond), v));
        }
    }
}