using UnityEngine;

namespace model
{
    public class SquareBoard
    {
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

        public SquareCell FindCellByCoordinates(int column, int row)
        {
            SquareCell cell = null;

            int packColumn = column / DefineData.MAX_COLUMN_COUNT;
            int packRow = row / DefineData.MAX_ROW_COUNT;

            int packOrderIndex = (packColumn * DefineData.MAX_ROW_COUNT) + packRow;

            int cellColumn = packColumn;
            int cellRow = packRow;

            int cellOrderIndex = (cellColumn * DefineData.MAX_ROW_COUNT) + cellRow;


            cell = _squarePacks[packOrderIndex].CellArray[cellOrderIndex];

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
