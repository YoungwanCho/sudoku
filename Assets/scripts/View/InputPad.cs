using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPad : MonoBehaviour
{
    private Button[] _inputValueButtons = new Button[DefineData.MAX_NUMBER_VALUE];

    public void Awake()
    {
        CreateInputValueButtons();
    }

    public void Initialize(controller.GameController.OnClickInputPad onClickInputPad)
    {
        
        for(int i=0; i<_inputValueButtons.Length; i++)
        {
            //_inputValueButtons[i].onClick.AddListener(delegate { onClickInputPad); });
        }
    }

    private void CreateInputValueButtons()
    {
        GameObject prefab = Resources.Load(DefineData.PREFAB_INPUT_NUMBER_PAD_PATH) as GameObject;
        GameObject obj = null;
        Text text = null;
        Image image = null;

        float totalWidth = (DefineData.MAX_NUMBER_VALUE * DefineData.NUMBERSIZE.x) + DefineData.MAX_NUMBER_VALUE;

        float startX = (-totalWidth * 0.5f) + DefineData.NUMBERSIZE.x * 0.5f;
        
        for(int i=0; i<DefineData.MAX_NUMBER_VALUE; i++)
        {
            obj = Instantiate(prefab, this.transform) as GameObject;
            obj.transform.localPosition = new Vector3(startX + (DefineData.NUMBERSIZE.x * i) + (i+1), 0.0f, 0.0f);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = (i + 1).ToString(); // 입력 값으로 사용
            _inputValueButtons[i] = obj.GetComponent<Button>();
            text = obj.GetComponentInChildren<Text>();
            text.text = obj.name;
            image = obj.GetComponentInChildren<Image>();
            Sprite sprite = Resources.Load(string.Format("Image/{0}", "cell_red"), typeof(Sprite)) as Sprite;
            image.sprite = sprite;
        }
    }
}
