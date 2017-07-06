using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class LevelSelect : MonoBehaviour
    {
        [SerializeField]
        private Transform buttonParent_ = null;
        private BasicButton[] _levelSelectButton = new BasicButton[7];

        public void Awake()
        {
            CreateLevelSelectButton();
        }

        public void Initialize(System.Action<GameObject> levelSelectFunc)
        {
            string buttonText = string.Empty;
            for (int i = 0; i < _levelSelectButton.Length; i++)
            {
                buttonText = i.ToString();
                if(i == 0)
                {
                    buttonText = "Map Editor";
                }
                _levelSelectButton[i].Initialize(levelSelectFunc, Color.green, buttonText);
            }
        }

        private void CreateLevelSelectButton()
        {
            for (int i = 0; i < _levelSelectButton.Length; i++)
            {
                _levelSelectButton[i] = FactoryManager.Instance.InstantiateGameObject<BasicButton>(DefineData.PREFAB_BASIC_BUTTON_PATH, buttonParent_, Vector3.zero, Quaternion.identity, Vector3.one, i.ToString(), "UI");
            }
        }
    }
}
