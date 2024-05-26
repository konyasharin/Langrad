using UnityEngine;

namespace Resources.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewFireBall", menuName = "Game/Magics/FireBall")]
    public class MagicData : ScriptableObject
    {
        public GameObject prefab;
        public int damage;
    }
}