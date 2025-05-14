using UnityEngine;
using UnityEngine.SceneManagement;

namespace Equilibrium
{
    public class MacroGameHandler : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 300;
            EventManager.RaiseEvent(EventManager.EventType.OPEN_GAME);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            if (Input.GetKeyDown(KeyCode.Escape))
                Quit();
        }

        public void Reload() => Switch(SceneManager.GetActiveScene().name);

        public void Quit() 
        {
            print("quitting game ...");
            EventManager.RaiseEvent(EventManager.EventType.CLOSE_GAME);
            Application.Quit();
        }

        public void Switch(string name)
        {
            if (name == "Quit")
            {
                Quit();
                return;
            }
            
            SceneManager.LoadScene(name);
        }
    }
}