using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Entity _entity;
    private GameObject _keyObject;
    private readonly List<Entity> _entitiesToInteract = new List<Entity>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity newEntity = other.GetComponent<Entity>();
        if (newEntity != null && newEntity.GetInteractIsAvailable())
        {
            _entitiesToInteract.Add(newEntity);
            ChangeActiveEntity();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ChangeActiveEntity();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Entity deleteEntity = other.GetComponent<Entity>();
        if (deleteEntity != null)
        {
            for (int i = 0; i < _entitiesToInteract.Count; i++)
            {
                if (_entitiesToInteract[i] == deleteEntity)
                {
                    _entitiesToInteract.RemoveAt(i);
                }
            }
        }
    }

    private void ChangeActiveEntity()
    {
        if (_entitiesToInteract.Count == 0)
        {
            _entity = null;
            Destroy(_keyObject);
            return;
        }
        bool isChange = false;
        float minDistance = 0;
        if (_entity == null)
        {
            minDistance = Vector2.Distance(_entitiesToInteract[0].transform.position, Player.GetInstance().transform.position);
            _entity = _entitiesToInteract[0];
            isChange = true;
        }
        else
        {
            minDistance = Vector2.Distance(_entity.transform.position, Player.GetInstance().transform.position);
        }
        for (int i = 0; i < _entitiesToInteract.Count; i++)
        {
            float distance = Vector2.Distance(_entitiesToInteract[i].transform.position, Player.GetInstance().transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _entity = _entitiesToInteract[i];
                isChange = true;
            }
        }

        if (isChange)
        {
            Destroy(_keyObject);
            SpawnKeyObject();
        }
    }

    private void SpawnKeyObject()
    {
        Vector2 positionToSpawn = _entity.transform.position;
        positionToSpawn.y -= _entity.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f;
        _keyObject = Instantiate(_entity.GetKeyObject());
        _keyObject.transform.position = positionToSpawn;
    }

    private void Update()
    {
        if (_entity == null) return;
        if (Input.GetKeyDown(_entity.GetKeyToInteract()))
        {
            _entity.Interact();
        }
    }
}
