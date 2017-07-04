using System.Collections;
using System.Collections.Generic;
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
            _quitButton = InstantiateBasicButton("QuitButton", this.transform, new Vector3(0, -500.0f, 0), Quaternion.identity, Vector3.one);
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
