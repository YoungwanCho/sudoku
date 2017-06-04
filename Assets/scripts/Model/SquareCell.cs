using UnityEngine;

namespace model
{
    public class SquareCell
    {
        public int OrderIndex { get { return this._orderIndex; } }
        public int NumberValue { get { return this._numberValue; } }

        private readonly int _orderIndex;
        private int _numberValue = 0;
        private bool _isOpenValue = false;

        public SquareCell(int cellIndex, int numberValue, bool isOpenValue)
        {
            this._orderIndex = cellIndex;
            this._numberValue = numberValue;
            this._isOpenValue = isOpenValue;
            Debug.Log(string.Format("{0}, {1}, {2}", cellIndex, numberValue, isOpenValue)); 
        }

        public void Initialize()
        {
            Debug.Log("SquareCell Model Init");

            this._isOpenValue = false;
            this._numberValue = 0;
        }
    }
}
