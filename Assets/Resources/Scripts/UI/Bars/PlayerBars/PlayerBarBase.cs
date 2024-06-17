using Resources.Scripts.Actors.Player;
using Resources.Scripts.ServiceLocatorSystem;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    
    public abstract class PlayerBarBase : BarBase, IService
    {
        protected PlayerCharacter Player;

        public void Initialize()
        {
            Player = ServiceLocator.Instance.Get<PlayerCharacter>();
            MaxValue = GetValue();
            Player.OnUpdateStat.AddListener(UpdateValue);
        }
    }
}
