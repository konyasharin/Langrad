using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.Entities
{
    public class Portal : Entity
    {
        [SerializeField]
        private int sceneIndex;

        private void Start()
        {
            InteractIsAvailable = true;
        }

        public override void Interact()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
