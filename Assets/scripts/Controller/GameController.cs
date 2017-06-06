using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class GameController : MonoBehaviour
    {
        public delegate void OnClick(int n, int n2);

        private scene.Game _game = null;

        private model.SquareBoard _modelBoard = null;
        private view.SquareBoard _viewBoard = null;

        public void Awake()
        {
            _modelBoard = new model.SquareBoard();
            _viewBoard = CreateSquareBoard();
        } 

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(scene.Game game)
        {
            this._game = game;
            _modelBoard.Initialize();
            _viewBoard.Initialize(this.OnClickCell);
        } 

        public void OnClickCell(int column, int row)
        {
            UnityEngine.Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
        }

        public void OnClickNumberButton(int value)
        {
            UnityEngine.Debug.Log("OnClick Input Button Value : " + value);
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


    }
}
