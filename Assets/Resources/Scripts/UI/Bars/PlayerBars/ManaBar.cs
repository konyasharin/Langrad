using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class ManaBar : PlayerBarBase
    {
        public static ManaBar Instance;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override float GetValue()
        {
            return PlayerCharacter.Instance.Mana;
        }
    }
}