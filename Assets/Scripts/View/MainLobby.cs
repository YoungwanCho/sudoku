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

        public void Initialize(System.Action<GameObject> NewGameStartFunc)
        {
            _newGameButton.Initialize(NewGameStartFunc, Color.green, "New Game");
        }

        private void CreateNewGameButton()
        {
            _newGameButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>(DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, Vector3.zero, Quaternion.identity, Vector3.one, "NewGameStart", "UI");
                
        }
    }
}
