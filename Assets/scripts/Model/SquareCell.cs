using UnityEngine;

namespace model
{
    public class SquareCell
    {
        public int OrderIndex { get { return this._orderIndex; } }
        public int NumberValue { get { return this._numberValue; } }
        public int PackIndex { get { return this._packIndex; } }
        public BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }

        private readonly BoardCoordinate _boardCoordinate;
        private readonly int _packIndex;
        private readonly int _orderIndex;
        private readonly int _column;
        private readonly int _row;

        private int _numberValue = 0;
        private bool _isOpenValue = false;

        public SquareCell(int packIndex, int orderIndex)
        {
            this._orderIndex = orderIndex;
            this._column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            this._row = orderIndex / DefineData.MAX_ROW_COUNT;
            this._boardCoordinate = new BoardCoordinate(packIndex, this._column, this._row);

            this.Initialize(orderIndex, false); //@TEST
        }

        public void Initialize(int numberValue, bool isOpenValue)
        {
            this._numberValue = numberValue;
            this._isOpenValue = isOpenValue;

        }

    }
}
