using System;
using System.Collections;
using Resources.Scripts.DialogSystem;
using UnityEngine;
using System.Linq;

namespace Resources.Scripts
{
    public class Character : Entity
    {
        [field: SerializeField]
        public string CharacterName { get; private set; }
        [field: SerializeField] public Dialog[] Dialogs { get; private set; }

        private void Start()
        {
            EntityType = EntityType.Character;
            KeyToInteract = KeyCode.F;
            CheckInteractIsAvailable();
        }

        public void CheckInteractIsAvailable()
        {
            foreach (var dialog in Dialogs)
            {
                if (dialog.status == DialogStatus.Unblock && dialog.scriptableObject != null)
                {
                    InteractIsAvailable = true;
                    break;
                }

                InteractIsAvailable = false;
            }
        }
    
        public IEnumerator StartDialog()
        {
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i].status == DialogStatus.Unblock)
                {
                    yield return StartCoroutine(DialogsManager.Instance.StartDialog(Dialogs[i]));
                    Dialogs[i].status = DialogStatus.Completed;
                    break;
                }
            }
            CheckInteractIsAvailable();
        }
    }
}
