using System;

[Serializable]
public struct Dialog
{
    public DialogStatuses dialogStatus;
    public Sentence[] sentences;
    public Choice[] choices;
}
