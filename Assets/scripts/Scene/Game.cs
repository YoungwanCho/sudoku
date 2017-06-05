using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene
{

    public class Game : MonoBehaviour, IScene
    {
		private controller.ModelController _modelCtrl = null;
		private controller.ViewController _viewCtrl = null;
        private controller.InputController _inputCtrl = null;

        private model.SquareCell _modelSelectCell = null;
        private view.SquareCell _viewSelectCell = null;

        public void Awake()
        {
            Debug.Log("Game Start()");
            _modelCtrl = CreateModelController();
            _viewCtrl = CreateViewController();
            _inputCtrl = CreateInputController(_modelCtrl, _viewCtrl);          
            _modelSelectCell = null;
        }

        public void Start()
        {
            _viewCtrl.Initialize(_inputCtrl.OnClickCell);
        } 

        public void Initialize()
        {

        } 

		public void Enter()
        {
        }

        public void Exit()
        {   
        }

        //public void OnClickCell(int column, int row)
        //{
        //    Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
        //    //ModelUpdateSelectCell(column, row);
        //}

        public void ModelUpdateSelectCell(int column, int row)
        {
            _modelSelectCell = _modelCtrl.FindCellByCoordinates(column, row);
            _viewSelectCell = _viewCtrl.FindCellByCoordinates(column, row);
            


            _viewSelectCell.UIUpdate(18, "cell_green");
            //_selectCell


        }

        private controller.InputController CreateInputController(controller.ModelController model, controller.ViewController view)
        {
            return new controller.InputController(model, view);
        }

        private controller.ModelController CreateModelController()
        {
            return new controller.ModelController();
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