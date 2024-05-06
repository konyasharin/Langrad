using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Game/Dialog")]
public class DialogScriptableObject: ScriptableObject
{
    public Sentence[] sentences;
}
