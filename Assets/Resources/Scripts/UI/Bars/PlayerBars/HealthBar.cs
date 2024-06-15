using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.UI.Bars.PlayerBars
{
    public class HealthBar : PlayerBarBase
    {
        public static HealthBar Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override float GetValue()
        {
            return PlayerCharacter.Instance.Health;
        }
    }
}
