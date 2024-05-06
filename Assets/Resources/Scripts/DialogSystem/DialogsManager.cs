using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsManager : MonoBehaviour
{
    public static DialogsManager Instance { get; private set; }
    private DialogWindow _dialogWindow;
    private ChoicesWindow _choicesWindow;
    private Player _player;
    public Dictionary<PlotInfluenceType, int> PlotInfluences { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _dialogWindow = DialogWindow.Instance;
        _choicesWindow = ChoicesWindow.Instance;
        _player = Player.Instance;
        PlotInfluences = SaveLoadManager.LoadGame().PlotInfluences;
    }

    public IEnumerator StartDialog(Dialog dialog)
    {
        _player.moveIsBlock = true;
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
        _player.moveIsBlock = false;
    }

    public void ChangePlotInfluence(PlotInfluenceType plotInfluenceType, int countInfluence)
    {
        PlotInfluences[plotInfluenceType] += countInfluence;
        SaveLoadManager.SaveGame();
    }
}
