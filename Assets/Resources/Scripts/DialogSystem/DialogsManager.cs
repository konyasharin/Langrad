using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.SaveLoadSystem;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

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
    }
}
