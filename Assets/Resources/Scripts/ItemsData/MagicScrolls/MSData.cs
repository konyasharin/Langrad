using Resources.Scripts.Items.MagicScrolls;
using UnityEngine;

namespace Resources.Scripts.ItemsData.MagicScrolls
{
    [CreateAssetMenu(fileName = "NewMagicScroll", menuName = "Game/Items/MagicScroll")]
    public class MSData : ItemData
    {
        [Min(1)]
        public int energyCosts;
        public MSType type;
    }
}
