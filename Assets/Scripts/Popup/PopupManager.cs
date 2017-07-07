using UnityEngine;
using System.Collections.Generic;


public class PopupManager : UnitySingleton<PopupManager>
{
    //const int POPUP_DEPTH = 60000;

    private Dictionary<IPopupWindow, GameObject> _createdPopups = new Dictionary<IPopupWindow, GameObject>();
    private Transform _popupParent = null;
    //private int lastDepth = POPUP_DEPTH;
    //public int LastDepth
    //{
    //    set { lastDepth = value; /*DebugHelper.Log(string.Format("Last Depth Set : {0}", lastDepth));*/ }
    //    get { /*DebugHelper.Log(string.Format("Last Depth Get : {0}", lastDepth));*/ return lastDepth; }
    //}

    /**
     * @brief 팝업들이 생성되는 부모의 게임 오브젝트.
     */
    public Transform PopupParent
    {
        get
        {
            if(_popupParent == null)
            {
                Canvas[] canvasArr = FindObjectsOfType<Canvas>();
                for(int i=0; i<canvasArr.Length; i++)
                {
                    if(canvasArr[i].name.Equals("CanvasPopup"))
                    {
                        _popupParent = canvasArr[i].transform;
                    }
                }
            }
            _popupParent.SetAsLastSibling();
            return _popupParent;
        }
    }

    /**
     * @brief 팝업을 생성하고 연다.
     * @param prefabName 프리팹 이름.
     */
    public IPopupWindow Open<T>(string prefabName) where T: Component, IPopupWindow
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Popup/" + prefabName);
        GameObject contents = Instantiate(prefab) as GameObject;
        contents.transform.parent = this.PopupParent;
        contents.transform.localPosition = Vector3.zero;
        contents.transform.localRotation = Quaternion.identity;
        contents.transform.localScale = Vector3.one;
        contents.SetActive(true);
        //// 팝업에 이펙트가 필요한경우 OnOpenedPopup()함수를 통해 이펙트를 생성 Or 레이어 세팅을 해줄것!
        //GameObjectUtil.SetLayerRecursively(contents, "UI_Popup");

        //프리팹에서 인터페이스 클래스의 컴포넌트를 찾는다.
        IPopupWindow window = contents.transform.GetComponent<T>() as IPopupWindow;
        if (window == null)
        {
            //없으면 추가시켜줌.
            window = contents.AddComponent<T>();
        }

        window.OnOpenedPopup(contents);
        _createdPopups.Add(window, contents);
        return window;
    }

    /**
     * @brief 팝업을 닫는다.
     */
    public bool Close(IPopupWindow popup)
    {
        GameObject go = null;
        if (_createdPopups.TryGetValue(popup, out go))
        {
            bool result = popup.OnClosedPopup();
            if (result == false)
                return false;

            _createdPopups.Remove(popup);

            Destroy(go);
        }

        return true;
    }

    public bool IsOpened()
    {
        return (_createdPopups.Count > 0);
    }

    public bool CloseTopPopup()
    {
        if (IsOpened() == false)
            return false;

        foreach (KeyValuePair<IPopupWindow, GameObject> pair in _createdPopups)
        {
            GameObject popup = pair.Value;
            if (popup == null) continue;

            //UIPanel panel = popup.GetComponent<UIPanel>();
            //if (panel == null) continue;
            //if (LastDepth != panel.depth) continue;

            return this.Close(pair.Key);
        }

        return false;
    }
}
