using UnityEngine;

namespace model
{
    public class SquarePack
    {
        public SquareCell[] SquareCells { get { return _squareCells; } }
        private readonly int _orderIndex;
        private SquareCell[] _squareCells = new SquareCell[DefineData.MAX_CELL_COUNT];

        public SquarePack(int orderIndex)
        {
            this._orderIndex = orderIndex;
            this.CreateCells(orderIndex);
            this.Initialize();
        }

        public void Initialize()
        {
            Debug.Log("SquarePack Model Init");
            for (int i = 0; i < _squareCells.Length; i++)
            {
                _squareCells.Initialize();
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
                _squareCells[i] = new SquareCell(packIndex, i);
            }
        }
    }
}
