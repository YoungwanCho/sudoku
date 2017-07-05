using UnityEngine;
using System.Collections.Generic;

namespace model
{
    public class SquareBoard
    {
        public model.SquarePack[] SquarePack { get { return _squarePacks; } }
        public model.SquareCell SelectCell { get { return _selectCell; } }
        public int EmptyCellCount { get { return _emptyCellCount; } }

        private model.SquarePack _selectPack = null;
        private model.SquareCell _selectCell = null;
        private model.SquarePack[] _squarePacks = new model.SquarePack[DefineData.MAX_PACK_COUNT];

        private int _emptyCellCount = 0;

        public SquareBoard()
        {
            CreatePacks();
        }

        public void Initialize(model.StageData stageData)
        {
            int[] packNumberArr = new int[DefineData.MAX_CELL_COUNT];
            int emptyCount = 0;
            for (int i = 0; i < _squarePacks.Length; i++)
            {
                packNumberArr = GetNumberValueOfPackFromStageData(stageData, i);
                _squarePacks[i].Initialize(packNumberArr);
                
                for(int j=0; j<packNumberArr.Length; j++)
                {
                    if(packNumberArr[j] == 0)
                    {
                        emptyCount++;
                    }
                }                
            }
            this._emptyCellCount = emptyCount;
            Debug.Log(string.Format("Init EmptyCellCount : {0}", this._emptyCellCount));
        }

        public void OnSellectCell(int column, int row, System.Action CallBack)
        {
            _selectCell = FindCellByCoordinates(column, row);
            UpdateSelectPack(_selectCell);
            UpdateCellData(_selectCell);
            CallBack();
        }

        public void InputNumber(bool isMemoMode, int number, int[] memoArr, controller.GameController.DoStack UndoStackPush)
        {
            Debug.Log(string.Format("InputNumber(isMemoMode:{0}, number:{1}, memoArr:{2}, UndoStackPush:{3})", isMemoMode, number, memoArr == null, UndoStackPush == null));
            if (_selectCell == null) return;
            if (_selectCell.IsOpenValue) //@TODO: 입력셀이 아닌경우 표시!
            {
                Debug.Log(string.Format("Not Input Cell"));
                return;
            }
            int previusNumber = _selectCell.NumberValue;
            bool isPreviusMemoMode = _selectCell.IsMemoMode;
            int[] previusMemoArr = new int[_selectCell.MemoArray.Length];
            System.Array.Copy(_selectCell.MemoArray, previusMemoArr, _selectCell.MemoArray.Length);
            if (isMemoMode)
            {

                if (memoArr == null) // 인풋 넘버 버튼으로 누른경우
                {
                    if (number == 0) //메모모드중 DeleteKey를 누른경우
                    {
                        Debug.Log("Input number Processing Delete");
                        _selectCell.ClearMemoArray();
                    }
                    else
                    {
                        int memoNumber = number;
                        number = 0;

                        _selectCell.UpdateMemoArray(memoNumber);
                    }
                }
                else // 언두 리두로 복구하는경우 
                {
                    _selectCell.UpdateMemoArray(memoArr);
                }
            }
            else
            {
                _selectCell.ClearMemoArray();
            }

            Debug.Log(string.Format("-------------isMemoMode:{0}, previusNumber:{1}", isMemoMode, previusNumber));

            //@TODO: Memo 언두 예외처리
            if (isMemoMode)
            {
                if (previusNumber != 0)
                {
                    _emptyCellCount++;
                    Debug.Log(string.Format("MemoMode _emptyCellCount : {0}", _emptyCellCount));
                }
            }
            else
            {
                if (previusNumber == 0 && number != 0)
                {
                    _emptyCellCount--;
                }
                else if (number == 0)
                {
                    _emptyCellCount++;
                }
            }

            _selectCell.UpdateNumberValue(number);
            this.UpdateCellData(_selectCell);
            _selectPack.UpdateDuplicateInPack();
            this.UpdateDuplicateInAim(_selectCell);

            if (UndoStackPush != null) //Undo로 입력한 경우에는 UndoStack에 추가하지 않는다.
            {
                int[] currentMemoArr = new int[_selectCell.MemoArray.Length];
                System.Array.Copy(_selectCell.MemoArray, currentMemoArr, _selectCell.MemoArray.Length);
                UndoStackPush(_selectCell.BoardCoorinate, previusNumber, _selectCell.NumberValue, isPreviusMemoMode, _selectCell.IsMemoMode, previusMemoArr, currentMemoArr);
            }
        }

        public bool CheckGameSuccess()
        {
            bool isSuccess = true;
            model.SquarePack targetPack;
            model.SquareCell targetCell;
            for(int i=0; i<_squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for(int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    if(targetCell.IsDuplicatePack || targetCell.IsDuplicateColumn || targetCell.IsDuplicateRow)
                    {
                        isSuccess = false;
                        return isSuccess;
                    }
                }
            }
            return isSuccess;
        }

        private model.SquarePack[] GetColumnPacks(int column)
        {
            model.SquarePack[] packs = new model.SquarePack[DefineData.MAX_COLUMN_COUNT];
            
            for(int i=0; i<packs.Length; i++)
            {
                packs[i] = _squarePacks[column + (DefineData.MAX_COLUMN_COUNT * i)];
            }

            return packs;
        }

        private model.SquarePack[] GetRowPacks(int row)
        {
            model.SquarePack[] packs = new model.SquarePack[DefineData.MAX_ROW_COUNT];
            int startOrderIndex = row * DefineData.MAX_ROW_COUNT;
            for(int i=0; i<packs.Length; i++)
            {
                packs[i] = _squarePacks[startOrderIndex + i];
            }

            return packs;
        }

        private int[] GetNumberValueOfPackFromStageData(model.StageData stagedata, int packIndex)
        {
            int[] numberArr = new int[DefineData.MAX_CELL_COUNT];
            int startIndex = packIndex * DefineData.MAX_CELL_COUNT;
            for(int i = 0; i< DefineData.MAX_CELL_COUNT; i++)
            {  
                numberArr[i] = stagedata.cellDataList[startIndex + i].number;             
            }
            return numberArr;           
        }

        private void UpdateCellData(model.SquareCell selectCell)
        {
            model.SquarePack targetPack = null;
            model.SquareCell targetCell = null;
            for (int i=0; i<_squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for (int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    targetCell.UpdateStateBasedOnSelectedCell(selectCell);        
                }
            }
        }

        private void UpdateDuplicateInAim(SquareCell selectCell)
        {
            UpdateDuplicateNumberOfColumn(selectCell.BoardCoorinate.column);
            UpdateDuplicateNumberOfRow(selectCell.BoardCoorinate.row);
        }

        private void UpdateDuplicateNumberOfRow(int boardRow)
        {
            List<model.SquareCell> duplicateCells = new List<SquareCell>();
            bool isDuplicate = false;
            int targetPackRow = boardRow / DefineData.MAX_ROW_COUNT;
            int targetCellRow = boardRow % DefineData.MAX_ROW_COUNT;

            model.SquarePack[] packs = this.GetRowPacks(targetPackRow);
            model.SquareCell[] cells = new model.SquareCell[DefineData.MAX_CELL_COUNT];
            model.SquareCell[,] tempCells = new model.SquareCell[3, 3];
            model.SquareCell[] packCells = null;

            for (int i = 0; i < packs.Length; i++)
            {
                packCells = packs[i].GetRowCells(targetCellRow);
                for(int j=0; j<packCells.Length; j++)
                {
                    tempCells[i, j] = packCells[j];
                }
            }

            for (int i = 0; i < DefineData.MAX_CELL_COUNT; i++)
            {
                cells[i] = tempCells[i / 3, i % 3];
            }

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].NumberValue ==0)
                {
                    continue;
                }
                for (int j = i; j < cells.Length; j++)
                {
                    if (i == j) // 인덱스가 같은 경우
                        continue;

                    if (cells[i].NumberValue == cells[j].NumberValue)
                    {
                        if(!duplicateCells.Contains(cells[i]))
                        {
                            duplicateCells.Add(cells[i]);
                        }

                        if(!duplicateCells.Contains(cells[j]))
                        {
                            duplicateCells.Add(cells[j]);
                        }
                    }             
                }
            }


            for (int i = 0; i < cells.Length; i++)
            {
                isDuplicate = false;

                for (int j = 0; j < duplicateCells.Count; j++)
                {
                    if(cells[i] == duplicateCells[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                cells[i].UpdateDuplicateRow(isDuplicate);
            }
        }

        private void UpdateDuplicateNumberOfColumn(int boardColumn)
        {
            List<model.SquareCell> duplicateCells = new List<SquareCell>();
            bool isDuplicate = false;
            int targetPackColumn = boardColumn / DefineData.MAX_COLUMN_COUNT;
            int targetCellRow = boardColumn % DefineData.MAX_ROW_COUNT;

            model.SquarePack[] packs = this.GetColumnPacks(targetPackColumn);
            model.SquareCell[] cells = new model.SquareCell[DefineData.MAX_CELL_COUNT];
            model.SquareCell[,] tempCells = new SquareCell[3, 3];
            model.SquareCell[] packCells = null;

            for (int i = 0; i < packs.Length; i++)
            {
                packCells = packs[i].GetColumnCells(targetPackColumn);
                for(int j=0; j<packCells.Length; j++)
                {
                    tempCells[i, j] = packCells[j];
                }
            }

            for (int i = 0; i < DefineData.MAX_CELL_COUNT; i++)
            {
                cells[i] = tempCells[i / 3, i % 3];
            }

            for (int i = 0; i < cells.Length; i++)
            {
                if(cells[i].NumberValue ==0)
                {
                    continue;
                }
                for (int j = i; j < cells.Length; j++)
                {
                    if (i == j) // 인덱스가 같은경우
                        continue;

                    if(cells[i].NumberValue == cells[j].NumberValue)
                    {
                        if(!duplicateCells.Contains(cells[i]))
                        {
                            duplicateCells.Add(cells[i]);
                        }

                        if(!duplicateCells.Contains(cells[j]))
                        {
                            duplicateCells.Add(cells[j]);
                        }
                    }
                }
            }

            for (int i = 0; i < cells.Length; i++)
            {
                isDuplicate = false;
                for (int j = 0; j < duplicateCells.Count; j++)
                {
                    if (cells[i] == duplicateCells[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                cells[i].UpdateDuplicateColumn(isDuplicate);
            }
        }

        private void UpdateSelectPack(model.SquareCell cell)
        {
           _selectPack = _squarePacks[cell.PackIndex]; 
        }

        private SquareCell FindCellByCoordinates(int column, int row)
        {
            SquareCell cell = null;

            int packColumn = column / DefineData.MAX_COLUMN_COUNT;
            int packRow = row / DefineData.MAX_ROW_COUNT;

            int packOrderIndex = (packRow * DefineData.MAX_ROW_COUNT) + packColumn;

            int cellColumn = column % DefineData.MAX_COLUMN_COUNT;
            int cellRow = row % DefineData.MAX_ROW_COUNT;

            int cellOrderIndex = (cellRow * DefineData.MAX_ROW_COUNT) + cellColumn;


            cell = _squarePacks[packOrderIndex].SquareCells[cellOrderIndex];

            Debug.Log(string.Format("parameter : [{0}, {1}] - Cell Coordinate : [{2}, {3}]", 
                column, row, cell.BoardCoorinate.column, cell.BoardCoorinate.row));
            
            return cell;
        }

        private void CreatePacks()
        {
            for (int i = 0; i < _squarePacks.Length; i++)
            {
                _squarePacks[i] = new model.SquarePack(i);
            }
        }

        
    }
}
