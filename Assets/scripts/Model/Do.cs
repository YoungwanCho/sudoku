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
        public readonly bool isPreviusMemoMode;
        public readonly bool isCurrentMemoMode;
        public readonly int[] previusMemoArray;
        public readonly int[] currentMemoArray;

        public Do(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber, bool isPreviusMemoMode, bool isCurrentMemoMode, int[] previusMemo, int[] currentMemo)
        {
            this.boardCoordinate = boardCoordinate;
            this.previusNumber = previusNumber;
            this.currentNumber = currentNumber;
            this.isPreviusMemoMode = isPreviusMemoMode;
            this.isCurrentMemoMode = isCurrentMemoMode;
            this.previusMemoArray = previusMemo;
            this.currentMemoArray = currentMemo;
            PrintDo();
        }

        public void PrintDo()
        {
            string arrstr = string.Empty;
            for(int i=0; i<previusMemoArray.Length; i++)
            {
                arrstr += string.Format("[{0}]", previusMemoArray[i]);
            }            
            Debug.Log(string.Format("[{0}, {1}] = pre : {2} cur : {3}, isMemoMode : {4}, memoArray : {5}", 
                this.boardCoordinate.column, this.boardCoordinate.row, previusNumber, currentNumber, isPreviusMemoMode, arrstr));
        }
    }
}
