using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    private Dictionary<string, DialogStatuses> _dialogs;

    public void SaveDialogs(Dialog[] dialogs)
    {
        foreach (var dialog in dialogs)
        {
            _dialogs.Add("Vova", dialog.dialogStatus);   
        }
    }
}
