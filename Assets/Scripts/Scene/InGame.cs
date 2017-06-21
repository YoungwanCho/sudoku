using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene
{

    public class InGame : MonoBehaviour, IScene
    {
        private controller.GameController _gameController = null;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Initialize(SceneManager manager)
        {
        }

        public void Awake()
        {
            Debug.Log("Game Start()");
            _gameController = CreateGameController();
        }

        public void Start()
        {
            _gameController.Initialize(this);
        }



        private controller.GameController CreateGameController()
        {
            //Debug.Log("CreateViewController");
            GameObject obj = Instantiate(new GameObject(), this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "GameController";
            return obj.AddComponent<controller.GameController>();            
        }

    }
}