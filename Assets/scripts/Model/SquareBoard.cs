using UnityEngine;

namespace model
{
    public class SquareBoard
    {
        public model.SquareCell[] EqualColumnCells { get { return _equalColumnCells; } }
        public model.SquareCell[] EqaulRowCells { get { return _equalRowCells; } }
        public model.SquareCell[] EqaulValueCells { get { return _equalValueCells; } }
        public model.SquarePack[] SquarePack { get { return _squarePacks; } }

        private model.SquarePack _selectPack = null;
        private model.SquareCell _selectCell = null;
        private model.SquareCell[] _equalColumnCells = new model.SquareCell[DefineData.MAX_CELL_COUNT - 1];
        private model.SquareCell[] _equalRowCells = new model.SquareCell[DefineData.MAX_CELL_COUNT - 1];
        private model.SquareCell[] _equalValueCells = new model.SquareCell[DefineData.MAX_CELL_COUNT - 1];
        private model.SquarePack[] _squarePacks = new model.SquarePack[DefineData.MAX_PACK_COUNT];

        public SquareBoard()
        {
            CreatePacks();
            this.Initialize();
        }

        public void Initialize()
        {
            Debug.Log("Square Board Model Init");
            for (int i = 0; i < _squarePacks.Length; i++)
            {
                _squarePacks[i].Initialize();
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

        private void UpdateCellData(model.SquareCell selectCell)
        {
            model.SquarePack targetPack = null;
            model.SquareCell targetCell = null;
            int equalColumnCount = 0;
            int equalRowCount = 0;
            int equalValueCount = 0; 
             
            for(int i=0; i<_squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for (int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];

                    //if(targetCell.BoardCoorinate.column != column && targetCell.BoardCoorinate.row != row)
                    //{
                    //    continue;
                    //}
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
                    
                    if(targetCell.NumberValue == selectCell.NumberValue)
                    {
                        _equalValueCells[equalValueCount] = targetCell;
                        equalValueCount++;
                    }                      
                }
            }
        }

        private void UpdateSelectPack(model.SquareCell cell)
        {
           _selectPack= _squarePacks[cell.PackIndex]; 
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
