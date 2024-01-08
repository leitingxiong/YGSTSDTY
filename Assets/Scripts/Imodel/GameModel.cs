using QFramework;

namespace Imodel
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> Day { get; }
        BindableProperty<int> GuestCountLimit { get; }

        BindableProperty<int> Gold { get; }

        BindableProperty<int> GuestCount { get; }

        BindableProperty<int> Cleanliness { get; }
        
        BindableProperty<int> ActionPoint { get; } 
        BindableProperty<int> San { get; }
        BindableProperty<int> Soul { get; }

    }

    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> Day { get; } = new()
        {
            Value = 0
        };
        public BindableProperty<int> GuestCountLimit { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> Gold { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> GuestCount { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> Cleanliness { get; } = new()
        {
            Value = 0
        };

        public BindableProperty<int> ActionPoint { get; } = new()
        {
            Value = 0
        };
        public BindableProperty<int> San { get; } = new()
        {
            Value = 0
        };
        public BindableProperty<int> Soul { get; } = new()
        {
            Value = 0
        };

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();

            Cleanliness.Value = storage.LoadInt(nameof(Cleanliness), 100);
            Cleanliness.Register(v => storage.SaveInt(nameof(Cleanliness), v));
            
            ActionPoint.Value = storage.LoadInt(nameof(ActionPoint), 3); 
            ActionPoint.Register(v => storage.SaveInt(nameof(ActionPoint), v)); 
          
            Gold.Value = storage.LoadInt(nameof(Gold), 0); 
            Gold.Register((v) => storage.SaveInt(nameof(Gold), v)); 
        
            GuestCount.Value = storage.LoadInt(nameof(GuestCount), 0);
            GuestCount.Register((v) => storage.SaveInt(nameof(GuestCount), v));
        
            GuestCountLimit.Value = storage.LoadInt(nameof(GuestCountLimit), 5);
            GuestCountLimit.Register((v) => storage.SaveInt(nameof(GuestCountLimit), v));
        
            Day.Value = storage.LoadInt(nameof(Day), 1);
            Day.Register((v) => storage.SaveInt(nameof(Day), v));
        
            San.Value = storage.LoadInt(nameof(San), 100);
            San.Register((v) => storage.SaveInt(nameof(San), v));
        
            Soul.Value = storage.LoadInt(nameof(Soul), 0);
            Soul.Register((v) => storage.SaveInt(nameof(Soul), v));
        }
    }
}