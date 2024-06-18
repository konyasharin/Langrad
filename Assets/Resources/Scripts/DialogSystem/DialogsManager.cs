using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.SaveLoadSystem;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.DialogSystem
{
    public class DialogsManager : MonoBehaviour, IService
    {
        [field: SerializeField] public ChoicesWindow ChoicesWindow { get; private set; }
        [field: SerializeField] public DialogWindow DialogWindow { get; private set; }
        
        public DialogsSaver DialogsSaver { get; private set; }
        public Dictionary<PlotInfluenceType, int> PlotInfluences { get; private set; }
        
        private PlayerCharacter _player;

        public void Initialize()
        {
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            ChoicesWindow.Initialize();
            DialogsSaver = new DialogsSaver();
            // PlotInfluences = _saveLoadManager.LoadGame().PlotInfluences;
        }

        public IEnumerator StartDialog(DialogModel dialog)
        {
            _player.moveIsBlock = true;
            yield return DialogWindow.Activate();
            foreach (var sentence in dialog.DialogScriptableObject.sentences)
            {
                yield return DialogWindow.ShowText(sentence);
            }

            if (dialog.Choices.Length != 0)
            {
                yield return ChoicesWindow.Activate(dialog.Choices);
        
                while (ChoicesWindow.GetIsActive())
                {
                    yield return null;
                }
            }
            
            ToggleDialogs(dialog.Toggles);
            
            dialog.status = DialogStatus.Completed;
            DialogsSaver.SaveDialog(dialog);
            
            yield return DialogWindow.Deactivate();
            _player.moveIsBlock = false;
        }

        public void ChangePlotInfluence(PlotInfluenceType plotInfluenceType, int countInfluence)
        {
            PlotInfluences[plotInfluenceType] += countInfluence;
            //_saveLoadManager.SaveGame();
        }

        public void ToggleDialogs(DialogToggle[] dialogToggles)
        {
            foreach (var dialogToggle in dialogToggles)
            {
                foreach (var dialog in dialogToggle.character.Dialogs)
                {
                    if (dialog.DialogScriptableObject == dialogToggle.dialogScriptableObject)
                    {
                        dialog.status = dialogToggle.newDialogStatus;
                        DialogsSaver.SaveDialog(dialog);
                    }
                    else
                    {
                        Debug.LogWarning($"Character with name '{dialogToggle.character.CharacterName}' " +
                                         $"doesn't have dialog {dialogToggle.dialogScriptableObject.name}");
                    }
                }
                
                dialogToggle.character.CheckInteractIsAvailable();
            }
        }
    }
}
