using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scene
{
    public class GameResult : MonoBehaviour, IScene
    {
        private SceneManager _sceneManager = null;
        private view.GameResult _view = null;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
            _sceneManager = manager;
            _view.Initialize(OnClcikQuitButton);
            _view.UpdateClearTime(_sceneManager.GetStageClearTime());
        }

        public void Awake()
        {
            _view = CreateGameResultView();
        }

        public void OnClcikQuitButton(GameObject obj)
        {
            _sceneManager.ChangeScene(SceneManager.SCENE.MAINLOBBY);
        } 

        private view.GameResult CreateGameResultView()
        {
            return FactoryManager.Instance.InstantiateGameObject<view.GameResult>(DefineData.PREFAB_VIEW_GAMERESULT_PATH, this.transform, Vector3.zero, Quaternion.identity, Vector3.one, "LevelSelect", "UI");
        }

    }
}
