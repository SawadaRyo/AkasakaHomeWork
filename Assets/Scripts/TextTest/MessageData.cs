using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateMessageAsset")]
public class MessageData : ScriptableObject
{
    public delegate void TextLoadDelegate(TextLoad textLoad);

    [SerializeField]
    private Message[] _messages;

    event TextLoadDelegate _textLoadDelegate = null;

    public Message[] Messages => _messages;
    public TextLoadDelegate LoadDelegate { get => _textLoadDelegate; set => _textLoadDelegate = value; }
}

[Serializable]
public class Message
{
    [SerializeField]
    private string _nameStr = "–¼‘O";
    [SerializeField]
    private string _messageStr = "aaaaaaa";
    [SerializeField]
    private Sprite _characterImage = null;
    [SerializeField]
    private EventState _unityEvents = EventState.None;

    public string NameStr => _nameStr;
    public string MessageStr => _messageStr;
    public Sprite CharacterImage => _characterImage;
    public EventState UnityEvents => _unityEvents;
}

public enum EventState
{
    None,
    Fade
}

