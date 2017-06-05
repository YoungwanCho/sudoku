using UnityEngine;

namespace model
{
    public class SquarePack
    {
        public SquareCell[] CellArray { get { return _cellArray; } }
        private readonly int _orderIndex;
        private SquareCell[] _cellArray = new SquareCell[DefineData.MAX_CELL_COUNT];

        public SquarePack(int orderIndex)
        {
            this._orderIndex = orderIndex;
            this.CreateCells(orderIndex);
            this.Initialize();
        }

        public void Initialize()
        {
            Debug.Log("SquarePack Model Init");
            for (int i = 0; i < _cellArray.Length; i++)
            {
                _cellArray.Initialize();
            }
        }

        public bool CheckDuplicateNumberValues()
        {
            return false;
        }

        private void CreateCells(int packIndex)
        {
            for (int i = 0; i < DefineData.MAX_CELL_COUNT; i++)
            {
                _cellArray[i] = new SquareCell(packIndex, i);
            }
        }
    }
}
