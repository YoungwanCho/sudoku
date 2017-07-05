using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class SquarePack : MonoBehaviour
    {
        public SquareCell[] SquareCells { get { return _squareCells; } }

        private view.SquareCell[] _squareCells = null;
        private BoxCollider2D _collider = null;
        private int _orderIndex = 0; //@TODO: 생성자를 사용하지 않고 한번만 할당 하도록 수정

        public void Awake()
        {
            _squareCells = CreateCells();
            InitGrid();
            _collider = this.gameObject.AddComponent<BoxCollider2D>();
        }

        public void Initialize(System.Action<int, int> onClickCell, int orderIndex)
        { 
            _orderIndex = orderIndex;
            for(int i=0; i<_squareCells.Length; i++)
            {
                _squareCells[i].Initialize(onClickCell, orderIndex, i);
            }
        }

        private view.SquareCell[] CreateCells()
        {
            view.SquareCell[] squareCells = new view.SquareCell[DefineData.MAX_CELL_COUNT];

            for (int i = 0; i < squareCells.Length; i++)
            {
                squareCells[i] = FactoryManager.Instance.InstantiateGameObject<view.SquareCell>
                    (DefineData.PREFAB_SQUARECELL_PATH, this.transform, Vector3.zero, Quaternion.identity, Vector3.one, string.Format("Cell {0}", i), "UI");
            }
            return squareCells;
        }

        private void InitGrid()
        {
            GridLayoutGroup grid = this.gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = DefineData.CELLSIZE;
            grid.spacing = DefineData.CELL_INTERVAL;
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperLeft;
            grid.constraint = GridLayoutGroup.Constraint.Flexible;
        }
    }
}