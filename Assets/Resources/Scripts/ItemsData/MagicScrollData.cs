using UnityEngine;

namespace Resources.Scripts.ItemsData
{
    [CreateAssetMenu(fileName = "NewMagicScroll", menuName = "Game/Item/MagicScroll")]
    public class MagicScrollData : ItemData
    {
        [Min(1)]
        public int energyCosts;
    }
}
