using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

// テキスト送りを行うクラス
public class TextLoad : MonoBehaviour
{
    [SerializeField, Tooltip("表示するテキストの配列")]
    MessageData _scenarios;
    [SerializeField]
    PanelObjects _panelObjects;
    [SerializeField]
    Text _messageText;
    [SerializeField]
    Text _nameText;
    [SerializeField]
    Image _image;
    [Range(0.001f, 0.3f), SerializeField] float intervalForCharacterDisplay = 0.05f;



    [Tooltip("現在表示しているテキスト")]
    string _currentText = string.Empty;
    [Tooltip("現在表示しているテキストの文字数")]
    int _currentLine = 0;
    [Tooltip("現在表示しているテキストの末尾")]
    int _lastUpdateCharacter = -1;
    [Tooltip("文字送りのスピード")]
    float _timeUntilDisplay = 0;
    [Tooltip("読み終わった現在時間")]
    float _timeElapsed = 1;
    InputAsyncAwait _asyncAwait = new();


    public MessageData Scenarios => _scenarios;

    void Start()
    {
        _panelObjects.ActiveObj(true);
        SetNextLine();
        StartCoroutine(RunUpdate());
    }

    IEnumerator RunUpdate()
    {
        while (true)
        {
            yield return ActionOfMouseInput();
            yield return _asyncAwait.WaitForMouseButtonDown(out _);
            if (_currentLine == _scenarios.Messages.Length)
            {
                Debug.Log("Finish");
                _panelObjects.ActiveObj(false);
                break;
            }
            SetNextLine();
            yield return null;
        }
    }

    void LoadTextTime(IAwaiter<int> awaiter)
    {
        if (awaiter == null) return;
        switch (awaiter.Result)
        {
            case 1:
                if (_messageText.text.Length < _currentText.Length)
                {
                    intervalForCharacterDisplay = 0.001f;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator ActionOfMouseInput()
    {
        StartCoroutine(_asyncAwait.WaitForMouseButtonDown(out IAwaiter<int> input));
        while (_lastUpdateCharacter < _currentText.Length)
        {
            Debug.Log(input.Result);
            LoadTextTime(input);
            _timeUntilDisplay = _currentText.Length * intervalForCharacterDisplay;

            //現在表示されているテキストの文字数
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntilDisplay) * _currentText.Length);

            //テキストの更新
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _messageText.text = _currentText.Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
            }

            yield return null;
        }
        Debug.Log("f");
    }

    void SetNextLine()
    {
        intervalForCharacterDisplay = 0.05f;
        _nameText.text = _scenarios.Messages[_currentLine].NameStr;
        _currentText = _scenarios.Messages[_currentLine].MessageStr;
        _image.sprite = _scenarios.Messages[_currentLine].CharacterImage;
        switch (_scenarios.Messages[_currentLine].UnityEvents)
        {
            case EventState.Fade:
                _scenarios.LoadDelegate(this);
                break;
            default:
                break;
        }
        _currentLine++;

        _timeUntilDisplay = _currentText.Length * intervalForCharacterDisplay;
        _timeElapsed = Time.time;

        _lastUpdateCharacter = -1;
    }
}
