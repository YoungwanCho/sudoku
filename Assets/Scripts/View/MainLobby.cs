using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class MainLobby : MonoBehaviour
    {
        public Text lobbyTitleText_ = null;
        public InputBasicButton newGameButton = null;
        public InputBasicButton optionButton_ = null;
        public InputBasicButton archiveButton_ = null;


        private System.Action<GameObject> _newGameStartFunc;

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
            _newGameStartFunc = NewGameStartFunc;
            newGameButton.Initialize(OnClickNewGameStart, "cell_green", "New Game");
        }

        public void OnClickNewGameStart(Object obj)
        {
            Debug.Log(string.Format("OnClickNewGame {0}", obj.name));
            _newGameStartFunc(obj as GameObject);
        }

        private void CreateNewGameButton()
        {
            newGameButton = InstantiateBasicButton("NewGameStart", this.transform, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        private InputBasicButton InstantiateBasicButton(string objName, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale)
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_DEFAULT_BUTTON_PATH) as GameObject;
            GameObject obj = obj = Instantiate(prefab, parent) as GameObject;
            obj.transform.localPosition = localPos;
            obj.transform.localRotation = localRot;
            obj.transform.localScale = localScale;
            obj.name = objName;
            return obj.GetComponent<InputBasicButton>();
        }
    }
}
