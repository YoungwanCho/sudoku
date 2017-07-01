using UnityEngine;

namespace model
{
    public class SquareCell
    {
        public int[] MemoArray { get { return this._memoArray; } }
        public int OrderIndex { get { return this._orderIndex; } }
        public int NumberValue { get { return this._numberValue; } }
        public int PackIndex { get { return this._packIndex; } }

        public bool IsMemoMode { get { return this._isMemoMode; } }
        public bool IsOpenValue { get { return this._isOpenValue; } }
        public bool IsDuplicatePack { get { return this._isDuplicatePack; } }
        public bool IsDuplicateColumn { get { return this._isDuplicateColumn; } }
        public bool IsDuplicateRow { get { return this._isDuplicateRow; } }
        public bool IsSelectCell { get { return this._isSelectCell; } }
        public bool IsEqualNumber { get { return this._isEqualNumber; } }
        public bool IsEqualColumn { get { return this._isEqualColumn; } }
        public bool IsEqualRow { get { return this._isEqualRow; } }

        public BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }

        private readonly BoardCoordinate _boardCoordinate;
        private readonly int _packIndex;
        private readonly int _orderIndex;
        private readonly int _column;
        private readonly int _row;

        private int _numberValue = 0;
        private bool _isOpenValue = false;
        private bool _isDuplicatePack = false;
        private bool _isDuplicateColumn = false;
        private bool _isDuplicateRow = false;
        private bool _isMemoMode = false;
        private bool _isSelectCell = false;
        private bool _isEqualNumber = false;
        private bool _isEqualColumn = false;
        private bool _isEqualRow = false;
        private int[] _memoArray = new int[9];

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
            this._isDuplicatePack = false;
            this._isDuplicateColumn = false;
            this._isDuplicateRow = false;

            this._isSelectCell = false;
            this._isEqualNumber = false;
            this._isEqualColumn = false;
            this._isEqualRow = false;

            this._isMemoMode = false;

            ClearMemoArray();

        }

        public void UpdateStateBasedOnSelectedCell(model.SquareCell selectCell)
        {
			this._isSelectCell = (selectCell.BoardCoorinate.column == this.BoardCoorinate.column && selectCell.BoardCoorinate.row == this.BoardCoorinate.row);
            this._isEqualNumber = this._numberValue == 0 ? false : (selectCell.NumberValue == this._numberValue);
            this._isEqualColumn = (selectCell.BoardCoorinate.column == this._boardCoordinate.column);
            this._isEqualRow = (selectCell.BoardCoorinate.row == this._boardCoordinate.row);

            if(this._isOpenValue)
            {
                return;
            }
        }

        public void UpdateMemoArray(int number)
        {
            if (_isOpenValue || !(1 <= number && number <= 9))
            {
                return;
            }

            int arrIndex = number - 1;
            int previusNumber = _memoArray[arrIndex];

            if(previusNumber != number) // 입력처리
            {
                _memoArray[arrIndex] = number;
            }
            else // 취소처리
            {
                _memoArray[arrIndex] = 0;
            }

            this.UpdateMemoMode();            
        }

        public void UpdateMemoArray(int[] memoArr) // 복구용
        {
            if(_isOpenValue)
            {
                return;
            }

            for(int i=0; i< _memoArray.Length; i++)
            {
                this._memoArray[i] = memoArr == null ? 0 : memoArr[i];
            }
            this.UpdateMemoMode();
        }

        public void UpdateNumberValue(int number)
        {
            this._numberValue = number;
        }

		public void ClearMemoArray()
		{
			for (int i = 0; i < _memoArray.Length; i++)
			{
				_memoArray[i] = 0;
			}
			_isMemoMode = false;
		}

        public void UpdateDuplicatePack(bool isOn)
        {
            if (this._isOpenValue)
            {
                //Debug.Log(string.Format("오픈 값은 중복처리 대상이 아닙니다 PackIndex : {0}, CellIndex : {1}, Number : {2}", PackIndex, _orderIndex, _numberValue));
                return;
            }

            if(this._numberValue == 0)
            {
                this._isDuplicatePack = false;
            }
            else
            {
                this._isDuplicatePack = isOn;
            }

            if(_isDuplicatePack)
            {
                Debug.Log(string.Format("Pack DuplicateState - PackIndex :{0}, State : {1},  [{2}, {3}] Number : {4}",
                    this.PackIndex, isOn, _boardCoordinate.column, _boardCoordinate.row, this._numberValue));
            }
        }

        public void UpdateDuplicateColumn(bool isOn)
        {
            if(this._isOpenValue)
            {
                return;
            }

            if(this._numberValue == 0)
            {
                this._isDuplicateColumn = false;
            }
            else
            {
                this._isDuplicateColumn = isOn;
            }

            if(_isDuplicateColumn)
            {
                Debug.Log(string.Format("Column DuplicateState - PackIndex :{0}, State : {1},  [{2}, {3}] Number : {4}",
                    this.PackIndex, isOn, _boardCoordinate.column, _boardCoordinate.row, this._numberValue));
            }
        }

        public void UpdateDuplicateRow(bool isOn)
        {
            if (this._isOpenValue)
            {
                return;
            }

            if (this._numberValue == 0)
            {
                this._isDuplicateRow = false;
            }
            else
            {
                this._isDuplicateRow = isOn;
            }

            if (_isDuplicateRow)
            {
                Debug.Log(string.Format("Row DuplicateState - PackIndex :{0}, State : {1},  [{2}, {3}] Number : {4}",
                    this.PackIndex, isOn, _boardCoordinate.column, _boardCoordinate.row, this._numberValue));
            }
        }

        private bool UpdateMemoMode()
        {
            _isMemoMode = false;
            for (int i=0; i<_memoArray.Length; i++)
            {
                if (_memoArray[i] != 0)
                {
                    _isMemoMode = true;
                    Debug.Log(string.Format("Memo Mode Cell : [{0}, {1}]", this._boardCoordinate.column, _boardCoordinate.row));
                    break;    
                }
            }
            return _isMemoMode;
        }

    }
}
