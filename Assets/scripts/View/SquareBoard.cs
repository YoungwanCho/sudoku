using UnityEngine;
using System.Collections.Generic;

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

            for (int i = 0; i < _squarePacks.Length; i++)
            {
                targetPack = _squarePacks[i];
                for (int j = 0; j < targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    isEqualValue = false;
                    isEqualColumn = false;
                    isEqualRow = false;

                    if(modelSquareBoard.SquarePack[i].SquareCells[j].IsDuplicate)
                    {
                        targetCell.UpdateTrim("cell_black", false, Color.red);
                        continue;
                    }

                    for (int k = 0; k < modelSquareBoard.EqaulValueCells.Count; k++)
                    {
                        if (modelSquareBoard.EqaulValueCells[k].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulValueCells[k].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_orange", true, Color.green);
                            isEqualValue = true;
                            break;
                        }
                    }

                    if (isEqualValue) continue;

                    for (int k = 0; k < modelSquareBoard.EqualColumnCells.Length; k++)
                    {
                        if (modelSquareBoard.EqualColumnCells[k].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqualColumnCells[k].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_green", true, modelSquareBoard.EqualColumnCells[k].IsDuplicate ? Color.red : Color.black);
                            isEqualColumn = true;
                            break;
                        }
                    }

                    if (isEqualColumn) continue;

                    for (int k = 0; k < modelSquareBoard.EqaulRowCells.Length; k++)
                    {
                        if (modelSquareBoard.EqaulRowCells[k].BoardCoorinate.column == targetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulRowCells[k].BoardCoorinate.row == targetCell.BoardCoorinate.row)
                        {
                            targetCell.UpdateTrim("cell_green", true, modelSquareBoard.EqualColumnCells[k].IsDuplicate ? Color.red : Color.black);
                            isEqualRow = true;
                            break;
                        }
                    }

                    if (isEqualRow) continue;

                    targetCell.UpdateTrim("cell_lemon", false, Color.black);
                }
            }
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