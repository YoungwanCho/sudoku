using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scene
{
    public class LevelSelect : MonoBehaviour, IScene
    {
        public int LevelSelectIndex { get { return _levelSelectIndex; } }
        public SceneManager _sceneManager;

        private view.LevelSelect _levelSelect = null;
        private int _levelSelectIndex = 0;

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
            _levelSelect = CreateLevelSelectView();
        }

        void Start()
        {
            _levelSelect.Initialize(this.OnClickLevelSelect);
        }

        public void OnClickLevelSelect(GameObject obj)
        {
            Debug.Log(string.Format("LevelSelectButton : {0}", obj.name));
            _levelSelectIndex = System.Int32.Parse(obj.name);
            _sceneManager.ChangeScene(SceneManager.SCENE.INGAME);
        }

        private view.LevelSelect CreateLevelSelectView()
        {
            return FactoryManager.Instance.InstantiateGameObject<view.LevelSelect>(DefineData.PREFAB_VIEW_LEVELSELECT_PATH, this.transform, Vector3.zero, Quaternion.identity, Vector3.one, "LevelSelect", "UI");
        }
    }
}
