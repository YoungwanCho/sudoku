using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquareCell : MonoBehaviour
    {
        public Text numberValue_ = null;
        public Image backGroundImage_ = null;

        private model.BoardCoordinate _boardCoordinate = null;

        private int _packIndex = 0;
        private int _orderIdex = 0;
        private int _column = 0;
        private int _row = 0;

        public void Awake()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.size = DefineData.CELLSIZE;

            //Button btn = this.gameObject.AddComponent<Button>();
            //btn.onClick.AddListener(OnClick);
        }

        //public void OnClick()
        //{
        //    Debug.Log("OnClick : " + name);
        //}

        public void Initialize(int packIndex, int orderIndex)
        {
            this._packIndex = packIndex;
            this._orderIdex = orderIndex;
            this._column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            this._row = orderIndex / DefineData.MAX_ROW_COUNT;
            this._boardCoordinate = new model.BoardCoordinate(packIndex, this._column, this._row);
            //Debug.Log(string.Format("Name :{0} -  [{1}, {2}]", name, _boardCoordinate.column, _boardCoordinate.row));
            SetPosition(orderIndex);
        }

        public void UIUpdate(int numberValue, string imageName)
        {
            numberValue_.text = numberValue.ToString();
            //numberValue_.text = string.Format("[{0}, {1}]", _boardCoordinate.column, _boardCoordinate.row);
            Sprite image = Resources.Load(string.Format("Image/{0}", imageName), typeof(Sprite)) as Sprite;
            backGroundImage_.sprite = image;
        }

        private void SetPosition(int orderIndex)
        {
            RectTransform imageRect = backGroundImage_.GetComponent<RectTransform>();
            imageRect.sizeDelta = DefineData.CELLSIZE;

            RectTransform textRect = numberValue_.GetComponent<RectTransform>();
            textRect.sizeDelta = DefineData.CELLSIZE;

            int column = _orderIdex % DefineData.MAX_COLUMN_COUNT;
            int row = _orderIdex / DefineData.MAX_ROW_COUNT;

            float startX = -((imageRect.sizeDelta.x * DefineData.MAX_COLUMN_COUNT) + (DefineData.MAX_COLUMN_COUNT + 1)) * 0.5f;
            float startY = ((imageRect.sizeDelta.y * DefineData.MAX_ROW_COUNT) + (DefineData.MAX_ROW_COUNT + 1)) * 0.5f;

            float posX = startX + (imageRect.sizeDelta.x * 0.5f) + ((imageRect.sizeDelta.x * column) + (column + 1));
            float posY = startY - (imageRect.sizeDelta.y * 0.5f) - ((imageRect.sizeDelta.y * row) - (row + 1));

            this.transform.localPosition = new Vector3(posX, posY, 0);
        }

    }
}