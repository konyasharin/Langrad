using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class ArmorBar : PlayerBarBase
    {
        public static ArmorBar Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override float GetValue()
        {
            return PlayerCharacter.Instance.Armor;
        }
    }
}
