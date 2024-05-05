using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            RemoveEntityToInteract(deleteEntity);
        }
    }

    private void ChangeActiveEntity()
    {
        if (_entitiesToInteract.Count == 0 || Player.Instance.moveIsBlock)
        {
            _entity = null;
            Destroy(_keyObject);
            return;
        }
        bool isChange = false;
        float minDistance = 0;
        if (_entity == null)
        {
            minDistance = Vector2.Distance(_entitiesToInteract[0].transform.position, Player.Instance.transform.position);
            _entity = _entitiesToInteract[0];
            isChange = true;
        }
        else
        {
            minDistance = Vector2.Distance(_entity.transform.position, Player.Instance.transform.position);
        }
        for (int i = 0; i < _entitiesToInteract.Count; i++)
        {
            float distance = Vector2.Distance(_entitiesToInteract[i].transform.position, Player.Instance.transform.position);
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
            StartCoroutine(Interact());
        }
    }

    private void RemoveEntityToInteract(Entity entity)
    {
        for (int i = 0; i < _entitiesToInteract.Count(); i++)
        {
            if (_entitiesToInteract[i] == entity)
            {
                _entitiesToInteract.RemoveAt(i);
                if (entity == _entity)
                {
                    _entity = null;
                    ChangeActiveEntity();
                }
            }
        }
    }

    private IEnumerator Interact()
    {
        Entity cachedEntity = _entity;
        yield return _entity.Interact();
        if (!cachedEntity.GetInteractIsAvailable())
        {
            RemoveEntityToInteract(cachedEntity);
        }
    }
}
