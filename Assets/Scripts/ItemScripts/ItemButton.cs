using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public string url;

    private Button _button;

    public void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        if(url != String.Empty)
        Application.OpenURL(url);
    }

    public void Resize(Vector2 size)
    {
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
