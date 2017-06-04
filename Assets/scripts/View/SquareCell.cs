using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquareCell : MonoBehaviour
    {
        public Text numberValue_ = null;
        public Image backGroundImage_ = null;

        private int _orderIdex = 0;

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

        public void Initialize(int orderIndex)
        {
            this._orderIdex = orderIndex;
            this.numberValue_.text = string.Empty;

            SetPosition(orderIndex);
        }

        public void UIUpdate(int numberValue, string imageName)
        {
            numberValue_.text = numberValue.ToString();
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

            float posX = startX + ((imageRect.sizeDelta.x * column) + (column + 1) + (imageRect.sizeDelta.x * 0.5f));
            float posY = startY - ((imageRect.sizeDelta.y * row) - (row + 1) - (imageRect.sizeDelta.y * 0.5f));

            this.transform.localPosition = new Vector3(posX, posY, 0);
        }

    }
}