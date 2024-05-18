using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.Canvas
{
    public class MenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
