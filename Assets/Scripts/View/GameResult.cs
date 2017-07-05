using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class GameResult : MonoBehaviour
    {
        [SerializeField]
        private Text title_ = null;
        [SerializeField]
        private Text clearTime_ = null;

        private BasicButton _quitButton = null;

        public void Initialize(System.Action<GameObject> quitFunc)
        {
            CreateQuitButton();
            _quitButton.Initialize(quitFunc, Color.green, "Quit");
        }

        public void UpdateClearTime(string time)
        {
            clearTime_.text = time;
        }

        private void CreateQuitButton()
        {
            _quitButton = FactoryManager.Instance.InstantiateGameObject<BasicButton>(DefineData.PREFAB_BASIC_BUTTON_PATH, this.transform, new Vector3(0, -500.0f, 0), Quaternion.identity, Vector3.one, "QuitButton", "UI");
        }
    }
}
