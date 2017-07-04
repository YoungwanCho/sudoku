using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace view
{
    public class LevelSelect : MonoBehaviour
    {
        [SerializeField]
        private Transform buttonParent_ = null;
        private BasicButton[] _levelSelectButton = new BasicButton[6];

        public void Awake()
        {
            CreateLevelSelectButton();
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
