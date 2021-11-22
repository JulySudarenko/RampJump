using UnityEngine;

namespace Code.Controllers
{
    internal class StopGame
    {
        public void OnPause()
        {
            Time.timeScale = 0;
        }

        public void RunGame()
        {
            Time.timeScale = 1;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
