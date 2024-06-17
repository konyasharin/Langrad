using System;
using Resources.Scripts.Actors.Player.ManaSystem;
using Resources.Scripts.Entities;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Actors.Player
{
    public class PlayerCharacter : Actor, IService
    {
        [SerializeField] private ManaSettings manaSettings;
        [SerializeField] private InteractionManager interactionManager;
        
        [field: SerializeField, Min(0)] public int Armor { get; protected set; }
        
        [HideInInspector] public float speedX;
        [HideInInspector] public float speedY;
        
        public AnimationsController AnimationsController { get; private set; }
        public MagicController MagicController { get; private set; }
        public ManaController ManaController { get; private set; }
        public UnityEvent OnDeath { get; private set; } = new();
        
        private int _maxHealth;

        public void Initialize()
        {
            AnimationsController = new AnimationsController();
            MagicController = new MagicController();
            
            AnimationsController.Initialize(MagicController);
            MagicController.Initialize(AnimationsController);
            
            ManaController = new ManaController(manaSettings);

            ServiceLocator.Instance.Add(interactionManager);
            ServiceLocator.Instance.Get<InteractionManager>().Initialize();
        }
        
        protected override void Awake()
        {
            base.Awake();
            _maxHealth = Health;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    

        private void Update()
        {
            if (!moveIsBlock)
            {
                Move();   
            }
            else
            {
                Rb.velocity = Vector2.zero;
            }
            
            AnimationsController.UpdateSpeed();
        }

        protected override void Move()
        {
            Rb.velocity = new Vector2(speedX * Speed, speedY * Speed);

            if (speedX < 0)
            {
                SpriteRenderer.flipX = false;
            }
            else if (speedX > 0)
            {
                SpriteRenderer.flipX = true;
            }
        }

        public override void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                if (Armor - damage >= 0)
                {
                    Armor -= damage;
                }
                else
                {
                    damage -= Armor;
                    Health -= damage;
                    Armor = 0;
                }
                OnUpdateStat.Invoke();
            }

            if (Health <= 0)
            {
                Death();
            }
        }

        public void Heal(int healValue)
        {
            if (healValue > 0)
            {
                if (Health + healValue > _maxHealth)
                {
                    Health = _maxHealth;
                }
                else
                {
                    Health += healValue;
                }
                OnUpdateStat.Invoke();
            }
        }

        protected override void Death()
        {
            OnDeath.Invoke();
            Time.timeScale = 0;
        }

        public void OnWaitMagickAttack()
        {
            StartCoroutine(AnimationsController.WaitMagicAttack());
        }
    }
}
