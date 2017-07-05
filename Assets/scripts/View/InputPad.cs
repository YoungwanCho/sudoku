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
            _quitButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                (DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(0.0f, -300.0f, 0.0f), Quaternion.identity, Vector3.one, "Quit", "UI");
        }

        private void CreateDeleteButton()
        {
            _deleteButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                (DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one, "Delete", "UI");
        }

        private void CreateMemoButton()
        {
            _memoButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                (DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(-480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one, "Memo", "UI");
        }

        private void CreateNumberButton()
        {
            for (int i = 0; i < DefineData.MAX_NUMBER_VALUE; i++)
            {
                _numberButton[i] = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                    (DefineData.PREFAB_BASIC_BUTTON_PATH, numberButtonParent_, Vector3.zero, Quaternion.identity, Vector3.one, (i + 1).ToString(), "UI");
            }
        }

        private void CreateDoActioButton()
        {
            _undoButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                    (DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(-350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one, "Undo", "UI");

            _redoButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>
                    (DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one, "Redo", "UI");
        }
    }
}
