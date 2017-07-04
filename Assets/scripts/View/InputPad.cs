using UnityEngine;
namespace view
{
    public class InputPad : MonoBehaviour
    {
        [SerializeField]
        private Transform numberButtonParent_;

        private BasicButton[] _numberButton = new BasicButton[DefineData.MAX_NUMBER_VALUE];

        private BasicButton _undoButton = null;
        private BasicButton _redoButton = null;
        private BasicButton _memoButton = null;
        private BasicButton _deleteButton = null;
        private BasicButton _quitButton = null;

        public void Awake()
        {
            CreateNumberButton();
            CreateDoActioButton();
            CreateMemoButton();
            CreateDeleteButton();
            CreateQuitButton();
        }

        public void Initialize(System.Action<GameObject> onClickNumberButton,
            System.Action<GameObject> onClickDoAaction,
            System.Action<GameObject> onClickMemo,
            System.Action<GameObject> onClickDelete,
            System.Action<GameObject> onClickQuit)
        {
            for (int i = 0; i < _numberButton.Length; i++)
            {
                _numberButton[i].Initialize(onClickNumberButton, Color.red, (i + 1).ToString());
            }

            _undoButton.Initialize(onClickDoAaction, Color.black, "Undo");
            _redoButton.Initialize(onClickDoAaction, Color.black, "Redo");
            _memoButton.Initialize(onClickMemo, Color.green, "Memo");
            _deleteButton.Initialize(onClickDelete, Color.green, "Delete");
            _quitButton.Initialize(onClickQuit, Color.green, "Quit");

            this.UpdateMemoButton(false);
        }

        public void UpdateMemoButton(bool isOn)
        {
            Color color = isOn ? Color.green : Color.gray;
            _memoButton.UpdateButton(color);
        }

        private void CreateQuitButton()
        {
            _quitButton = InstantiateBasicButton("Quit", this.transform, new Vector3(0.0f, -300.0f, 0.0f), Quaternion.identity, Vector3.one);
        }

        private void CreateDeleteButton()
        {
            _deleteButton = InstantiateBasicButton("Delete", this.transform, new Vector3(480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
        }

        private void CreateMemoButton()
        {
            _memoButton = InstantiateBasicButton("Memo", this.transform, new Vector3(-480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
        }

        private void CreateNumberButton()
        {
            for (int i = 0; i < DefineData.MAX_NUMBER_VALUE; i++)
            {
                _numberButton[i] = InstantiateBasicButton((i + 1).ToString(), numberButtonParent_, Vector3.zero, Quaternion.identity, Vector3.one);
            }
        }

        private void CreateDoActioButton()
        {
            _undoButton = InstantiateBasicButton("Undo", this.transform, new Vector3(-350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
            _redoButton = InstantiateBasicButton("Redo", this.transform, new Vector3(350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
        }

        private BasicButton InstantiateBasicButton(string objName, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale)
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_BASIC_BUTTON_PATH) as GameObject;
            GameObject obj = obj = Instantiate(prefab, parent) as GameObject;
            obj.transform.localPosition = localPos;
            obj.transform.localRotation = localRot;
            obj.transform.localScale = localScale;
            obj.name = objName;
            obj.layer = LayerMask.NameToLayer("UI");
            return obj.GetComponent<BasicButton>();
        }
    }
}
