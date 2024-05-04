using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsManager : MonoBehaviour
{
    private static DialogsManager _instance;
    public static DialogsManager Instance { get; }
    private DialogWindow _dialogWindow;
    private ChoicesWindow _choicesWindow;
    private Player _player;
    private Dictionary<PlotInfluence, int> _plotInfluences;
    public Dictionary<PlotInfluence, int> PlotInfluences { get; }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _dialogWindow = DialogWindow.GetInstance();
        _choicesWindow = ChoicesWindow.GetInstance();
        _player = Player.GetInstance();
        _plotInfluences = SaveLoadManager.LoadGame().PlotInfluences;
    }

    public IEnumerator StartDialog(Dialog dialog)
    {
        _player.moveIsBlock = true;
        yield return _dialogWindow.Activate();
        foreach (var sentence in dialog.sentences)
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

    public void ChangePlotInfluence(PlotInfluence plotInfluence, int countInfluence)
    {
        _plotInfluences[plotInfluence] += countInfluence;
        Debug.Log(_plotInfluences[plotInfluence]);
        SaveLoadManager.SaveGame();
    }
}
