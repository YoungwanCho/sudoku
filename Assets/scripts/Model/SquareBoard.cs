using UnityEngine;
using System.Collections.Generic;

namespace model
{
    public class SquareBoard
    {
        public model.SquareCell[] EqualColumnCells { get { return _equalColumnCells; } }
        public model.SquareCell[] EqaulRowCells { get { return _equalRowCells; } }
        public List<model.SquareCell> EqaulValueCells { get { return _equalValueCells; } }
        public model.SquarePack[] SquarePack { get { return _squarePacks; } }

        private model.SquarePack _selectPack = null;
        private model.SquareCell _selectCell = null;
        private model.SquareCell[] _equalColumnCells = new model.SquareCell[DefineData.MAX_CELL_COUNT - 1];
        private model.SquareCell[] _equalRowCells = new model.SquareCell[DefineData.MAX_CELL_COUNT - 1];
        private List<model.SquareCell> _equalValueCells = new List<SquareCell>();
        
        private model.SquarePack[] _squarePacks = new model.SquarePack[DefineData.MAX_PACK_COUNT];

        public SquareBoard()
        {
            CreatePacks();
        }

        public void Initialize(model.StageData stageData)
        {
            int[] packNumberArr = new int[DefineData.MAX_CELL_COUNT];

            for (int i = 0; i < _squarePacks.Length; i++)
            {
                packNumberArr = GetNumberValueOfPackFromStageData(stageData, i);
                _squarePacks[i].Initialize(packNumberArr);
            }
        }

        public void SelectCell(int column, int row, System.Action CallBack)
        {
            _selectCell = FindCellByCoordinates(column, row);
            UpdateSelectPack(_selectCell);
            if (_selectCell.NumberValue == 0) // ºóÄ­À» ¼±ÅÃÇÑ°æ¿ì
            {

            }
            else
            {

            }
            UpdateCellData(_selectCell);
            CallBack();
        }

        public void InputNumber(int number)
        {
            if (_selectCell == null) return;
            if (_selectCell.IsOpenValue) //@TODO: ÀÔ·Â¼¿ÀÌ ¾Æ´Ñ°æ¿ì Ç¥½Ã!
            {
                Debug.Log(string.Format("ÀÔ·Â¼¿ÀÌ ¾Æ´Õ´Ï´Ù."));
                return;
            }
            
            _selectCell.UpdateNumberValue(number);
            UpdateCellData(_selectCell);

            _selectPack.UpdateDuplicateInPack();
            this.UpdateDuplicateInAim(_selectCell);

            model.SquarePack pack = null;
            model.SquareCell cell = null;
            for(int i=0; i<_squarePacks.Length; i++)
            {
                pack = _squarePacks[i];
                for(int j=0; j<pack.SquareCells.Length; j++)
                {
                    cell = pack.SquareCells[j];
                    if(cell.IsDuplicate)
                    {
                        Debug.Log(string.Format("Duplicate : [{0}, {1}] PackIndex : {2} Number : {3} ",
                            cell.BoardCoorinate.column, cell.BoardCoorinate.row, cell.PackIndex, cell.NumberValue));
                    }
                }
            }
        }

        public bool CheckGameSuccess()
        {
            for(int i=0; i<_squarePacks.Length; i++)
            {
            }
            return true;
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

            for(int i=0; i<packs.Length; i++)
            {
                packs[i] = _squarePacks[row + i];
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
            int equalColumnCount = 0;
            int equalRowCount = 0;
            _equalValueCells.Clear();

            for (int i=0; i<_squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for (int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];

                    if(targetCell.BoardCoorinate.column == selectCell.BoardCoorinate.column && targetCell.BoardCoorinate.row == selectCell.BoardCoorinate.row) // ¼¿·ºÆ®¼¿ ÀÌ¶ó¸é ÆÐ½º (°´Ã¼ ÀÌÄ÷¿¬»êÇÏÀÚ)
                    {
                        continue;
                    }
                    
                    if(targetCell.BoardCoorinate.column == selectCell.BoardCoorinate.column)
                    {
                        _equalColumnCells[equalColumnCount] = targetCell;
                        equalColumnCount++;
                    }
                    else if(targetCell.BoardCoorinate.row == selectCell.BoardCoorinate.row)
                    {
                        _equalRowCells[equalRowCount] = targetCell;
                        equalRowCount++;
                    }
                    
                    if(selectCell.NumberValue == 0) // ºóÄ­ÀÌ¶ó¸é
                    {
                        continue;
                    }
                    else if(targetCell.NumberValue == selectCell.NumberValue)
                    {
                        _equalValueCells.Add(targetCell);
                    }                      
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
                for (int j = 0; j < cells.Length; j++)
                {
                    if (i == j) // ÀÎµ¦½º°¡ °°Àº °æ¿ì
                        continue;

                    if (cells[i].NumberValue == 0 || cells[i].NumberValue == cells[j].NumberValue)
                    {
                        cells[j].UpdateDuplicateState(true);
                    }
                }
            }
        }

        private void UpdateDuplicateNumberOfColumn(int boardColumn)
        {
            int targetPackColumn = boardColumn / DefineData.MAX_COLUMN_COUNT;
            int targetCellRow = boardColumn % DefineData.MAX_COLUMN_COUNT;

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
                for (int j = 0; j < cells.Length; j++)
                {
                    if (i == j) // ÀÎµ¦½º°¡ °°Àº°æ¿ì
                        continue;

                    if (cells[i].NumberValue == 0 || cells[i].NumberValue == cells[j].NumberValue)
                    {
                        cells[j].UpdateDuplicateState(true);
                    }
                }
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
