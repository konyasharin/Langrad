using System;
using UnityEngine.Events;

[Serializable]
public struct Choice
{
    public string text;
    public UnityEvent choiceEvent;
}
