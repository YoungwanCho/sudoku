using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class GameResult : MonoBehaviour
    {
        public Text title_ = null;
        public Text clearTime_ = null;

        private InputBasicButton _quitButton = null;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(System.Action<GameObject> quitFunc)
        {
            CreateQuitButton();
            _quitButton.Initialize(quitFunc, "cell_green", "Quit");
        }

        public void UpdateClearTime(string time)
        {
            clearTime_.text = time;
        }

        private void CreateQuitButton()
        {
            _quitButton = InstantiateBasicButton("QuitButton", this.transform, Vector3.zero, Quaternion.identity, Vector3.one);
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
