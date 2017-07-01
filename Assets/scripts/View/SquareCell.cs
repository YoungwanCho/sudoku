using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquareCell : MonoBehaviour
    {
        public model.BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }

		[SerializeField] private Text[] memoTextArray_ = new Text[DefineData.MAX_CELL_COUNT];
        [SerializeField] private Text numberValue_;
        [SerializeField] private Image backGroundImage_;
        [SerializeField] private Transform memoParent_;

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
        }

        public void Initialize(System.Action<int, int> onClickCell, int packIndex, int orderIndex)
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
        }

        public void UpdateCell(model.SquareCell modelCell)
        {
            Color normalColor = new Color(0, 100, 0); //@TODO: 나중에 디파인 처리

            Color imageColor = Color.black;
            Color textColor = Color.white;

            if (!modelCell.IsOpenValue && (modelCell.IsDuplicatePack || modelCell.IsDuplicateColumn || modelCell.IsDuplicateRow))
            {
                imageColor = Color.black;
                textColor = Color.red;
            }
            else if (modelCell.IsSelectCell || modelCell.IsEqualColumn || modelCell.IsEqualRow)
            {
                imageColor = Color.magenta;
                textColor = Color.blue;
            }
            else if (modelCell.IsEqualNumber)
            {
                imageColor = Color.yellow;
                textColor = Color.black;
            }
            else
            {
                imageColor = Color.grey;
                textColor = normalColor;
            }

            UpdateTrim(imageColor, textColor);

            if (modelCell.IsMemoMode)
            {
                this.UpdateMemoText(modelCell.MemoArray);
            }
            else
            {
                this.UpdateNumberText(modelCell.NumberValue);
            }
        }

        private void UpdateTrim(Color imageColor, Color textColor)
        {
            backGroundImage_.color = imageColor;
            numberValue_.color = textColor;
        }

        private void UpdateNumberText(int numberValue)
        {
            numberValue_.gameObject.SetActive(true);
            memoParent_.gameObject.SetActive(false);
            numberValue_.text = numberValue == 0 ? string.Empty : numberValue.ToString();
        }

        private void UpdateMemoText(int[] memoArray)
        {
            numberValue_.gameObject.SetActive(false);
            memoParent_.gameObject.SetActive(true);
            for (int i = 0; i < memoTextArray_.Length; i++)
            {
                memoTextArray_[i].text = memoArray[i].ToString();
            }
        }

    }
}