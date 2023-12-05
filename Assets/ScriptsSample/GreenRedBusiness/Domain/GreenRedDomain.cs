using UnityEngine;

namespace GreenRedNamespace
{

    public class GreenRedDomain
    {

        GreenRedContext context;

        public GreenRedDomain() { }

        public void Inject(GreenRedContext context)
        {
            this.context = context;
        }

        // 生成
        public void SpawnEntity() {

            var entity = Factory_CreateEntity();
            var repo = context.GreenRedRepo;
            repo.SetCurrent(entity);

            entity.FSM.EnterRed();

        }

        GreenRedEntity Factory_CreateEntity()
        {
            var entity = new GreenRedEntity();
            return entity;
        }

        // 执行状态机
        public void ExecuteFSM(float dt)
        {
            var repo = context.GreenRedRepo;
            var entity = repo.Current;
            var fsm = entity.FSM;

            int status = fsm.Status;
            if (status == 0)
            {
                ExecuteRed(dt);
            }
            else if (status == 1)
            {
                ExecuteYellow(dt);
            }
            else if (status == 2)
            {
                ExecuteGreen(dt);
            }
        }

        void ExecuteRed(float dt)
        {
            var repo = context.GreenRedRepo;
            var entity = repo.Current;
            var fsm = entity.FSM;

            var stateModel = fsm.RedStateModel;
            if (stateModel.isEntering)
            {
                stateModel.isEntering = false;
                Debug.Log("Enter Red");
                // 进入一次
                return;
            }

            // Loop
            stateModel.maintainTimeSec -= dt;
            if (stateModel.maintainTimeSec <= 0)
            {
                fsm.EnterYellow();
            }
        }

        void ExecuteYellow(float dt)
        {
            var repo = context.GreenRedRepo;
            var entity = repo.Current;
            var fsm = entity.FSM;

            var stateModel = fsm.YellowStateModel;
            if (stateModel.isEntering)
            {
                stateModel.isEntering = false;
                Debug.Log("Enter Yellow");
                // 进入一次
                return;
            }

            // Loop
            stateModel.maintainTimeSec -= dt;
            if (stateModel.maintainTimeSec <= 0)
            {
                fsm.EnterGreen();
            }
        }

        void ExecuteGreen(float dt)
        {
            var repo = context.GreenRedRepo;
            var entity = repo.Current;
            var fsm = entity.FSM;

            var stateModel = fsm.GreenStateModel;
            if (stateModel.isEntering)
            {
                stateModel.isEntering = false;
                Debug.Log("Enter Green");
                // 进入一次
                return;
            }

            // Loop
            stateModel.maintainTimeSec -= dt;
            if (stateModel.maintainTimeSec <= 0)
            {
                fsm.EnterRed();
            }
        }

    }

}