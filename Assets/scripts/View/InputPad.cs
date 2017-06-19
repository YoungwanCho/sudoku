using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPad : MonoBehaviour
{
    private InputBasicButton[] _numberButton = new InputBasicButton[DefineData.MAX_NUMBER_VALUE];

    private InputBasicButton _undoButton = null;
    private InputBasicButton _redoButton = null;
    private InputBasicButton _memoButton = null;

    public void Awake()
    {
        CreateNumberButton();
        CreateDoActioButton();
        CreateMemoButton();
    }

    public void Initialize(controller.GameController.OnClickInputPad onClickNumberButton, 
        controller.GameController.OnClickInputPad onClickDoAaction,
        controller.GameController.OnClickInputPad onClickMemo)
    {
        for(int i=0; i< _numberButton.Length; i++)
        {
            _numberButton[i].Initialize(onClickNumberButton, "cell_red", (i+1).ToString());
        }

        _undoButton.Initialize(onClickDoAaction, "cell_black", "Undo");
        _redoButton.Initialize(onClickDoAaction,"cell_black", "Redo");
        _memoButton.Initialize(onClickMemo, "cell_green", "Memo");
        this.UpdateMemoButton(false);
    }

    public void UpdateMemoButton(bool isOn)
    {
        Color color = isOn ? Color.white : Color.gray;
        _memoButton.UpdateButton(color);
    }

    private void CreateMemoButton()
    {
        GameObject prefab = Resources.Load(DefineData.PREFAB_INPUT_NUMBER_PAD_PATH) as GameObject;
        GameObject obj = Instantiate(prefab, this.transform) as GameObject;
        obj.transform.localPosition = new Vector3(-480.0f, -200.0f, 0.0f);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.name = "Memo";
        _memoButton = obj.GetComponent<InputBasicButton>();
    }

    private void CreateNumberButton()
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
            _numberButton[i] = obj.GetComponent<InputBasicButton>();
        }
    }

    private void CreateDoActioButton()
    {
        GameObject prefab = Resources.Load(DefineData.PREFAB_INPUT_NUMBER_PAD_PATH) as GameObject;
        GameObject obj = obj = Instantiate(prefab, this.transform) as GameObject;
        obj.transform.localPosition = new Vector3(-350.0f, -200.0f, 0.0f);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.name = "Undo";
        _undoButton = obj.GetComponent<InputBasicButton>();

        obj = obj = Instantiate(prefab, this.transform) as GameObject;
        obj.transform.localPosition = new Vector3(350.0f, -200.0f, 0.0f);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.name = "Redo";
        _redoButton = obj.GetComponent<InputBasicButton>();

    } 
}
