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
    private InputBasicButton _deleteButton = null;

    public void Awake()
    {
        CreateNumberButton();
        CreateDoActioButton();
        CreateMemoButton();
        CreateDeleteButton();
    }

    public void Initialize(controller.GameController.OnClickInputPad onClickNumberButton, 
        controller.GameController.OnClickInputPad onClickDoAaction,
        controller.GameController.OnClickInputPad onClickMemo,
        controller.GameController.OnClickInputPad onClickDelete)
    {
        for(int i=0; i< _numberButton.Length; i++)
        {
            _numberButton[i].Initialize(onClickNumberButton, "cell_red", (i+1).ToString());
        }

        _undoButton.Initialize(onClickDoAaction, "cell_black", "Undo");
        _redoButton.Initialize(onClickDoAaction,"cell_black", "Redo");
        _memoButton.Initialize(onClickMemo, "cell_green", "Memo");
        _deleteButton.Initialize(onClickDelete, "cell_green", "Delete");

        this.UpdateMemoButton(false);
    }

    public void UpdateMemoButton(bool isOn)
    {
        Color color = isOn ? Color.white : Color.gray;
        _memoButton.UpdateButton(color);
    }

    private void CreateDeleteButton()
    {
        _deleteButton = InstantiateBasicButton("Delete", this.transform, new Vector3(480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
    }

    private void CreateMemoButton()
    {
        _memoButton = InstantiateBasicButton("Memo", this.transform, new Vector3(-480.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
    }

    private void CreateNumberButton()
    {
        float totalWidth = (DefineData.MAX_NUMBER_VALUE * DefineData.NUMBERSIZE.x) + DefineData.MAX_NUMBER_VALUE;
        float startX = (-totalWidth * 0.5f) + DefineData.NUMBERSIZE.x * 0.5f;
        
        for(int i=0; i<DefineData.MAX_NUMBER_VALUE; i++)
        {
            _numberButton[i] = InstantiateBasicButton((i + 1).ToString(), this.transform, new Vector3(startX + (DefineData.NUMBERSIZE.x * i) + (i + 1), 0.0f, 0.0f), Quaternion.identity, Vector3.one);
        }
    }

    private void CreateDoActioButton()
    {
        _undoButton = InstantiateBasicButton("Undo", this.transform, new Vector3(-350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
        _redoButton = InstantiateBasicButton("Redo", this.transform, new Vector3(350.0f, -200.0f, 0.0f), Quaternion.identity, Vector3.one);
    }
    
    private InputBasicButton InstantiateBasicButton(string objName, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale)
    {
        GameObject prefab = Resources.Load(DefineData.PREFAB_INPUT_NUMBER_PAD_PATH) as GameObject;
        GameObject obj = obj = Instantiate(prefab, parent) as GameObject;
        obj.transform.localPosition = localPos;
        obj.transform.localRotation = localRot;
        obj.transform.localScale = localScale;
        obj.name = objName;
        return obj.GetComponent<InputBasicButton>();
    }
}
