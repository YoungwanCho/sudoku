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
            GameObject prefab = Resources.Load<GameObject>(DefineData.PREFAB_VIEW_GAMERESULT_PATH);
            GameObject obj = null;
            obj = Instantiate(prefab, this.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "LevelSelct";
            obj.layer = LayerMask.NameToLayer("UI");
            return obj.GetComponent<view.GameResult>();
        }

    }
}
