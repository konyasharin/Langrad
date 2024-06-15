using UnityEngine;

namespace Resources.Scripts.DialogSystem
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Game/Dialog")]
    public class DialogScriptableObject: ScriptableObject
    {
        public Sentence[] sentences;
    }
}
