using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    
    public abstract class PlayerBarBase : BarBase
    {
        public void Initialize()
        {
            MaxValue = GetValue();
            PlayerCharacter.Instance.OnUpdateStat.AddListener(UpdateValue);
        }
    }
}
