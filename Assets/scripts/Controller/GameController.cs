﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class GameController : MonoBehaviour
    {
        public delegate void DoStack(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber, bool isPreviusMemo, bool isCurrentMemo, int[] previusMemo, int[] currentMemo);
        private scene.InGame _game = null;

        private controller.DoController _doCtrl = null;

        private model.StageData _stageData = null;
        private model.SquareBoard _modelBoard = null;
        private view.SituationBoard _situationBoard = null;
        private view.SquareBoard _viewBoard = null;
        private InputPad _inputPad = null;
        private bool _isMemoMode = false;
        
        public void Awake()
        {
            _doCtrl = new controller.DoController();
            _modelBoard = new model.SquareBoard();
            _stageData = LoadStageData("stage1");
            _viewBoard = CreateSquareBoard();
            _inputPad = CreateInputPad();
            _situationBoard = CreateSituationBoard();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        //private void OnGUI()
        //{
        //    if(GUI.Button(new Rect(200, 200, 200, 200), "StageData Save"))
        //    {
        //        new controller.StageEditor().MapSave(_modelBoard);                
        //    }

        //    if(GUI.Button(new Rect(600, 200, 200, 200), "StageData Load"))
        //    {
        //        LoadStageData("stage1");
        //    }
        //}

        public void Initialize(scene.InGame game)
        {
            this._game = game;
            _modelBoard.Initialize(this._stageData);
            _viewBoard.Initialize(this.OnClickCell);
            _inputPad.Initialize(this.OnClickInputNumberButton, this.OnClickDoAction, this.OnClickMemo, this.OnClickDelete, this.OnClickQuit);
            _doCtrl.Initialize();
            OnClickCell(4, 4);
        }

        public void OnClickQuit(GameObject obj)
        {
            Debug.Log("OnClick Quit Button");
            _game.OnClickQuit(obj);
        } 

        public void OnClickDelete(GameObject obj)
        {
            Debug.Log("OnClickDelete");
            OnClickInputNumberButton(new GameObject("0"));
        }
        
        public void OnClickMemo(GameObject obj)
        {
            Debug.Log("OnClickMemo");
            UpdateMemoMode(!_isMemoMode);  
        }

        public void OnClickCell(int column, int row)
        {
            UnityEngine.Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
            _modelBoard.OnSellectCell(column, row, this.UpdateView);
        }

        public void OnClickInputNumberButton(GameObject obj)
        {
            Debug.Log(string.Format("InputValueButton : {0}", obj.name));
            int number = System.Int32.Parse(obj.name);               
            _modelBoard.InputNumber(_isMemoMode, number, null, this.UndoStackPush);
            this.UpdateMemoMode(_isMemoMode);
            UpdateView();

            if(_modelBoard.EmptyCellCount == 0)
            {
                if(_modelBoard.CheckGameSuccess())
                {
                    _game.ClearGame(null); //@TODO: 기록정보를 전달해준다!
                    Debug.Log("Game Clear");
                }
            }
        }

        public void OnClickDoAction(GameObject obj)
        {
            Debug.Log(string.Format("OnClick DoAction : {0}", obj.name));
            if (obj.name == "Undo")
            {
                if (_doCtrl.GetUndoStackCount() <= 0)
                {
                    return;
                }

                model.Do peek = _doCtrl.UndoPop();
                peek.PrintDo();
                int column = peek.boardCoordinate.column;
                int row = peek.boardCoordinate.row;
                OnClickCell(column, row);
                int previusNumber = peek.previusNumber;
                int currentNumber = peek.currentNumber;
                int[] previusMemoArr = new int[peek.previusMemoArray.Length];
                int[] currentMemoArr = new int[peek.currentMemoArray.Length];
                System.Array.Copy(peek.previusMemoArray, previusMemoArr, peek.previusMemoArray.Length);
                System.Array.Copy(peek.currentMemoArray, currentMemoArr, peek.currentMemoArray.Length);
                model.Do redo = new model.Do(peek.boardCoordinate, currentNumber, previusNumber, peek.isCurrentMemoMode, peek.isPreviusMemoMode, currentMemoArr, previusMemoArr);
                _doCtrl.RedoPush(redo);
                _modelBoard.InputNumber(peek.isPreviusMemoMode, previusNumber, peek.previusMemoArray, null);
            }
            else if (obj.name == "Redo")
            {
                if (_doCtrl.GetRedoStackCount() <= 0)
                {
                    return;
                }

                model.Do peek = _doCtrl.RedoPop();
                peek.PrintDo();
                int column = peek.boardCoordinate.column;
                int row = peek.boardCoordinate.row;
                OnClickCell(column, row);
                int previusNumber = peek.previusNumber;
                int currentNumber = peek.currentNumber;
                int[] previusMemoArr = new int[peek.previusMemoArray.Length];
                int[] currentMemoArr = new int[peek.currentMemoArray.Length];
                System.Array.Copy(peek.previusMemoArray, previusMemoArr, peek.previusMemoArray.Length);
                System.Array.Copy(peek.currentMemoArray, currentMemoArr, peek.currentMemoArray.Length);
                model.Do undo = new model.Do(peek.boardCoordinate, currentNumber, previusNumber, peek.isCurrentMemoMode, peek.isPreviusMemoMode, currentMemoArr, previusMemoArr);
                _doCtrl.UndoPush(undo);
                _modelBoard.InputNumber(peek.isPreviusMemoMode, previusNumber, peek.previusMemoArray, null);        
            }
            UpdateView();
        }

        public void UpdateMemoMode(bool isOn)
        {
            _isMemoMode = isOn;
            _inputPad.UpdateMemoButton(_isMemoMode);
            Debug.Log(string.Format("MemoMode : {0}", _isMemoMode));
        }

        public void UndoStackPush(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber, bool isPreviusMemo, bool isCurrentMemo, int[] previusMemo, int[] currentMemo)
        {
            model.Do undo = new model.Do(boardCoordinate, previusNumber, currentNumber, isPreviusMemo, isCurrentMemo, previusMemo, currentMemo);
            _doCtrl.UndoPush(undo);
        }

        public void UpdateView()
        {
            _viewBoard.UpdateBoardAim(this._modelBoard);
            _viewBoard.UpdateBoardValue(this._modelBoard);
            _situationBoard.UpdateEmptyCellCount(_modelBoard.EmptyCellCount);
        }

        private view.SquareBoard CreateSquareBoard()
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUAREBOARD_PATH) as GameObject;
            GameObject obj = Instantiate(prefab, this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "SquareBoard";
            obj.layer = LayerMask.NameToLayer("UI");

            return obj.GetComponent<view.SquareBoard>();
        }

        private InputPad CreateInputPad()
        {
            GameObject obj = Instantiate(new GameObject(), this.transform);
            obj.AddComponent<RectTransform>();
            obj.transform.localPosition = new Vector3(0.0f, -600.0f, 0.0f);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "InputPad";
            obj.layer = LayerMask.NameToLayer("UI");

            return obj.AddComponent<InputPad>();
        }

        private model.StageData LoadStageData(string stageName)
        {
            controller.StageData data = new controller.StageData();
            return data.LoadStageData(stageName);
        }

        private view.SituationBoard CreateSituationBoard()
        {
            GameObject prefabs = Resources.Load(DefineData.PREFAB_SITUATIONBOARD_PATH) as GameObject;
            GameObject obj = Instantiate(prefabs, this.transform) as GameObject;
            obj.transform.localPosition = new Vector3(0.0f, -800, 0.0f);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "SituationBoard";
            obj.layer = LayerMask.NameToLayer("UI");

            return obj.GetComponent<view.SituationBoard>();
        }


    }
}
