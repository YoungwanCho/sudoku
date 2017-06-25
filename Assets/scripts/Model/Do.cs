using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace model
{
    public class Do
    {
        public readonly int number;
        public readonly bool isMemoMode;
        public readonly int[] memoArray;

        public Do(int number, bool isMemoMode, int[] memoArray)
        {
            this.number = number;
            this.isMemoMode = isMemoMode;
            this.memoArray = memoArray;
        }
    }

    public class DoAction
    {
        public readonly model.BoardCoordinate boardCoordinate;
        public readonly Do previus;
        public readonly Do current;

        public DoAction(model.BoardCoordinate boardCoordinate, Do previus, Do current)
        {
            this.boardCoordinate = boardCoordinate;
            this.previus = previus;
            this.current = current;
        }

        public DoAction SwapDoAction()
        {
            int[] previusMemoArr = new int[this.previus.memoArray.Length];
            int[] currentMemoArr = new int[this.current.memoArray.Length];
            System.Array.Copy(this.previus.memoArray, previusMemoArr, this.previus.memoArray.Length);
            System.Array.Copy(this.current.memoArray, currentMemoArr, this.current.memoArray.Length);

            model.Do previus = new model.Do(this.previus.number, this.previus.isMemoMode, previusMemoArr);
            model.Do current = new model.Do(this.current.number, this.current.isMemoMode, currentMemoArr);

            model.DoAction doAction = new model.DoAction(this.boardCoordinate, current, previus);

            return doAction;
        }
    }
}
