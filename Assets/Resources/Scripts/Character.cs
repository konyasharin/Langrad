using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField]
    private string characterName;
    [SerializeField] private Dialog[] dialogs;
    private void Start()
    {
        EntityType = EntityType.Character;
        KeyToInteract = KeyCode.F;
        CheckInteractIsAvailable();
    }

    private void CheckInteractIsAvailable()
    {
        foreach (var dialog in dialogs)
        {
            if (dialog.status == DialogStatus.Unblock)
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
            if (dialogs[i].status == DialogStatus.Unblock)
            {
                yield return StartCoroutine(DialogsManager.Instance.StartDialog(dialogs[i]));
                dialogs[i].status = DialogStatus.Completed;
                break;
            }
        }
        CheckInteractIsAvailable();
    }
}
