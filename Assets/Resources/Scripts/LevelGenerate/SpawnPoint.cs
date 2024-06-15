using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class SpawnPoint : MonoBehaviour
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }
        [field: SerializeField]
        public SpawnPointType SpawnPointType { get; private set; }
    }
}
