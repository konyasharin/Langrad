using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField]
    private string characterName;
    [SerializeField] private Dialog[] dialogs;
    private DialogsManager _dialogsManager;
    private void Start()
    {
        EntityType = EntityTypes.Character;
        _dialogsManager = DialogsManager.GetInstance();
        KeyToInteract = KeyCode.F;
        CheckInteractIsAvailable();
    }

    private void CheckInteractIsAvailable()
    {
        foreach (var dialog in dialogs)
        {
            if (dialog.dialogStatus == DialogStatuses.Unblock)
            {
                InteractIsAvailable = true;
                break;
            }

            InteractIsAvailable = false;
        }
    }
    
    public IEnumerator StartDialog()
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (dialogs[i].dialogStatus == DialogStatuses.Unblock)
            {
                yield return StartCoroutine(_dialogsManager.StartDialog(dialogs[i]));
                dialogs[i].dialogStatus = DialogStatuses.Completed;
                break;
            }
        }
        CheckInteractIsAvailable();
    }
}
