using UnityEngine;

namespace Resources.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health;
        [field: SerializeField] public int SpawnPrice { get; set; }
    }
}
