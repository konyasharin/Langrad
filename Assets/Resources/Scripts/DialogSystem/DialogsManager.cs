using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsManager : MonoBehaviour
{
    private static DialogsManager _instance;
    private DialogWindow _dialogWindow;
    private ChoicesWindow _choicesWindow;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _dialogWindow = DialogWindow.GetInstance();
        _choicesWindow = ChoicesWindow.GetInstance();
    }

    public static DialogsManager GetInstance()
    {
        return _instance;
    }

    public IEnumerator StartDialog(Dialog dialog)
    {
        yield return StartCoroutine(_dialogWindow.Activate());
        foreach (var sentence in dialog.sentences)
        {
            yield return StartCoroutine(_dialogWindow.ShowText(sentence));
        }
        
        yield return StartCoroutine(_choicesWindow.Activate(dialog.choices));
        
        yield return StartCoroutine(_dialogWindow.Deactivate());
    }
}
