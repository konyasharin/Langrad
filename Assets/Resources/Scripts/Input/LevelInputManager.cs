using Resources.Scripts.Actors.Player;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.Entities;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.UI.Inventory;
using UnityEngine;

namespace Resources.Scripts.Input
{
    public class LevelInputManager : TownInputManager
    {
        protected override void Update()
        {
            base.Update();
            CheckInventoryActivate();
            CheckMagicActivate();
            CheckUseQuickAccessInventory();
        }

        private void CheckInventoryActivate()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.inventoryActivateKey))
            {
                InventoryDisplay.Instance.gameObject.SetActive(!InventoryDisplay.Instance.gameObject.activeSelf);
            }
        }

        private void CheckMagicActivate()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.magicActivateKey))
            {
                MagicController.Instance.HandleKeyDown();
            }
        }

        private void CheckUseQuickAccessInventory()
        {
            for (int i = 0; i < QuickAccessInventory.Instance.KeysToUse.Length; i++)
            {
                if (UnityEngine.Input.GetKeyDown(QuickAccessInventory.Instance.KeysToUse[i]))
                {
                    QuickAccessInventory.Instance.HandleKeyDown(i);
                }
            }
        }
    }
}