using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace view
{
    public class LevelSelect : MonoBehaviour
    {
        private InputBasicButton[] _levelSelectButton = new InputBasicButton[6];

        private System.Action<GameObject> _levelSelectFunc = null;
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
            _levelSelectFunc = levelSelectFunc;
            for (int i = 0; i < _levelSelectButton.Length; i++)
            {
                _levelSelectButton[i].Initialize(OnClickLevelSelect, "cell_green", i.ToString());
            }
        }

        public void OnClickLevelSelect(UnityEngine.Object obj)
        {
            Debug.Log(string.Format("OnClickLevelSelct {0}", obj.name));
            _levelSelectFunc(obj as GameObject);
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
                localPos = new Vector3(startPosX + (intervalWidth * i) + (sizeWidth * i), 0.0f, 0.0f);
                _levelSelectButton[i] = InstantiateBasicButton(i.ToString(), this.transform, localPos, Quaternion.identity, Vector3.one);
            }
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
