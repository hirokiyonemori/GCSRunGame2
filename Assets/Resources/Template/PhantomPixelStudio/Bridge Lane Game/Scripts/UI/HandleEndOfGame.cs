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
        //[SerializeField] 
        //private FinishLine finishLine;
        [SerializeField] private EnemyAttack enemyAttack;

        [SerializeField]
        private FadeManager fadeManager;

        [SerializeField]
        private GameObjectController gameObjectController;

        private void Awake()
        {
            AudioClip audio = AudioManager.Instance.bgmStage;
            AudioManager.Instance.PlayBGM(audio);
            youWinScreen.SetActive(false); //turn off the screen at the start of the game
            gameOverScreen.SetActive(false); //turn off the screen at the start of the game
            Time.timeScale = 1f; //reset our time scale in cases where the scene was restarted
        }

        private void Start()
        {

            //gameObjectController.ToggleGameObject(0);
            fadeManager.StartFadeIn();
            PlayerUnitManager.UnitManager.GameOver +=
                GameOverScreen; //subscribe to the game over event from the unit manager


            enemyAttack.OnFinishLineTouch += WinGame; //subscribe to the win game event from the finish line
        }

        private void WinGame()
        {
            youWinScreen.SetActive(true);
            Time.timeScale = 0;
        }

        private void GameOverScreen()
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene("GameScene");
            // 時間を再開する
            Time.timeScale = 1;
            int stage = GameManager.Instance.GetStage();
            if (stage < 4)
            {
                GameManager.Instance.SetStage(stage + 1);
            }


            fadeManager.StartFadeOut("GameScene");

        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            //UnityEditor.EditorApplication.isPlaying = false;
#endif
            //Application.Quit();
            // 時間を再開する
            Time.timeScale = 1;
            AudioManager.Instance.StopBGM();
            fadeManager.StartFadeOut("MainMenu");
            //SceneManager.LoadScene("MainMenu");
        }
    }
}