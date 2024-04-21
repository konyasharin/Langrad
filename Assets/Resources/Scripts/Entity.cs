using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    protected EntityTypes EntityType;
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
    
    public void Interact()
    {
        switch (EntityType)
        {
            case EntityTypes.Character:
                StartCoroutine(GetComponent<Character>().StartDialog());
                break;
        }
    }
}
