namespace ScriptsRuntime.Client.Controllers.Battle.Entities.CommonComponent {

    public class InnAttributeComponent {

        public static InnAttributeComponent Create() {
            return new InnAttributeComponent();
        }
        public int Gold { get; set; }
        public int clean { get; set; }
        //客人数量
        public int  GuestNum { get; set; }
        //客人最大数量
        public int GuestMaxNum { get; set; }
        //行动点
        public int ActionPoint { get; set; }


    }
}