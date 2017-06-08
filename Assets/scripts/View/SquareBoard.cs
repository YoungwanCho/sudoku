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

        public void UpdateBoardValue(model.SquareBoard modelSquareBoard)
        {
            view.SquarePack targetPack = null;
            view.SquareCell targetCell = null;

            model.SquarePack modelPack = null;
            model.SquareCell modelCell = null;

            for(int i=0; i<_squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                modelPack = modelSquareBoard.SquarePack[i];
                for (int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    modelCell = modelPack.SquareCells[j];
                    targetCell.UpdateText(modelCell.NumberValue);
                    //targetCell.UpdateText(string.Format("[{0},{1}]", modelCell.BoardCoorinate.column, modelCell.BoardCoorinate.row));
                }
            }
        } 

        public void UpdateBoardAim(model.SquareBoard modelSquareBoard)
        {
            view.SquarePack targetPack = null;
            view.SquareCell targetCell = null;

            bool isEqualValue;
            bool isEqualColumn;
            bool isEqualRow;

            for(int i=0; i<modelSquareBoard.EqualColumnCells.Length; i++)
            {
                Debug.Log(string.Format("Column : [{0},{1}]", modelSquareBoard.EqualColumnCells[i].BoardCoorinate.column, modelSquareBoard.EqualColumnCells[i].BoardCoorinate.row));
            }

            for (int i = 0; i < modelSquareBoard.EqaulRowCells.Length; i++)
            {
                Debug.Log(string.Format("Row : [{0},{1}]", modelSquareBoard.EqaulRowCells[i].BoardCoorinate.column, modelSquareBoard.EqaulRowCells[i].BoardCoorinate.row));
            }

            for (int i = 0; i < _squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for (int j = 0; j < targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    isEqualValue = false;
                    isEqualColumn = false;
                    isEqualRow = false;

                    for (int k = 0; k < modelSquareBoard.EqaulValueCells.Count; k++)
                    {
                        if (modelSquareBoard.EqaulValueCells[k].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulValueCells[k].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_orange", true);
                            isEqualValue = true;
                            break;
                        }
                    }

                    if (isEqualValue) continue;

                    for (int n = 0; n < modelSquareBoard.EqualColumnCells.Length; n++)
                    {
                        if (modelSquareBoard.EqualColumnCells[n].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqualColumnCells[n].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_green", true);
                            isEqualColumn = true;
                            break;
                        }
                    }

                    if (isEqualColumn) continue;

                    for (int n = 0; n < modelSquareBoard.EqaulRowCells.Length; n++)
                    {
                        if (modelSquareBoard.EqaulRowCells[n].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulRowCells[n].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_green", true);
                            isEqualRow = true;
                            break;
                        }
                    }

                    if (isEqualRow) continue;

                    targetCell.UpdateTrim("cell_lemon", false);
                }
            }
        }
        
        //public SquareCell FindCellByCoordinates(int column, int row)
        //{
        //    SquareCell cell = null;

        //    int packColumn = column / DefineData.MAX_COLUMN_COUNT;
        //    int packRow = row / DefineData.MAX_ROW_COUNT;

        //    int packOrderIndex = (packColumn * DefineData.MAX_ROW_COUNT) + packRow;

        //    int cellColumn = packColumn;
        //    int cellRow = packRow;

        //    int cellOrderIndex = (cellColumn * DefineData.MAX_ROW_COUNT) + cellRow;

        //    cell = _squarePacks[packOrderIndex].SquareCells[cellOrderIndex];

        //    Debug.Log(string.Format("parameter : [{0}, {1}] - Cell Coordinate : [{2}, {3}]",
        //        column, row, cell.BoardCoorinate.column, cell.BoardCoorinate.row));

        //    return cell;
        //}

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