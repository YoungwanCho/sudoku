using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scene
{
    public class MainLobby : MonoBehaviour, IScene
    {
        private view.MainLobby _mainLobby = null;
        private SceneManager _sceneManager = null;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
            _sceneManager = manager;
        }

        public void Awake()
        {
            _mainLobby = CreateMainLobbyView();
        }

        public void Start()
        {
            _mainLobby.Initialize(this.OnClickNewGameStart);
        }

        public void OnClickNewGameStart(GameObject obj)
        {
            _sceneManager.ChangeScene(SceneManager.SCENE.LEVELSELECT);
        }

        private view.MainLobby CreateMainLobbyView()
        {
            return FactoryManager.Instance.InstantiateGameObject<view.MainLobby>(DefineData.PREFAB_VIEW_MAINLOBBY_PATH, this.transform, Vector3.zero, Quaternion.identity, Vector3.one, "MainLobby", "UI");
        }
    }
}
