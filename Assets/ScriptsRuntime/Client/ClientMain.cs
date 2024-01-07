using System;
using DC;
using DC.Assets;
using DC.Database;
using DC.Events;
using DC.Infrastructure.Context;
using DC.Template;
using ScriptsRuntime.Client.Applications.UIApplication;
using ScriptsRuntime.Client.Controllers.Battle;
using ScriptsRuntime.Client.Controllers.Inn;
using ScriptsRuntime.Client.Controllers.Login;
using UnityEngine;

namespace ScriptsRuntime.Client {

    public class ClientMain : MonoBehaviour {
        //旅舍 英文翻译: Inn
        // ==== Controller ====
        LoginController loginController;
        InnController innController;
        YardController yardController;

        // ==== Application ====
        UIApp uiApp;

        // ==== Infrastructure ====
        InfraContext infraContext;

        // ==== Core ====
        AssetsCore assetsCore;
        TemplateCore templateCore;
        DatabaseCore dbCore;
        EventCore eventCore;

        bool isInit;
        bool isTearDown;

        void Awake() {

            isInit = false;
            isTearDown = false;

            Application.targetFrameRate = 60;
            MonoBehaviour.DontDestroyOnLoad(gameObject);

            // ==== Instantiate ====
            // - Assets
            assetsCore = new AssetsCore();

            // - Template
            templateCore = new TemplateCore();

            // - Database
            dbCore = new DatabaseCore();

            // - Events
            eventCore = new EventCore();

            // - Infrastructure
            infraContext = new InfraContext();

            // - Login
            loginController = new LoginController();

            // - Level
            innController = new InnController();

            // - Battle
            yardController = new YardController();

            // - UI
            uiApp = new UIApp();

            // ==== Injection ====
            // - Infrastructure
            infraContext.Inject(assetsCore, templateCore, dbCore, eventCore);

            // - UI
            var canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            uiApp.Inject(canvas, assetsCore);

            // - Login
            loginController.Inject(infraContext, uiApp);

            // - Lobby
            innController.Inject(infraContext, uiApp);

            // - Battle
            yardController.Inject(infraContext, uiApp);

            Action action = async () => {

                try {

                    // ==== Initialize ====
                    // - Assets
                    await assetsCore.LoadAll();

                    // - Template
                    await templateCore.LoadAll();

                    // - Database

                    // - UI
                    uiApp.Init();

                    // - Login
                    loginController.Init();

                    // - Lobby
                    innController.Init();

                    // - Battle
                    yardController.Init();

                    // ==== GAME START ====
                    loginController.Enter();

                    isInit = true;

                } catch (Exception ex) {
                    DCLog.Error(ex.ToString());
                }

            };

            action.Invoke();

        }

        void Update() {

            if (!isInit) {
                return;
            }

            var lobbyEV = infraContext.EventCore.LobbyEventCenter;
            if (lobbyEV.IsEnterLobby) {
                lobbyEV.SetIsEnterLobby(false);
                innController.Enter("admin");

                yardController.Exit();
                return;
            }

            var battleEV = infraContext.EventCore.BattleEventCenter;
            var enterBattleEvent = battleEV.EnterBattleEvent;
            if (enterBattleEvent.isEnter) {
                battleEV.SetEnterBattleEvent((false, 0, 0));
                yardController.Enter(enterBattleEvent.chapter, enterBattleEvent.level);

                innController.Exit();
                return;
            }

            float dt = Time.deltaTime;
            innController.Tick(dt);
            yardController.Tick(dt);

        }

        void LateUpdate() {
            if (!isInit) {
                return;
            }
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void OnDestroy() {
            TearDown();
        }

        void TearDown() {
            if (isTearDown) {
                return;
            }

            isTearDown = true;

            dbCore.TearDown();

        }

    }

}
