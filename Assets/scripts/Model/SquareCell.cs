using UnityEngine;

namespace model
{
    public class SquareCell
    {
        public int OrderIndex { get { return this._orderIndex; } }
        public int NumberValue { get { return this._numberValue; } }
        public int PackIndex { get { return this._packIndex; } }
        public bool IsOpenValue { get { return this._isOpenValue; } }
        public bool IsDuplicate { get { return this._isDuplicate; } }
        public BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }

        private readonly BoardCoordinate _boardCoordinate;
        private readonly int _packIndex;
        private readonly int _orderIndex;
        private readonly int _column;
        private readonly int _row;

        private int _numberValue = 0;
        private bool _isOpenValue = false;
        private bool _isDuplicate = false;

        public SquareCell(int packIndex, int orderIndex)
        {
            this._packIndex = packIndex;
            this._orderIndex = orderIndex;
            this._column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            this._row = orderIndex / DefineData.MAX_ROW_COUNT;
            this._boardCoordinate = new BoardCoordinate(packIndex, this._column, this._row);
        }

        public void Initialize(int number)
        {
            this.UpdateNumberValue(number);
            this._isOpenValue = number != 0;
            this._isDuplicate = false;
        }

        public void UpdateNumberValue(int number)
        {
            this._numberValue = number;
        }

        public void UpdateDuplicateState(bool isOn)
        {
            if (this._isOpenValue)
            {
                Debug.Log("오픈 값은 중복처리 대상이 아닙니다");
                return;
            }

            if(this.NumberValue == 0)
            {
                this._isDuplicate = false;
            }
            else
            {
                Debug.Log(string.Format("DuplicateState - PackIndex :{0}, State : {1},  [{2}, {3}] Number : {4}",
                    this.PackIndex, isOn, _boardCoordinate.column, _boardCoordinate.row, this._numberValue));

                this._isDuplicate = isOn;
            }
        }

        public Color GetTextColor()
        {
            if (_isOpenValue)
                return Color.blue;

            if (_isDuplicate)
                return Color.red;

            return Color.green;
        }
    }
}
