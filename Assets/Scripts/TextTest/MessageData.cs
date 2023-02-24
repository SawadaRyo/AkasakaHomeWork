using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateMessageAsset")]
public class MessageData : ScriptableObject
{
    [SerializeField]
    private Message[] _messages;

    public Message[] Messages => _messages;
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

    public string NameStr => _nameStr;
    public string MessageStr => _messageStr;
    public Sprite CharacterImage => _characterImage;

}

