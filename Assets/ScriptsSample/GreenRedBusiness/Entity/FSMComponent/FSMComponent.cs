namespace GreenRedNamespace
{

    // FSM: 有限状态机
    public class FSMComponent
    {

        int status; // 0 = Red, 1 = Yellow, 2 = Green
        public int Status => status;

        RedStateModel redStateModel;
        public RedStateModel RedStateModel => redStateModel;

        YellowStateModel yellowStateModel;
        public YellowStateModel YellowStateModel => yellowStateModel;

        GreenStateModel greenStateModel;
        public GreenStateModel GreenStateModel => greenStateModel;

        public FSMComponent()
        {
            redStateModel = new RedStateModel();
            yellowStateModel = new YellowStateModel();
            greenStateModel = new GreenStateModel();
        }

        public void EnterRed()
        {
            status = 0;
            var stateModel = redStateModel;
            stateModel.maintainTimeSec = stateModel.maintainTimeSecMax;
            stateModel.isEntering = true;
        }

        // Enter Yellow
        public void EnterYellow()
        {
            status = 1;
            var stateModel = yellowStateModel;
            stateModel.maintainTimeSec = stateModel.maintainTimeSecMax;
            stateModel.isEntering = true;
        }

        // Enter Green
        public void EnterGreen()
        {
            status = 2;
            var stateModel = greenStateModel;
            stateModel.maintainTimeSec = stateModel.maintainTimeSecMax;
            stateModel.isEntering = true;
        }


    }
}