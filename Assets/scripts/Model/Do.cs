using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace model
{
    public class Do
    {
        public readonly model.BoardCoordinate boardCoordinate;
        public readonly int previusNumber;
        public readonly int currentNumber;
        public readonly bool isMemoMode;
        public readonly int[] memoArray;

        public Do(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber, bool isMemoMode, int[] memoArray)
        {
            this.boardCoordinate = boardCoordinate;
            this.previusNumber = previusNumber;
            this.currentNumber = currentNumber;
            this.isMemoMode = isMemoMode;
            this.memoArray = memoArray;
            PrintDo();
        }

        public void PrintDo()
        {            
            Debug.Log(string.Format("[{0}, {1}] = pre : {2} cur : {3}, isMemoMode : {4}, memoArray : {5}", 
                this.boardCoordinate.column, this.boardCoordinate.row, previusNumber, currentNumber, isMemoMode, memoArray.ToString()));
        }
    }
}
