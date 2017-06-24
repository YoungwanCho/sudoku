using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPad : MonoBehaviour
{
    private DefaultButton[] _numberButton = new DefaultButton[DefineData.MAX_NUMBER_VALUE];

    private DefaultButton _undoButton = null;
    private DefaultButton _redoButton = null;
    private DefaultButton _memoButton = null;
    private DefaultButton _deleteButton = null;
    private DefaultButton _quitButton = null;

    public void Awake()
    {
        CreateNumberButton();
        CreateDoActioButton();
        CreateMemoButton();
        CreateDeleteButton();
        CreateQuitButton();
    }

    public void Initialize(System.Action<GameObject> onClickNumberButton, 
        System.Action<GameObject> onClickDoAaction,
        System.Action<GameObject> onClickMemo,
        System.Action<GameObject> onClickDelete,
        System.Action<GameObject> onClickQuit)
    {
        for(int i=0; i< _numberButton.Length; i++)
        {
            _numberButton[i].Initialize(onClickNumberButton, Color.red, (i+1).ToString());
        }

        _undoButton.Initialize(onClickDoAaction, Color.black, "Undo");
        _redoButton.Initialize(onClickDoAaction, Color.black, "Redo");
        _memoButton.Initialize(onClickMemo, Color.green, "Memo");
        _deleteButton.Initialize(onClickDelete, Color.green, "Delete");
        _quitButton.Initialize(onClickQuit, Color.green, "Quit");

        this.UpdateMemoButton(false);
    }

    public void UpdateMemoButton(bool isOn)
    {
        Color color = isOn ? Color.green : Color.gray;
        _memoButton.UpdateButton(color);
    }

    private void CreateQuitButton()
    {
        _quitButton = InstantiateBasicButton("Quit", this.transform, new Vector3(0.0f, -300.0f, 0.0f), Quaternion.identity, Vector3.one);
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
