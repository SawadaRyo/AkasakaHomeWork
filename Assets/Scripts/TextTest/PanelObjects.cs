using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelObjects : MonoBehaviour
{
    [SerializeField]
    Image[] _image = null;
    [SerializeField]
    Text[] _text = null;

    public void ActiveObj(bool isActive)
    {
        Array.ForEach(_image, x => x.enabled = isActive);
        Array.ForEach(_text, x => x.enabled = isActive);
    }
}
