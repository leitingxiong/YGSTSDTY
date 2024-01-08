using QFramework;
using QFramework.PointGame;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour,IController
    {
        void Start()
        {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
            
        }

        private void OnGamePass(GamePassEvent e)
        {
        }

        void OnDestroy()
        {
            this.UnRegisterEvent<GamePassEvent>(OnGamePass);
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}