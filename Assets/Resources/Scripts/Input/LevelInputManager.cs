using Resources.Scripts.Actors.Player;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.InventoryDisplays;

namespace Resources.Scripts.Input
{
    public class LevelInputManager : TownInputManager
    {
        private InventoryDisplay _inventoryDisplay;
        private MagicController _magicController;
        private QuickAccessInventory _quickAccessInventory;

        public override void Initialize()
        {
            base.Initialize();
            _inventoryDisplay = ServiceLocator.Instance.Get<InventoryManager>().InventoryDisplay;
            _magicController = ServiceLocator.Instance.Get<PlayerCharacter>().MagicController;
            _quickAccessInventory = ServiceLocator.Instance.Get<InventoryManager>().QuickAccessInventory;
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
            for (int i = 0; i < _quickAccessInventory.KeysToUse.Length; i++)
            {
                if (UnityEngine.Input.GetKeyDown(_quickAccessInventory.KeysToUse[i]))
                {
                    _quickAccessInventory.HandleKeyDown(i);
                }
            }
        }
    }
}