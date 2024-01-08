using System.Collections.Generic;
using QFramework;
using Unidux.Example.List;
using UnityEngine;

public interface IBuffModel : IModel
{
    BindableProperty<float> GoldBonus { get; }
    BindableProperty<float> GuestBonus { get; }
    BindableProperty<float> ItemBonus { get; }
    List<BindableProperty<float>> BuffList { get; set; }
}
[System.Serializable]
public class BuffModel : AbstractModel, IBuffModel
{
    public BindableProperty<float> GoldBonus { get; } = new()
    {
        Value = 0
    };
    public BindableProperty<float> GuestBonus { get; } = new()
    {
        Value = 0
    };
    public BindableProperty<float> ItemBonus { get; } = new()
    {
        Value = 0
    };
    public List<BindableProperty<float>> BuffList { get; set; } = new ();
    protected override void OnInit()
    {
        var storage = this.GetUtility<IStorage>();
        // GoldBonus.Value = storage.LoadInt(nameof(GoldBonus), 0);
        // GoldBonus.Register(v => storage.SaveInt(nameof(GoldBonus), v));
        // GuestBonus.Value = storage.LoadInt(nameof(GuestBonus), 0);
        // GuestBonus.Register(v => storage.SaveInt(nameof(GuestBonus), v));
        // ItemBonus.Value = storage.LoadInt(nameof(ItemBonus), 0);
        // ItemBonus.Register(v => storage.SaveInt(nameof(ItemBonus), v));
    }
}
