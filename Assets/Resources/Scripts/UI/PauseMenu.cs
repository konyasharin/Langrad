using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu Instance;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void UnPause()
        {
            gameObject.SetActive(false);
        }

        public void ToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}