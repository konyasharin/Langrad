using Resources.Scripts.Actors.Player;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.UI
{
    public class DeathWindow : MonoBehaviour, IService
    {
        private PlayerCharacter _player;

        public void Initialize()
        {
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            _player.OnDeath.AddListener(ShowWindow);
            gameObject.SetActive(false);
        }

        private void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void GoToTown()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}
