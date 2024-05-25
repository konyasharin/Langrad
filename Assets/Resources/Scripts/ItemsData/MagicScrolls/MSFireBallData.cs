using UnityEngine;

namespace Resources.Scripts.ItemsData.MagicScrolls
{
    [CreateAssetMenu(fileName = "NewFireBall", menuName = "Game/Items/FireBall")]
    public class MSFireBallData : MSData
    {
        public GameObject prefab;
        public int damage;
    }
}