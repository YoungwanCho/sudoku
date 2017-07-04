using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class MainLobby : MonoBehaviour
    {
        [SerializeField]
        private Text lobbyTitleText_ = null;
        [SerializeField]
        private BasicButton optionButton_ = null;
        [SerializeField]
        private BasicButton archiveButton_ = null;

        private BasicButton _newGameButton = null;

        public void Awake()
        {
            CreateNewGameButton();
        }

        // Use this for initialization
        void Start()
        {
            //Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(System.Action<GameObject> NewGameStartFunc)
        {
            _newGameButton.Initialize(NewGameStartFunc, Color.green, "New Game");
        }

        private void CreateNewGameButton()
        {
            _newGameButton = InstantiateBasicButton("NewGameStart", this.transform, Vector3.zero, Quaternion.identity, Vector3.one);
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
