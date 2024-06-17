using Resources.Scripts.Actors.Player;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.Inventory;

namespace Resources.Scripts.Input
{
    public class LevelInputManager : TownInputManager
    {
        private InventoryDisplay _inventoryDisplay;
        private MagicController _magicController;

        public override void Initialize()
        {
            base.Initialize();
            _inventoryDisplay = ServiceLocator.Instance.Get<InventoryDisplay>();
            _magicController = ServiceLocator.Instance.Get<PlayerCharacter>().MagicController;
        }

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
                _inventoryDisplay.gameObject.SetActive(!_inventoryDisplay.gameObject.activeSelf);
            }
        }

        private void CheckMagicActivate()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.magicActivateKey))
            {
                _magicController.HandleKeyDown();
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