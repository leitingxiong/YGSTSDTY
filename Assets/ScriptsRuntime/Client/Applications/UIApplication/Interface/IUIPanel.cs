using ScriptsRuntime.Client.Applications.UIApplication.Enum;

namespace ScriptsRuntime.Client.Applications.UIApplication.Interface {

    public interface IUIPanel {

        UIRootLevel RootLevel { get; }
        int OrderWeight { get; }
        bool IsUnique { get; }

        void TearDown();

    }

}