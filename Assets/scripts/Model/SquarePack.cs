using UnityEngine;
using System.Collections.Generic;

namespace model
{
    public class SquarePack
    {
        public SquareCell[] SquareCells { get { return _squareCells; } }

        private readonly int _orderIndex;
        private SquareCell[] _squareCells = new SquareCell[DefineData.MAX_CELL_COUNT];
        //private List<SquareCell> _duplicateCells = new List<SquareCell>();

        public SquarePack(int orderIndex)
        {
            this._orderIndex = orderIndex;
            this.CreateCells(orderIndex);
        }

        public void Initialize(int[] stageNumberArr)
        {
            Debug.Log("SquarePack Model Init");
            for (int i = 0; i < _squareCells.Length; i++)
            {
                _squareCells[i].Initialize(stageNumberArr[i]);
            }
        }

        public model.SquareCell[] GetColumnCells(int column)
        {
            model.SquareCell[] cells = new model.SquareCell[DefineData.MAX_COLUMN_COUNT];

            for(int i=0; i<DefineData.MAX_COLUMN_COUNT; i++)
            {
                cells[i] = _squareCells[column + (DefineData.MAX_COLUMN_COUNT * i)];
            }

            return cells;
        }

        public model.SquareCell[] GetRowCells(int row)
        {
            model.SquareCell[] cells = new model.SquareCell[DefineData.MAX_ROW_COUNT];

            for(int i=0; i<DefineData.MAX_ROW_COUNT; i++)
            {
                cells[i] = _squareCells[row + i];
            }

            return cells;
        }

        public bool isDuplicateNumber()
        {
            for (int i=0; i<_squareCells.Length; i++)
            {
                for (int j = 0; j < _squareCells.Length; j++)
                {
                    if (i == j) continue;

                    if (_squareCells[i].NumberValue == _squareCells[j].NumberValue)
                    {
                        return true;
                    }
                }
            }
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
