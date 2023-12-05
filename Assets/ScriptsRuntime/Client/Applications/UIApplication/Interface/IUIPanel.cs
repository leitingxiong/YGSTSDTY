namespace DC.UIApplication {

    public interface IUIPanel {

        UIRootLevel RootLevel { get; }
        int OrderWeight { get; }
        bool IsUnique { get; }

        void TearDown();

    }

}