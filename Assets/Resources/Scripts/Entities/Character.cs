using System.Collections;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    public class Character : Entity
    {
        [field: SerializeField] public string CharacterName { get; private set; }
        [field: SerializeField] public Dialog[] Dialogs { get; private set; }

        private DialogsManager _dialogsManager;

        protected override void Start()
        {
            base.Start();
            _dialogsManager = ServiceLocator.Instance.Get<DialogsManager>();
        }

        public override void CheckInteractIsAvailable()
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
    
        public override void Interact()
        {
            StartCoroutine(StartDialog());
        }

        private IEnumerator StartDialog()
        {
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i].status == DialogStatus.Unblock)
                {
                    yield return StartCoroutine(_dialogsManager.StartDialog(Dialogs[i]));
                    Dialogs[i].status = DialogStatus.Completed;
                    break;
                }
            }
            CheckInteractIsAvailable();
        }
    }
}
