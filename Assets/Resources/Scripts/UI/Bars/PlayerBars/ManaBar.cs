using Resources.Scripts.Actors.Player;
using Resources.Scripts.Actors.Player.ManaSystem;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class ManaBar : PlayerBarBase
    {
        
        protected override float GetValue()
        {
            return Player.ManaController.Mana;
        }
    }
}