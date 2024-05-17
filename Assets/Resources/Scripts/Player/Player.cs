using System;
using UnityEngine;

namespace Resources.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [Min(0.1f)] public float speed = 1f;
        private Animator _animatorController;
        private SpriteRenderer _spriteRenderer;
        public static Player Instance { get; private set; }
        [HideInInspector] 
        public bool moveIsBlock = false;
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _animatorController = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    

        private void Update()
        {
            float speedX = Input.GetAxis("Horizontal");
            float speedY = Input.GetAxis("Vertical");
            if (!moveIsBlock)
            {
                transform.position = new Vector2(speedX * speed * Time.deltaTime + transform.position.x, 
                    speedY * speed * Time.deltaTime + transform.position.y);
                if (Math.Abs(speedX) > 0 || Math.Abs(speedY) > 0)
                {
                    _animatorController.SetBool("isRun", true);
                }
                else
                {
                    _animatorController.SetBool("isRun", false);
                }

                if (speedX < 0)
                {
                    _spriteRenderer.flipX = false;
                }
                else if (speedX > 0)
                {
                    _spriteRenderer.flipX = true;
                }
            }
            else
            {
                _animatorController.SetBool("isRun", false);
            }
        }
    }
}
