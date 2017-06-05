using UnityEngine;
using System.Collections;

namespace controller
{
    public class ModelController
	{
        private model.SquareBoard _board = null;

        public ModelController()
        {
            _board = new model.SquareBoard();
            this.Initialize();
        }

        public void Initialize()
        {
            Debug.Log("Model Controller Init");
            _board.Initialize();
        }

        public model.SquareCell FindCellByCoordinates(int column, int row)
        {
            return _board.FindCellByCoordinates(column, row);
        }

    }
}