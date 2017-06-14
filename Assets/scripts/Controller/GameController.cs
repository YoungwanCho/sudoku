using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class GameController : MonoBehaviour
    {
        public delegate void OnClick(int n, int n2);
        public delegate void OnClickInputPad(UnityEngine.Object obj);
        private scene.Game _game = null;

        private model.SquareBoard _modelBoard = null;
        private view.SquareBoard _viewBoard = null;
        private InputPad _inputPad = null;
        private model.StageData _stageData = null;

        public void Awake()
        {
            _modelBoard = new model.SquareBoard();
            _viewBoard = CreateSquareBoard();
            _inputPad = CreateInputPad();
            _stageData = LoadStageData("stage1");
        }

        // Use this for initialization
        void Start()
        {
            OnClickCell(4, 4);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGUI()
        {
            if(GUI.Button(new Rect(200, 200, 200, 200), "StageData Save"))
            {
                new controller.StageEditor().MapSave(_modelBoard);                
            }

            if(GUI.Button(new Rect(400, 200, 200, 200), "Empty Cell "))
            { 
                OnClickInputValueButton(new GameObject("0"));
            }

            if(GUI.Button(new Rect(600, 200, 200, 200), "StageData Load"))
            {
                LoadStageData("stage1");
            }
        }

        public void Initialize(scene.Game game)
        {
            this._game = game;
            _modelBoard.Initialize(this._stageData);
            _viewBoard.Initialize(this.OnClickCell);
            _inputPad.Initialize(this.OnClickInputValueButton);
        } 

        public void OnClickCell(int column, int row)
        {
            UnityEngine.Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
            _modelBoard.OnSellectCell(column, row, this.UpdateView);
        }

        public void OnClickInputValueButton(UnityEngine.Object obj)
        {
            
            Debug.Log(string.Format("InputValueButton : {0}", obj.name));
            int number = System.Int32.Parse(obj.name);
            _modelBoard.InputNumber(number);
            UpdateView();
        }

        public void UpdateView()
        {
            _viewBoard.UpdateBoardAim(this._modelBoard);
            _viewBoard.UpdateBoardValue(this._modelBoard);
        } 

        public void OnClickNumberButton(int value)
        {
            UnityEngine.Debug.Log("OnClick Input Button Value : " + value);
        }

        private view.SquareBoard CreateSquareBoard()
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUAREBOARD_PATH) as GameObject;
            GameObject obj = Instantiate(prefab, this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "SquareBoard";

            return obj.GetComponent<view.SquareBoard>();
        }

        private InputPad CreateInputPad()
        {
            GameObject obj = Instantiate(new GameObject(), this.transform);
            obj.transform.localPosition = new Vector3(0.0f, -600.0f, 0.0f);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "InputPad";
            return obj.AddComponent<InputPad>();
        }

        private model.StageData LoadStageData(string stageName)
        {
            controller.StageData data = new controller.StageData();
            return data.LoadStageData(stageName);
        }


    }
}
