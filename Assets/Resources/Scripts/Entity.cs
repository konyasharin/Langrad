using System.Collections;
using UnityEngine;

namespace Resources.Scripts
{
    public abstract class Entity : MonoBehaviour
    {
        protected EntityType EntityType;
        [SerializeField] private GameObject keyObject;
        protected KeyCode KeyToInteract;
        protected bool InteractIsAvailable;

        public GameObject GetKeyObject()
        {
            return keyObject;
        }
    
        public KeyCode GetKeyToInteract()
        {
            return KeyToInteract;
        }

        public bool GetInteractIsAvailable()
        {
            return InteractIsAvailable;
        }
    
        public IEnumerator Interact()
        {
            switch (EntityType)
            {
                case EntityType.Character:
                    yield return StartCoroutine(GetComponent<Character>().StartDialog());
                    break;
            }
        }
    }
}
