using System;
using Resources.Scripts.Actors.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.Canvas
{
    public class DeathWindow : MonoBehaviour
    {
        public static DeathWindow Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize()
        {
            PlayerCharacter.Instance.OnDeath.AddListener(ShowWindow);
            gameObject.SetActive(false);
        }

        private void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void GoToTown()
        {
            SceneManager.LoadScene(1);
        }
    }
}
