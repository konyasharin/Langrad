using Resources.Scripts.Actors.Player;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.Input;
using Resources.Scripts.SaveLoadSystem;
using Resources.Scripts.Services;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.ServiceLocatorSystem.ServiceLocatorLoaders
{
    public class ServiceLocatorLoaderTown : ServiceLocatorLoaderBase
    {
        [SerializeField] private CoroutinesManager coroutinesManager;
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private DialogsManager dialogsManager;
        [SerializeField] private TownInputManager townInputManager;
        
        protected override void AddServices()
        {
            ServiceLocator.Instance.Add(coroutinesManager);
            ServiceLocator.Instance.Add(player);
            ServiceLocator.Instance.Add(dialogsManager);
            ServiceLocator.Instance.Add(townInputManager);
            
            ServiceLocator.Instance.Add(new SaveLoadManager());
        }

        protected override void InitializeServices()
        {
            ServiceLocator.Instance.Get<PlayerCharacter>().Initialize();
            ServiceLocator.Instance.Get<DialogsManager>().Initialize();
            ServiceLocator.Instance.Get<TownInputManager>().Initialize();
        }
    }
}