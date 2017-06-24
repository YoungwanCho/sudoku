using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class MainLobby : MonoBehaviour
    {
        public Text lobbyTitleText_ = null;
        private DefaultButton _newGameButton = null;
        public DefaultButton optionButton_ = null;
        public DefaultButton archiveButton_ = null;

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

        private DefaultButton InstantiateBasicButton(string objName, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale)
        {
            GameObject prefab = Resources.Load(DefineData.PREFAB_DEFAULT_BUTTON_PATH) as GameObject;
            GameObject obj = obj = Instantiate(prefab, parent) as GameObject;
            obj.transform.localPosition = localPos;
            obj.transform.localRotation = localRot;
            obj.transform.localScale = localScale;
            obj.name = objName;
            obj.layer = LayerMask.NameToLayer("UI");
            return obj.GetComponent<DefaultButton>();
        }
    }
}
