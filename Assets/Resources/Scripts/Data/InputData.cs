using UnityEngine;

namespace Resources.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewInputData", menuName = "Game/Input")]
    public class InputData : ScriptableObject
    {
        public KeyCode magicActivateKey;
        public KeyCode inventoryActivateKey;
        public KeyCode skipDialogKey;
        public KeyCode entityInteractKey;
        public KeyCode pauseActivateKey;
    }
}