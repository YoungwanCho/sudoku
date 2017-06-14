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

        public Do(model.BoardCoordinate boardCoordinate, int previusNumber, int currentNumber)
        {
            this.boardCoordinate = boardCoordinate;
            this.previusNumber = previusNumber;
            this.currentNumber = currentNumber;
            PrintDo();
        }

        public void PrintDo()
        {
            Debug.Log(string.Format("[{0}, {1}] = pre : {2} cur: {3}", this.boardCoordinate.column, this.boardCoordinate.row, previusNumber, currentNumber));
        }
    }
}
