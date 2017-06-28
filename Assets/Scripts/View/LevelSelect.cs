using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace view
{
    public class LevelSelect : MonoBehaviour
    {
        public Transform buttonParent_ = null;
        private DefaultButton[] _levelSelectButton = new DefaultButton[6];
        // Use this for initialization

        public void Awake()
        {
            CreateLevelSelectButton();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(System.Action<GameObject> levelSelectFunc)
        {
            for (int i = 0; i < _levelSelectButton.Length; i++)
            {
                _levelSelectButton[i].Initialize(levelSelectFunc, Color.green, i.ToString());
            }
        }

        private void CreateLevelSelectButton()
        {
            Vector3 localPos = Vector3.zero;
            
            float sizeWidth = DefineData.CELLSIZE.x;
            float intervalWidth = sizeWidth * 0.3f;

            int count = _levelSelectButton.Length;

            float totalWidth = (intervalWidth * (count -1)) + (sizeWidth * count);
            float startPosX = -totalWidth * 0.5f;

            for (int i = 0; i < _levelSelectButton.Length; i++)
            {
                _levelSelectButton[i] = InstantiateBasicButton(i.ToString(), buttonParent_, Vector3.zero, Quaternion.identity, Vector3.one);
            }
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
