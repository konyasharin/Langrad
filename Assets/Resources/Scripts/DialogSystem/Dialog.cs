using System;

namespace Resources.Scripts.DialogSystem
{
    [Serializable]
    public struct Dialog
    {
        public DialogStatus status;
        public DialogScriptableObject scriptableObject;
        public Choice[] choices;

        public void ToggleStatus(DialogStatus newStatus)
        {
            this.status = newStatus;
        }
    }
}
