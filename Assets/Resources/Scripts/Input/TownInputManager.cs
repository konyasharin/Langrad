using Resources.Scripts.Actors.Player;
using Resources.Scripts.Data;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.Entities;
using Resources.Scripts.InventorySystem;
using UnityEngine;

namespace Resources.Scripts.Input
{
    public class TownInputManager : MonoBehaviour
    {
        [SerializeField] protected InputData inputData;

        protected virtual void Update()
        {
            CheckSkipDialog();
            CheckInteract();
            UpdatePlayerSpeed();
        }

        private void CheckSkipDialog()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.skipDialogKey))
            {
                DialogWindow.Instance.HandleKeyDown();
            }
        }

        private void CheckInteract()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.entityInteractKey))
            {
                InteractionManager.Instance.HandleKeyDown();
            }
        }

        private void UpdatePlayerSpeed()
        {
            PlayerCharacter.Instance.speedX = UnityEngine.Input.GetAxis("Horizontal");
            PlayerCharacter.Instance.speedY = UnityEngine.Input.GetAxis("Vertical");
        }
    }
}