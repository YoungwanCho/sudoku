using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scene
{
    public class InGame : MonoBehaviour, IScene
    {
        public model.StageData StageData { get { return _stageData; } }
        public System.Diagnostics.Stopwatch Timer { get { return _timer; } }

        private SceneManager _sceneManager = null;
        private controller.GameController _gameController = null;
        private model.StageData _stageData = null;
        private controller.StageData _stageCtrl = null;
        private System.Diagnostics.Stopwatch _timer = null;
        
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
            _sceneManager = manager;

            string stageName = string.Format("stage{0}", _sceneManager.GetSelectStageIndex());

            StartCoroutine(GameControllerInit(stageName));
            _timer.Reset();
            _timer.Start();
        }

        public void Awake()
        {
            _gameController = CreateGameController();
            _timer = new System.Diagnostics.Stopwatch();
        }

        public void Update()
        {
            UpdatePlayTime();
        }

        public void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        public string GetPlayTimeString()
        {
            return string.Format("{0}:{1}:{2}", _timer.Elapsed.Hours.ToString("00"), _timer.Elapsed.Minutes.ToString("00"), _timer.Elapsed.Seconds.ToString("00"));
        }

        public void OnClickQuit(GameObject obj)
        {
            _sceneManager.ChangeScene(SceneManager.SCENE.MAINLOBBY);
        }

        public void ClearGame(GameObject obj)
        {
            _timer.Stop();
            _sceneManager.ChangeScene(SceneManager.SCENE.RESULT);
        }

        private void UpdatePlayTime()
        {
            _gameController.UpdatePlayTime(GetPlayTimeString());
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
            GameObject obj = FactoryManager.Instance.InstantiateGameobject(new GameObject(), this.transform, Vector3.zero, Quaternion.identity, Vector3.one, "GameController", "UI");
            return obj.AddComponent<controller.GameController>();
        }

    }
}