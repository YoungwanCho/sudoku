﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene
{

    public class InGame : MonoBehaviour, IScene
    {
        private SceneManager _sceneManager = null;
        private controller.GameController _gameController = null;
        
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
            _sceneManager = manager;
            _gameController.Initialize(this);
        }

        public void Awake()
        {
            _gameController = CreateGameController();
        }

        public void Start()
        {
        }

        public void OnClickQuit(GameObject obj)
        {
            _sceneManager.ChangeScene(SceneManager.SCENE.MAINLOBBY);
        }

        public void ClearGame(GameObject obj)
        {
            _sceneManager.ChangeScene(SceneManager.SCENE.RESULT);
        }

        private controller.GameController CreateGameController()
        {
            GameObject obj = Instantiate(new GameObject(), this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "GameController";
            obj.layer = LayerMask.NameToLayer("UI");
            return obj.AddComponent<controller.GameController>();            
        }

    }
}