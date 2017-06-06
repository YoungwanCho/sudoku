using UnityEngine;
using System.Collections;

namespace view
{
    public class SquareBoard : MonoBehaviour
    {

        private view.SquarePack[] _squarePacks = null;

        public void Awake()
        {
           _squarePacks = CreateSquarePack();
        }

        public void Initialize(controller.GameController.OnClick onClickCell)
        {
            for(int i=0; i< _squarePacks.Length; i++)
            {
                _squarePacks[i].Initialize(onClickCell, i);
            }
        }

        public SquareCell FindCellByCoordinates(int column, int row)
        {
            SquareCell cell = null;

            int packColumn = column / DefineData.MAX_COLUMN_COUNT;
            int packRow = row / DefineData.MAX_ROW_COUNT;

            int packOrderIndex = (packColumn * DefineData.MAX_ROW_COUNT) + packRow;

            int cellColumn = packColumn;
            int cellRow = packRow;

            int cellOrderIndex = (cellColumn * DefineData.MAX_ROW_COUNT) + cellRow;

            cell = _squarePacks[packOrderIndex].SquareCells[cellOrderIndex];

            Debug.Log(string.Format("parameter : [{0}, {1}] - Cell Coordinate : [{2}, {3}]",
                column, row, cell.BoardCoorinate.column, cell.BoardCoorinate.row));

            return cell;
        }

        private view.SquarePack[] CreateSquarePack()
        {
            view.SquarePack[] squarePack = new view.SquarePack[DefineData.MAX_PACK_COUNT];
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUAREPACK_PATH) as GameObject;
            GameObject obj = null;
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < squarePack.Length; i++)
            {
                obj = Instantiate(prefab, this.transform) as GameObject;
                obj.transform.localPosition = pos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
                obj.name = string.Format("Pack {0}", i);

                squarePack[i] = obj.GetComponent<view.SquarePack>();
            }
            return squarePack;
        }

    }
}