using System;
using UnityEngine;

[Serializable]
public struct Sentence
{
    public string name;
    [TextArea]
    public string text;
}
