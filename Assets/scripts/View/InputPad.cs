using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPad : MonoBehaviour
{
    private InputNumberButton[] _numberButton = new InputNumberButton[DefineData.MAX_NUMBER_VALUE];

    public void Awake()
    {
        CreateInputNumberButtons();
    }

    public void Initialize(controller.GameController.OnClickInputPad onClickInputNumberButton)
    {
        for(int i=0; i< _numberButton.Length; i++)
        {
            _numberButton[i].Initialize(onClickInputNumberButton, "cell_red", (i+1));
        }
    }

    private void CreateInputNumberButtons()
    {
        GameObject prefab = Resources.Load(DefineData.PREFAB_INPUT_NUMBER_PAD_PATH) as GameObject;
        GameObject obj = null;

        float totalWidth = (DefineData.MAX_NUMBER_VALUE * DefineData.NUMBERSIZE.x) + DefineData.MAX_NUMBER_VALUE;

        float startX = (-totalWidth * 0.5f) + DefineData.NUMBERSIZE.x * 0.5f;
        
        for(int i=0; i<DefineData.MAX_NUMBER_VALUE; i++)
        {
            obj = Instantiate(prefab, this.transform) as GameObject;
            obj.transform.localPosition = new Vector3(startX + (DefineData.NUMBERSIZE.x * i) + (i+1), 0.0f, 0.0f);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.name = (i + 1).ToString(); // 입력 값으로 사용
            _numberButton[i] = obj.GetComponent<InputNumberButton>();
        }
    }
}
