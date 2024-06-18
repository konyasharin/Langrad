using System;
using UnityEngine;

namespace Resources.Scripts.DialogSystem
{
    [Serializable]
    public class DialogModel
    {
        [field: SerializeField] public Choice[] Choices { get; private set; }
        [field: SerializeField] public DialogScriptableObject DialogScriptableObject { get; private set; }
        [field: SerializeField] public DialogToggle[] Toggles { get; private set; }
        
        public DialogStatus status;
    }
}
