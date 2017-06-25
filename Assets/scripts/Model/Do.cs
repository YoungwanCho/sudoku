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
    }
}
