using UnityEngine;
using System.Collections.Generic;

namespace model
{
    public class SquarePack
    {
        public SquareCell[] SquareCells { get { return _squareCells; } }
                
        private SquareCell[] _squareCells = new SquareCell[DefineData.MAX_CELL_COUNT];

        private readonly int _orderIndex;

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
            int startIndex = DefineData.MAX_ROW_COUNT * row;
            for(int i=0; i<DefineData.MAX_ROW_COUNT; i++)
            {
                cells[i] = _squareCells[startIndex + i];
            }

            return cells;
        }

        public void UpdateDuplicateInPack()
        {
            List<SquareCell> duplicateCells = new List<SquareCell>();
            bool isDuplicate = false;
            for (int i=0; i<_squareCells.Length; i++)
            {
                if (_squareCells[i].NumberValue == 0)
                {
                    continue;
                }

                for (int j = i; j < _squareCells.Length; j++)
                {
                    if (i == j) continue;
                    if (_squareCells[i].NumberValue == _squareCells[j].NumberValue)
                    {
                        if (!duplicateCells.Contains(_squareCells[i]))
                        {
                            duplicateCells.Add(_squareCells[i]);
                        }

                        if (!duplicateCells.Contains(_squareCells[j]))
                        {
                            duplicateCells.Add(_squareCells[j]);
                        }
                    }
                }
            }

            for (int i = 0; i < _squareCells.Length; i++)
            {
                isDuplicate = false;
                for (int j = 0;  j < duplicateCells.Count; j++)
                {
                    if (_squareCells[i] == duplicateCells[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                _squareCells[i].UpdateDuplicatePack(isDuplicate);
            }
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
