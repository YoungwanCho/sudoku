using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton : MonoBehaviour
{
    [SerializeField]
    private Button _button = null;
    [SerializeField]
    private Image _image = null;
    [SerializeField]
    private Text _text = null;

    public void Initialize(System.Action<GameObject> onclick, string imageName, string text)
    {
        _button.onClick.AddListener(delegate { onclick(this.gameObject); });
        _image.sprite = Resources.Load(string.Format("Image/{0}", imageName), typeof(Sprite)) as Sprite;
        _text.text = text;
    }

    public void UpdateButton(Color color)
    {
        _image.color = color;
    }
} 
