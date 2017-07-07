using UnityEngine;


public interface IPopupWindow
{
    void OnOpenedPopup(GameObject popup);
    bool OnClosedPopup();
}
