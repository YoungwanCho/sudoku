using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquareCell : MonoBehaviour
    {
        public model.BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }
        public Text numberValue_;
        public Image backGroundImage_;
        public Transform memoParent_;

        private Text[] _memoTextArray = new Text[DefineData.MAX_CELL_COUNT]; 

        private model.BoardCoordinate _boardCoordinate = null;

        private Button _button = null;

        private int _packIndex = 0;
        private int _orderIdex = 0;
        private int _column = 0;
        private int _row = 0;

        public void Awake()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.size = DefineData.CELLSIZE;

            _button = this.gameObject.AddComponent<Button>();
            //CreateMemoObject();
        }

        public void Initialize(controller.GameController.OnClick onClickCell, int packIndex, int orderIndex)
        {
            _button.onClick.AddListener
            (
                delegate
                {
                    onClickCell(_boardCoordinate.column, _boardCoordinate.row);
                }
            );

            this._packIndex = packIndex;
            this._orderIdex = orderIndex;
            this._column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            this._row = orderIndex / DefineData.MAX_ROW_COUNT;
            this._boardCoordinate = new model.BoardCoordinate(packIndex, this._column, this._row);
            //Debug.Log(string.Format("Name :{0} -  [{1}, {2}]", name, _boardCoordinate.column, _boardCoordinate.row));
            SetPosition(orderIndex);
        }

        public void UpdateTrim(Color bgColor, bool isZooming, Color textColor)
        {
            ChangeImage(bgColor);

            ChangeScale(isZooming ? 1.1f : 1.0f);

            ChangeTextColor(textColor);
        }

        public void UpdateText(int numberValue)
        {            
            numberValue_.text = numberValue == 0 ? string.Empty : numberValue.ToString();
            //numberValue_.text = string.Format("[{0}, {1}]", _boardCoordinate.column, _boardCoordinate.row);
        }

        public void UpdateText(string text) // ÁÂÇ¥ º¸±â ¿ë
        {
            numberValue_.text = text;
        }
        
        public void UpdateMemoText(int number)
        {
            int arrIndex = number - 1;
            _memoTextArray[arrIndex].text = number.ToString();
        } 

        private void ChangeScale(float scale)
        {
            this.transform.localScale = Vector3.one * scale;
        } 

        private void ChangeImage(Color color)
        {
            //Sprite image = Resources.Load(string.Format("Image/{0}", imageName), typeof(Sprite)) as Sprite;
            //backGroundImage_.sprite = image;
            backGroundImage_.color = color;
        }
        
        private void ChangeTextColor(Color32 color)
        {
            numberValue_.color = color;
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

        private void CreateMemoObject()
        {
            Transform trans = null;
            
            for(int i=0; i<_memoTextArray.Length; i++)
            {
                trans = Instantiate(new GameObject(), memoParent_).transform;
                trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                trans.localRotation = Quaternion.identity;
                trans.localScale = Vector3.one;
                _memoTextArray[i] = trans.gameObject.AddComponent<Text>();
            }
        }

    }
}