using UnityEngine;
using UnityEngine.UI;

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

        public void UpdateBoard(model.SquareBoard modelSquareBoard)
        {
            view.SquarePack viewTargetPack = null;
            view.SquareCell viewTargetCell = null;
            model.SquarePack modelTargetPack = null;
            model.SquareCell modelTargetCell = null;

            for (int i = 0; i < _squarePacks.Length; i++)
            {
                viewTargetPack = _squarePacks[i];
                modelTargetPack = modelSquareBoard.SquarePack[i];

                for (int j = 0; j < viewTargetPack.SquareCells.Length; j++)
                {
                    viewTargetCell = viewTargetPack.SquareCells[j];
                    modelTargetCell = modelTargetPack.SquareCells[j];

                    viewTargetCell.UpdateCell(modelTargetCell);
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