using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicButton : MonoBehaviour
{
    [SerializeField]
    private Button _button = null;
    [SerializeField]
    private Image _image = null;
    [SerializeField]
    private Text _text = null;

    public void Awake()
    {
        _image.sprite = Resources.Load("Image/white", typeof(Sprite)) as Sprite;
    }

    public void Initialize(System.Action<GameObject> onclick, Color color, string text)
    {
        _button.onClick.AddListener(delegate { onclick(this.gameObject); });
        _image.color = color;
        _text.text = text;
    }

    public void UpdateButton(Color color)
    {
        _image.color = color;
    }
}
