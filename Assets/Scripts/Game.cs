using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

using UnityEngine;

    public class Game : MonoBehaviour,IController
    {
        private void Awake()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart);
            
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
