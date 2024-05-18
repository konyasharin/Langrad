using System.Collections;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Entity : MonoBehaviour
    {
        [field: SerializeField] 
        public GameObject KeyObject { get; private set; }
        [field: SerializeField]
        public KeyCode KeyToInteract { get; private set; }
        public bool InteractIsAvailable { get; protected set; }

        public abstract void Interact();
    }
}
