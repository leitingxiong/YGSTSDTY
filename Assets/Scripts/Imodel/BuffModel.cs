using System.Collections.Generic;
using QFramework;

namespace Imodel
{
    public interface IBuffModel : IModel
    {
        BindableProperty<int> GoldBonus { get; }
        BindableProperty<int> GuestBonus { get; }
        BindableProperty<int> ItemBonus { get; }
        List<BindableProperty<int>> BuffList { get; set; }
    }

    [System.Serializable]
    public class BuffModel : AbstractModel, IBuffModel
    {
        public BindableProperty<int> GoldBonus { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> GuestBonus { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> ItemBonus { get; } = new()
        {
            Value = 0
        };

        public List<BindableProperty<int>> BuffList { get; set; } = new();

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            BuffList.Add(GoldBonus);
            BuffList.Add(GuestBonus);

            // GoldBonus.Value = storage.LoadInt(nameof(GoldBonus), 0);
            // GoldBonus.Register(v => storage.SaveInt(nameof(GoldBonus), v));
            // GuestBonus.Value = storage.LoadInt(nameof(GuestBonus), 0);
            // GuestBonus.Register(v => storage.SaveInt(nameof(GuestBonus), v));
            // ItemBonus.Value = storage.LoadInt(nameof(ItemBonus), 0);
            // ItemBonus.Register(v => storage.SaveInt(nameof(ItemBonus), v));
        }
    }
}