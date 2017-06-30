using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace view
{
    public class SquareBoard : MonoBehaviour
    {
        private view.SquarePack[] _squarePacks = null;

        public void Awake()
        {
           _squarePacks = CreateSquarePack();
            InitGrid();
        }

        public void Initialize(System.Action<int, int> onClickCell)
        { 
            for (int i=0; i< _squarePacks.Length; i++)
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

                    targetCell.UpdateCell(modelCell);
                }
            }
        } 

        public void UpdateBoardAim(model.SquareBoard modelSquareBoard)
        {
            view.SquarePack viewTargetPack = null;
            view.SquareCell viewTargetCell = null;
            model.SquarePack modelTargetPack = null;
            model.SquareCell modelTargetCell = null;

            bool isEqualValue;
            bool isEqualColumn;
            bool isEqualRow;
            bool isSelectCell;

            for (int i = 0; i < _squarePacks.Length; i++)
            {
                viewTargetPack = _squarePacks[i];
                modelTargetPack = modelSquareBoard.SquarePack[i];

                for (int j = 0; j < viewTargetPack.SquareCells.Length; j++)
                {
                    viewTargetCell = viewTargetPack.SquareCells[j];
                    modelTargetCell = modelTargetPack.SquareCells[j];
                    isEqualValue = false;
                    isEqualColumn = false;
                    isEqualRow = false;
                    isSelectCell = false;

                    if(!modelTargetCell.IsOpenValue &&( modelTargetCell.IsDuplicatePack || modelTargetCell.IsDuplicateColumn || modelTargetCell.IsDuplicateRow))
                    {
                        viewTargetCell.UpdateTrim(Color.black, modelTargetCell.GetTextColor());
                        continue;
                    }

                    if (modelSquareBoard.SelectCell.BoardCoorinate.column == viewTargetCell.BoardCoorinate.column
                        && modelSquareBoard.SelectCell.BoardCoorinate.row == viewTargetCell.BoardCoorinate.row)
                    {
                        viewTargetCell.UpdateTrim(Color.magenta, modelSquareBoard.SelectCell.GetTextColor());
                        isSelectCell = true;
                    }

                    if (isSelectCell) continue;

                    for (int k = 0; k < modelSquareBoard.EqaulValueCells.Count; k++)
                    {
                        if (modelSquareBoard.EqaulValueCells[k].BoardCoorinate.column == viewTargetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulValueCells[k].BoardCoorinate.row == viewTargetCell.BoardCoorinate.row)
                        {
                            viewTargetCell.UpdateTrim(Color.yellow, modelSquareBoard.EqaulValueCells[k].GetTextColor());
                            isEqualValue = true;
                            break;
                        }
                    }

                    if (isEqualValue) continue;

                    for (int k = 0; k < modelSquareBoard.EqualColumnCells.Length; k++)
                    {
                        if (modelSquareBoard.EqualColumnCells[k].BoardCoorinate.column == viewTargetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqualColumnCells[k].BoardCoorinate.row == viewTargetCell.BoardCoorinate.row)
                        {
                            viewTargetCell.UpdateTrim(Color.magenta, modelSquareBoard.EqualColumnCells[k].GetTextColor());
                            isEqualColumn = true;
                            break;
                        }
                    }

                    if (isEqualColumn) continue;

                    for (int k = 0; k < modelSquareBoard.EqaulRowCells.Length; k++)
                    {
                        if (modelSquareBoard.EqaulRowCells[k].BoardCoorinate.column == viewTargetCell.BoardCoorinate.column &&
                            modelSquareBoard.EqaulRowCells[k].BoardCoorinate.row == viewTargetCell.BoardCoorinate.row)
                        {
                            viewTargetCell.UpdateTrim(Color.magenta, modelSquareBoard.EqaulRowCells[k].GetTextColor());
                            isEqualRow = true;
                            break;
                        }
                    }

                    if (isEqualRow) continue;

                    viewTargetCell.UpdateTrim(Color.grey, modelTargetCell.GetTextColor());
                    
                }
            }
        }

        private void InitGrid()
        {
            this.GetComponent<RectTransform>().sizeDelta = DefineData.BOARDSIZE;

            GridLayoutGroup grid = this.gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = DefineData.PACKSIZE;
            grid.spacing = DefineData.PACK_INTERVAL;
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperLeft;
            grid.constraint = GridLayoutGroup.Constraint.Flexible;
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
                obj.layer = LayerMask.NameToLayer("UI");
                squarePack[i] = obj.GetComponent<view.SquarePack>();
            }
            return squarePack;
        }

    }
}