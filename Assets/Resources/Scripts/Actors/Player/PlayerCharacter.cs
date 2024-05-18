using System;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCharacter : Actor
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");
        public static PlayerCharacter Instance { get; private set; }
        public Collider2D Collider { get; private set; }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            Collider = GetComponent<Collider2D>();
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
                Animator.SetBool(IsRun, false);
            }
        }

        protected override void Move()
        {
            float speedX = Input.GetAxis("Horizontal");
            float speedY = Input.GetAxis("Vertical");
            var position = transform.position;
            transform.position = new Vector2(speedX * Speed * Time.deltaTime + position.x, 
                speedY * Speed * Time.deltaTime + position.y);
            if (Math.Abs(speedX) > 0 || Math.Abs(speedY) > 0)
            {
                Animator.SetBool(IsRun, true);
            }
            else
            {
                Animator.SetBool(IsRun, false);
            }

            if (speedX < 0)
            {
                SpriteRenderer.flipX = false;
            }
            else if (speedX > 0)
            {
                SpriteRenderer.flipX = true;
            }
        }
    }
}
