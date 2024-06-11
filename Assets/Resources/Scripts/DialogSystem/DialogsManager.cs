using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.SaveLoadSystem;
using UnityEngine;

namespace Resources.Scripts.DialogSystem
{
    public class DialogsManager : MonoBehaviour
    {
        public static DialogsManager Instance { get; private set; }
        private DialogWindow _dialogWindow;
        private ChoicesWindow _choicesWindow;
        private PlayerCharacter _playerCharacter;
        public Dictionary<PlotInfluenceType, int> PlotInfluences { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _dialogWindow = DialogWindow.Instance;
            _choicesWindow = ChoicesWindow.Instance;
            _playerCharacter = PlayerCharacter.Instance;
            PlotInfluences = SaveLoadManager.LoadGame().PlotInfluences;
        }

        public IEnumerator StartDialog(Dialog dialog)
        {
            _playerCharacter.moveIsBlock = true;
            yield return _dialogWindow.Activate();
            foreach (var sentence in dialog.scriptableObject.sentences)
            {
                yield return _dialogWindow.ShowText(sentence);
            }

            if (dialog.choices.Length != 0)
            {
                yield return _choicesWindow.Activate(dialog.choices);
        
                while (_choicesWindow.GetIsActive())
                {
                    yield return null;
                }
            }
            yield return _dialogWindow.Deactivate();
            _playerCharacter.moveIsBlock = false;
        }

        public void ChangePlotInfluence(PlotInfluenceType plotInfluenceType, int countInfluence)
        {
            PlotInfluences[plotInfluenceType] += countInfluence;
            SaveLoadManager.SaveGame();
        }
    }
}
