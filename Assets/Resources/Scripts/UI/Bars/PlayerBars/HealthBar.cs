using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class HealthBar : PlayerBarBase
    {
        protected override float GetValue()
        {
            return Player.Health;
        }
    }
}
