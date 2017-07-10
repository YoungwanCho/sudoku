using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupYesNoInputField : PopupYesNo
{
    [SerializeField]
    private InputField inputField_ = null;

    public new void Awake()
    {
        Debug.Log("PopupYesNoInputField Awake");
        buttonYes_.onClick.AddListener(delegate { OnClickYes(this.gameObject); });
        buttonNo_.onClick.AddListener(delegate { OnClickNo(this.gameObject); });
    }

    public new void OnClickYes(object obj)
    {
        if (_actionYes != null)
        {
            _actionYes(inputField_.text);
        }

        PopupManager.Instance.Close(this);
    }

    public new void OnClickNo(object obj)
    {
        if (_actionNo != null)
        {
            _actionNo(obj);
        }

        PopupManager.Instance.Close(this);
    }
}
