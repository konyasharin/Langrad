using System;

[Serializable]
public struct Dialog
{
    public DialogStatus status;
    public DialogScriptableObject scriptableObject;
    public Choice[] choices;
}
