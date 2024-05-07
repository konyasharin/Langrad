using System;
using UnityEngine;

[Serializable]
public struct DialogToggle
{
    public Character character;
    public string dialogName;
    public DialogStatus newDialogStatus;
}
