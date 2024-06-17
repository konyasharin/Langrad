using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractionManager : MonoBehaviour, IService
    {
        [SerializeField, Min(0.1f)] private float interactionRadius;
        private readonly List<Entity> _entitiesInInteractZone = new();
        private Entity _entityToInteract;
        private GameObject _keyObject;
        private List<Entity> _entitiesToInteract = new();
        private CircleCollider2D _collider;
        private PlayerCharacter _player;

        public void Initialize()
        {
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
        }
        
        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = interactionRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Entity newEntity = other.GetComponent<Entity>();
            if (newEntity != null)
            {
                _entitiesInInteractZone.Add(newEntity);
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
                RemoveEntityFromList(_entitiesInInteractZone, deleteEntity);
                RemoveEntityFromList(_entitiesToInteract, deleteEntity);
            }
        }

        private void ChangeActiveEntity()
        {
            _entitiesToInteract = new List<Entity>();
            foreach (var entity in _entitiesInInteractZone)
            {
                if (entity.InteractIsAvailable)
                {
                    _entitiesToInteract.Add(entity);
                }
            }
        
            if (_entitiesToInteract.Count == 0 || _player.moveIsBlock)
            {
                _entityToInteract = null;
                Destroy(_keyObject);
                return;
            }
            bool isChange = false;
            float minDistance = 0;
            if (_entityToInteract == null)
            {
                minDistance = Vector2.Distance(_entitiesToInteract[0].transform.position, _player.transform.position);
                _entityToInteract = _entitiesToInteract[0];
                isChange = true;
            }
            else
            {
                minDistance = Vector2.Distance(_entityToInteract.transform.position, _player.transform.position);
            }
            for (int i = 0; i < _entitiesToInteract.Count; i++)
            {
                float distance = Vector2.Distance(_entitiesToInteract[i].transform.position, _player.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _entityToInteract = _entitiesToInteract[i];
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
            Vector2 positionToSpawn = _entityToInteract.transform.position;
            positionToSpawn.y -= _entityToInteract.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f;
            _keyObject = Instantiate(_entityToInteract.KeyObject);
            _keyObject.transform.position = positionToSpawn;
        }

        public void HandleKeyDown()
        {
            if (_entityToInteract == null) return;
            _entityToInteract.Interact();
        }

        private void RemoveEntityFromList(List<Entity> entities, Entity entity)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] == entity)
                {
                    entities.RemoveAt(i);
                    if (entity == _entityToInteract)
                    {
                        _entityToInteract = null;
                    }
                    break;
                }
            }
            ChangeActiveEntity();
        }
    }
}
