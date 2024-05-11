using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class RoomControl : MonoBehaviour
    {
        [field: SerializeField] public GameObject PassageSpawnPoints { get; private set; }

        public void SpawnPassage(Direction direction)
        {
            foreach (var passageSpawnPointTransform in GetComponentsInChildren<Transform>())
            {
                
            }
        }
    }
}
