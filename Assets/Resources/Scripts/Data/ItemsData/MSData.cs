using UnityEngine;

namespace Resources.Scripts.Data.ItemsData
{
    [CreateAssetMenu(fileName = "NewMagicScroll", menuName = "Game/Items/MagicScroll")]
    public class MSData : ItemData
    {
        [Min(1)]
        public int energyCosts;
        public MagicData magicData;
    }
}
