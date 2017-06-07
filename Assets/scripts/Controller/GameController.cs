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

        public void Awake()
        {
            _modelBoard = new model.SquareBoard();
            _viewBoard = CreateSquareBoard();
            _inputPad = CreateInputPad();
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

        public void Initialize(scene.Game game)
        {
            this._game = game;
            _modelBoard.Initialize();
            _viewBoard.Initialize(this.OnClickCell);
            _inputPad.Initialize(OnClickInputValueButton);
        } 

        public void OnClickCell(int column, int row)
        {
            UnityEngine.Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
            _modelBoard.SelectCell(column, row, UpdateView);
        }

        public void OnClickInputValueButton(UnityEngine.Object obj)
        {
            Debug.Log(string.Format("InputValueButton : {0}", obj.name));
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


    }
}
