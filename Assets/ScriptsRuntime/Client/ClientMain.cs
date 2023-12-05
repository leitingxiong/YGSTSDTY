using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DC.LoginBusiness.Controller;
using DC.LobbyBusiness.Controller;
using DC.BattleBusiness.Controller;
using DC.Infrastructure.Context;
using DC.UIApplication;
using DC.BattleBusiness;
using DC.Database;
using DC.Assets;
using DC.Template;
using DC.Events;

namespace DC.Client.Entry {

    public class ClientMain : MonoBehaviour {

        // ==== Controller ====
        LoginController loginController;
        LobbyController lobbyController;
        BattleController battleController;

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

            // - Lobby
            lobbyController = new LobbyController();

            // - Battle
            battleController = new BattleController();

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
            lobbyController.Inject(infraContext, uiApp);

            // - Battle
            battleController.Inject(infraContext, uiApp);

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
                    lobbyController.Init();

                    // - Battle
                    battleController.Init();

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
                lobbyController.Enter("admin");

                battleController.Exit();
                return;
            }

            var battleEV = infraContext.EventCore.BattleEventCenter;
            var enterBattleEvent = battleEV.EnterBattleEvent;
            if (enterBattleEvent.isEnter) {
                battleEV.SetEnterBattleEvent((false, 0, 0));
                battleController.Enter(enterBattleEvent.chapter, enterBattleEvent.level);

                lobbyController.Exit();
                return;
            }

            float dt = Time.deltaTime;
            lobbyController.Tick(dt);
            battleController.Tick(dt);

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