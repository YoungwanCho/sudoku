using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupYesNo : MonoBehaviour, IPopupWindow
{
    [SerializeField]
    protected Text textTitle_ = null;
    [SerializeField]
    protected Text textMessage_ = null;
    [SerializeField]
    protected Button buttonYes_ = null;
    [SerializeField]
    protected Button buttonNo_ = null;

    protected System.Action<object> _actionYes = null;
    protected System.Action<object> _actionNo = null;

    public void Awake()
    {
        Debug.Log("PopupYesNo Awake");
        buttonYes_.onClick.AddListener(delegate { OnClickYes(this.gameObject); });
        buttonNo_.onClick.AddListener(delegate { OnClickNo(this.gameObject); });
    }

    public void Initialize(System.Action<object> onClickYes, System.Action<object> onClickNo)
    {
        _actionYes = onClickYes;
        _actionNo = onClickNo;
    } 

    public void OnOpenedPopup(GameObject popup)
    {

    }

    public bool OnClosedPopup()
    {
        return true;
    }

    public void OnClickYes(object obj)
    {
        if(_actionYes != null)
        {
            _actionYes(obj);
        }

        PopupManager.Instance.Close(this);
    }

    public void OnClickNo(object obj)
    {
        if(_actionNo != null)
        {
            _actionNo(obj);
        }

        PopupManager.Instance.Close(this);
    }
    
}
