using UnityEngine;

namespace model
{
    public class SquarePack
    {
        private readonly int _orderIndex;
        private SquareCell[] _cellArray = new SquareCell[DefineData.MAX_CELL_COUNT];

        public SquarePack(int orderIndex)
        {
            this._orderIndex = orderIndex;
            this.CreateCells();
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



        private void CreateCells()
        {
            for (int i = 0; i < DefineData.MAX_CELL_COUNT; i++)
            {
                _cellArray[i] = new SquareCell(i, 0, false);
            }
        }
    }
}
