using UnityEngine;
using System.Collections;

namespace controller
{
    public class ViewController : MonoBehaviour
    {
        private view.SquareBoard _board = null;

        public void Awake()
        {
            _board = CreateSquareBoard();
        }

        public void Initialize(controller.InputController.OnClick OnClickCell)
        {
            _board.Initialize(OnClickCell);
        }

        private view.SquareBoard CreateSquareBoard()
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUAREBOARD_PATH) as GameObject;
            GameObject obj = Instantiate(prefab, this.transform) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = "SquareBoard";

            return obj.GetComponent<view.SquareBoard>();
        }

        public view.SquareCell FindCellByCoordinates(int column, int row)
        {
            return _board.FindCellByCoordinates(column, row);
        }
    }
}