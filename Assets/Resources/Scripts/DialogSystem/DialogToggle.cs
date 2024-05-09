using System;

namespace Resources.Scripts.DialogSystem
{
    [Serializable]
    public struct DialogToggle
    {
        public Character character;
        public string dialogName;
        public DialogStatus newDialogStatus;
    }
}
