using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//フェード処理を行うクラス
public class ImageFade : MonoBehaviour
{
    [SerializeField] Image _image = null;

    TextLoad _textLoad = null;
    float _fadeOutColor = 1f;
    float _fadeInColor = 0f;
    float _fadeTime = 1f;
    void OnEnable()
    {
        _textLoad = GameObject.FindObjectOfType<TextLoad>();
        _textLoad.LoadDelegate += FadeIn;
        //_textLoad.LoadDelegate += FadeOut;
    }

    // Update is called once per frame
    void OnDisable()
    {
        _textLoad.LoadDelegate -= FadeIn;
        //_textLoad.LoadDelegate -= FadeOut;
    }
    void FadeIn()
    {
        Color c = _image.material.color;
        DOTween.To(x => c.a = x
                    , _fadeOutColor
                    , _fadeInColor, _fadeTime)
                    .OnUpdate(() => _image.color = c);
    }

    void FadeOut()
    {
        Color c = _image.material.color;
        DOTween.To(x => c.a = x
                    , _fadeInColor
                    , _fadeOutColor, _fadeTime)
                    .OnUpdate(() => _image.color = c);
    }
}
