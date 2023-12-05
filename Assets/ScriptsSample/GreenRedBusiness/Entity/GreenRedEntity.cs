namespace GreenRedNamespace
{

    public class GreenRedEntity
    {

        FSMComponent fsm;
        public FSMComponent FSM => fsm;

        public GreenRedEntity()
        {
            this.fsm = new FSMComponent();
        }

    }
}