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
        
        public Dictionary<PlotInfluenceType, int> PlotInfluences { get; private set; }
        
        private PlayerCharacter _player;
        private SaveLoadManager _saveLoadManager;

        public void Initialize()
        {
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            _saveLoadManager = ServiceLocator.Instance.Get<SaveLoadManager>();
            ChoicesWindow.Initialize();
            PlotInfluences = _saveLoadManager.LoadGame().PlotInfluences;
        }

        public IEnumerator StartDialog(Dialog dialog)
        {
            _player.moveIsBlock = true;
            yield return DialogWindow.Activate();
            foreach (var sentence in dialog.scriptableObject.sentences)
            {
                yield return DialogWindow.ShowText(sentence);
            }

            if (dialog.choices.Length != 0)
            {
                yield return ChoicesWindow.Activate(dialog.choices);
        
                while (ChoicesWindow.GetIsActive())
                {
                    yield return null;
                }
            }
            yield return DialogWindow.Deactivate();
            _player.moveIsBlock = false;
        }

        public void ChangePlotInfluence(PlotInfluenceType plotInfluenceType, int countInfluence)
        {
            PlotInfluences[plotInfluenceType] += countInfluence;
            _saveLoadManager.SaveGame();
        }
    }
}
