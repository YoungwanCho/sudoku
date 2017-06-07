using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquarePack : MonoBehaviour
    {
        public SquareCell[] SquareCells { get { return _squareCells; } }
        private view.SquareCell[] _squareCells = null;
        private int _orderIndex = 0; //@TODO: 생성자를 사용하지 않고 한번만 할당 하도록 수정
        private BoxCollider2D _collider = null;

        public void Awake()
        {
            _squareCells = CreateCells();
            _collider = this.gameObject.AddComponent<BoxCollider2D>();
        }

        public void Initialize(controller.GameController.OnClick onClickCell, int orderIndex)
        {
            _orderIndex = orderIndex;
            SetPosition(orderIndex);

            
            for(int i=0; i<_squareCells.Length; i++)
            {
                _squareCells[i].Initialize(onClickCell, orderIndex, i);
                //_squareCells[i].UpdateText(i);
            }
        }

        private view.SquareCell[] CreateCells()
        {
            view.SquareCell[] squareCells = new view.SquareCell[DefineData.MAX_CELL_COUNT];
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUARECELL_PATH) as GameObject;
            GameObject obj = null;
            Vector3 pos = Vector3.zero;
            string imageName = string.Empty;

            for (int i = 0; i < squareCells.Length; i++)
            {
                obj = Instantiate(prefab, this.transform) as GameObject;
                obj.transform.localPosition = pos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
                obj.name = string.Format("Cell {0}", i);
                squareCells[i] = obj.GetComponent<view.SquareCell>();
            }
            return squareCells;
        }

        private void SetPosition(int orderIndex)
        {
            int column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            int row = orderIndex / DefineData.MAX_ROW_COUNT;
            
            float packWdith = ((DefineData.CELLSIZE.x * DefineData.MAX_COLUMN_COUNT) + (DefineData.MAX_COLUMN_COUNT + 1));
            float packHeight = ((DefineData.CELLSIZE.y * DefineData.MAX_ROW_COUNT) + (DefineData.MAX_ROW_COUNT + 1));

            float totalWidth = packWdith * DefineData.MAX_COLUMN_COUNT;
            float totalHeight = packHeight * DefineData.MAX_ROW_COUNT;

            float startX = -totalWidth * 0.5f + (packWdith * 0.5f);
            float startY = totalHeight * 0.5f - (packHeight * 0.5f);

            float posX = startX + ((packWdith * column) + (column + 1));
            float posY = startY - ((packHeight * row) - (row + 1));

            this.transform.localPosition = new Vector3(posX, posY, 0);

        }

    }
}