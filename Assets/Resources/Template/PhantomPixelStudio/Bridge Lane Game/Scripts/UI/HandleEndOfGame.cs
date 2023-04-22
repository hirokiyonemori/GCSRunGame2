using System.Collections;
using System.Collections.Generic;
using LaneGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaneGame
{
    public class HandleEndOfGame : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject youWinScreen;
        [SerializeField] private FinishLine finishLine;

        private void Awake()
        {
            youWinScreen.SetActive(false); //turn off the screen at the start of the game
            gameOverScreen.SetActive(false); //turn off the screen at the start of the game
            Time.timeScale = 1f; //reset our time scale in cases where the scene was restarted
        }

        private void Start()
        {
            PlayerUnitManager.UnitManager.GameOver +=
                GameOverScreen; //subscribe to the game over event from the unit manager
            finishLine.OnFinishLineTouch += WinGame; //subscribe to the win game event from the finish line
        }

        private void WinGame()
        {
            //we won, lets display the win screen and pause the game
            youWinScreen.SetActive(true);
            Time.timeScale = 0;
        }

        private void GameOverScreen()
        {
            //we lost, lets display the game over screen and pause the game
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void RestartLevel()
        {
            //this reloads the scene to play again
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            //this checks if we're in the editor or a build and quits appropriately
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}