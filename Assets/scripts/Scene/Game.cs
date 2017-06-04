using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene
{

    public class Game : MonoBehaviour, IScene
    {
		private controller.ModelController _modelCtrl = null;
		private controller.ViewController _viewCtrl = null;

        public void Start()
        {
            Debug.Log("Game Start()");
            _viewCtrl = CreateViewController();
        }

		public void Enter()
        {
        }

        public void Exit()
        {   
        }

        private controller.ViewController CreateViewController()
        {
            //Debug.Log("CreateViewController");
            GameObject obj = Instantiate(new GameObject(), this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "ViewController";
            return obj.AddComponent<controller.ViewController>();            
        }

    }
}