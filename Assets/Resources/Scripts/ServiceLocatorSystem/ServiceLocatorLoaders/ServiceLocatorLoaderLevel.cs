using Resources.Scripts.Actors.Player;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.Input;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.LevelGenerate;
using Resources.Scripts.SaveLoadSystem;
using Resources.Scripts.Services;
using Resources.Scripts.Spawners;
using Resources.Scripts.UI;
using Resources.Scripts.UI.Bars.PlayerBars;
using Resources.Scripts.UI.Inventory;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.ServiceLocatorSystem.ServiceLocatorLoaders
{
    public class ServiceLocatorLoaderLevel : ServiceLocatorLoaderBase
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private ArmorBar armorBar;
        [SerializeField] private ManaBar manaBar;
        [SerializeField] private DeathWindow deathWindow;
        [SerializeField] private QuickAccessInventoryDisplay quickAccessInventoryDisplay;
        [SerializeField] private InventoryDisplay inventoryDisplay;
        [SerializeField] private RoomsManager roomsManager;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private Spawner spawner;
        [SerializeField] private CoroutinesManager coroutinesManager;
        [SerializeField] private DialogsManager dialogsManager;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private LevelInputManager levelInputManager;

        protected override void AddServices()
        {
            ServiceLocator.Instance.Add(levelGenerator);
            ServiceLocator.Instance.Add(healthBar);
            ServiceLocator.Instance.Add(armorBar);
            ServiceLocator.Instance.Add(manaBar);
            ServiceLocator.Instance.Add(deathWindow);
            ServiceLocator.Instance.Add(quickAccessInventoryDisplay);
            ServiceLocator.Instance.Add(inventoryDisplay);
            ServiceLocator.Instance.Add(roomsManager);
            ServiceLocator.Instance.Add(playerSpawner);
            ServiceLocator.Instance.Add(spawner);
            ServiceLocator.Instance.Add(coroutinesManager);
            ServiceLocator.Instance.Add(dialogsManager);
            ServiceLocator.Instance.Add(inventoryManager);
            ServiceLocator.Instance.Add(levelInputManager);
            
            ServiceLocator.Instance.Add(new SaveLoadManager());
        }

        protected override void InitializeServices()
        {
            ServiceLocator.Instance.Get<RoomsManager>().Initialize();
            ServiceLocator.Instance.Get<PlayerSpawner>().Initialize();
            
            ServiceLocator.Instance.Get<LevelGenerator>().Initialize();
            ServiceLocator.Instance.Get<LevelGenerator>().Generate();
            
            ServiceLocator.Instance.Get<PlayerCharacter>().Initialize();
            ServiceLocator.Instance.Get<HealthBar>().Initialize();
            ServiceLocator.Instance.Get<ArmorBar>().Initialize();
            ServiceLocator.Instance.Get<ManaBar>().Initialize();
            ServiceLocator.Instance.Get<DeathWindow>().Initialize();
            ServiceLocator.Instance.Get<QuickAccessInventoryDisplay>().Initialize();
            ServiceLocator.Instance.Get<InventoryDisplay>().Initialize();
            ServiceLocator.Instance.Get<DialogsManager>().Initialize();
            ServiceLocator.Instance.Get<LevelInputManager>().Initialize();
        }
    }
}