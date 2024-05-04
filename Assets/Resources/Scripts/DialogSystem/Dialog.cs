using System;

[Serializable]
public struct Dialog
{
    public DialogStatus dialogStatus;
    public Sentence[] sentences;
    public Choice[] choices;
}
