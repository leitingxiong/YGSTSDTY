using DC.UIApplication;
using DC.Infrastructure.Context;
using DC.LobbyBusiness.Context;

namespace DC.LobbyBusiness.Controller {

    // 大厅
    /*
        Enter:
            1. 加载 奴仆/宝箱 数据
                (任务/商店/邮件/设置/公告/活动/好友/聊天/排行榜/战斗/背包/装备/技能/宠物/宝石/宝物/神器)
            2. 打开 UI_Lobby
    */
    public class LobbyController {

        InfraContext infraContext;
        LobbyContext lobbyContext;
        AllLobbyDomain allLobbyDomain;

        public LobbyController() {
            this.lobbyContext = new LobbyContext();
            this.allLobbyDomain = new AllLobbyDomain();
        }

        public void Inject(InfraContext infraContext, UIApp uiApp) {
            this.infraContext = infraContext;

            lobbyContext.Inject(uiApp);
            allLobbyDomain.LobbyDomain.Inject(infraContext, lobbyContext);
        }

        public void Init() {
            
        }

        public void Enter(string accountName) {
            var lobbyDomain = allLobbyDomain.LobbyDomain;
            lobbyDomain.OpenLobby(accountName);
        }

        public void Tick(float dt) {
            
        }

        public void Exit() {

        }

    }

}