using System;

[Serializable]
public struct Dialog
{
    public DialogStatus status;
    public DialogScriptableObject scriptableObject;
    public Choice[] choices;

    // public Dialog(DialogStatus status, DialogScriptableObject scriptableObject, Choice[] choices)
    // {
    //     this.status = status;
    // }

    public void ToggleStatus(DialogStatus newStatus)
    {
        this.status = newStatus;
    }
}
