using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class ArmorBar : PlayerBarBase
    {
        protected override float GetValue()
        {
            return Player.Armor;
        }
    }
}
