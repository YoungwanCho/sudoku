using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scene
{
    public class InGame : MonoBehaviour, IScene
    {
        public model.StageData StageData { get { return _stageData; } }

        private SceneManager _sceneManager = null;
        private controller.GameController _gameController = null;
        private model.StageData _stageData = null;
        private controller.StageData _stageCtrl = null;
        
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
            _sceneManager = manager;

            string stageName = string.Format("stage{0}", _sceneManager.GetSelectStageIndex() + 1);

            StartCoroutine(GameControllerInit(stageName));
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

        private IEnumerator GameControllerInit(string stateName)
        {
            yield return LoadStageData(stateName);
            _stageData = _stageCtrl.Data;
            _gameController.Initialize(this);
        }

        private IEnumerator LoadStageData(string stageName)
        {
            _stageCtrl = new controller.StageData();
            return _stageCtrl.LoadStageData(stageName);
        }

        private controller.GameController CreateGameController()
        {
            GameObject obj = Instantiate(new GameObject(), this.transform) as GameObject;
            obj.AddComponent<RectTransform>();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "GameController";
            obj.layer = LayerMask.NameToLayer("UI");
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<controller.GameController>();
        }

    }
}