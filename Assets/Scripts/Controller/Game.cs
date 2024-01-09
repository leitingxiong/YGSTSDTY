using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class Game : MonoBehaviour,IController
    {
        private void Awake()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart);
            
        }
        void Start()
        {
            transform.Find("Start").GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<StartGameCommand>(); });
            transform.Find("NewRoom").GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<NewRoomCommand>(); });
            transform.Find("UpLevel").GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<UpLevelRoomCommand>(); });
            transform.Find("Clean").GetComponent<Button>().onClick.AddListener(() => { this.SendCommand<CleanCommand>(); });
            transform.Find("Kill").GetComponent<Button>().onClick.AddListener(() => { this.SendCommand<KillCommand>(); });     
                
        }
        
        private void OnGameStart(GameStartEvent e)
        {
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GameStartEvent>(OnGameStart);
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}
