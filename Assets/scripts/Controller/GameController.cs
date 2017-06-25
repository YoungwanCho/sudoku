using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class GameController : MonoBehaviour
    {
        public delegate void DoStack(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber, bool isPreviusMemo, bool isCurrentMemo, int[] previusMemo, int[] currentMemo);
        private scene.InGame _game = null;

        private controller.DoController _doCtrl = null;

        private model.SquareBoard _modelBoard = null;
        private view.SituationBoard _situationBoard = null;
        private view.SquareBoard _viewBoard = null;
        private InputPad _inputPad = null;
        private bool _isMemoMode = false;
        
        public void Awake()
        {
            _doCtrl = new controller.DoController();
            _modelBoard = new model.SquareBoard();
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
            _modelBoard.Initialize(_game.StageData);
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
                model.DoAction peek = _doCtrl.UndoPop();
                OnClickCell(peek.boardCoordinate.column, peek.boardCoordinate.row);
                model.DoAction redo = peek.SwapDoAction();
                _doCtrl.RedoPush(redo);
                _modelBoard.InputNumber(redo.current.isMemoMode, redo.current.number, redo.current.memoArray, null);
            }
            else if (obj.name == "Redo")
            {
                if (_doCtrl.GetRedoStackCount() <= 0)
                {
                    return;
                }

                model.DoAction peek = _doCtrl.RedoPop();
                OnClickCell(peek.boardCoordinate.column, peek.boardCoordinate.row);
                model.DoAction undo = peek.SwapDoAction();
                _doCtrl.UndoPush(undo);
                _modelBoard.InputNumber(undo.current.isMemoMode, undo.current.number, undo.current.memoArray, null);
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
            model.Do previus = new model.Do(previusNumber, isPreviusMemo, previusMemo);
            model.Do current = new model.Do(currentNumber, isCurrentMemo, currentMemo);

            model.DoAction undo = new model.DoAction(boardCoordinate, previus, current);
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
