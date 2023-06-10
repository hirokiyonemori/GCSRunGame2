using System.Collections;
using System.Collections.Generic;
using LaneGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


        /// <summary>
        /// �A�h���u�̃N���X
        /// </summary>
        private AdmobLibrary _admobLibrary;

        [SerializeField]
        private Button restartButton;



        private void Awake()
        {

            _admobLibrary = new AdmobLibrary();
            //_admobLibrary.InitReward();
            _admobLibrary.FirstSetting();

            bool existsKey = ES3.KeyExists("Tutorial");
            int tutorialNo = 0;
            if (existsKey)
            {
                tutorialNo = ES3.Load<int>("Tutorial");
            }
            AudioClip audio = tutorialNo < TutorialManager.TUTORIAL_END ? AudioManager.Instance.bgmStage : AudioManager.Instance.battleBgm;

            AudioManager.Instance.PlayBGM(audio);
            AudioManager.Instance.SetLoop(true);
            youWinScreen.SetActive(false); //turn off the screen at the start of the game
            gameOverScreen.SetActive(false); //turn off the screen at the start of the game
            Time.timeScale = 1f; //reset our time scale in cases where the scene was restarted

            GameManager.Instance.PlayCnt = 0;

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
            AudioManager.Instance.PlayBGM(AudioManager.Instance.winBgm);
            AudioManager.Instance.SetLoop(false);

            int clearNo = !GameManager.Instance.HardMode ? (GameManager.Instance.GetStage() + 1) : (GameManager.Instance.GetStage() + 1 + StageManager.STAGE_MAX);
            LogSystem.Log(" clearNo " + clearNo);

            // ステージ番号を更新する
            if (GameManager.Instance.StageClear <= clearNo)
            {
                GameManager.Instance.StageClear = clearNo;
                ES3.Save("StageClear", GameManager.Instance.StageClear);
            }

            Time.timeScale = 0;
        }

        private void GameOverScreen()
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void RestartLevel(bool win)
        {
            //SceneManager.LoadScene("GameScene");
            // 時間を再開する
            if (win)
            {
                int stage = GameManager.Instance.GetStage();
                //LogSystem.Log(" GameManager.Instance.GetStage() " + GameManager.Instance.GetStage());
                if (stage + 1 < StageManager.STAGE_MAX)
                {
                    GameManager.Instance.SetStage(stage + 1);
                }
                else
                {
                    //LogSystem.Log("一番最後の場合はランダムでステージに戻す");
                    int randomNo = Random.Range(0, StageManager.STAGE_MAX);
                    //LogSystem.Log("一番最後の場合はランダムでステージに戻す" + randomNo);
                    GameManager.Instance.SetStage(randomNo);
                }
                Time.timeScale = 1;
                fadeManager.StartFadeOut("GameScene");

            }
            else
            {
                _admobLibrary.PlayInterstitial();
                //_admobLibrary.ShowReawrd();
                //_admobLibrary.OnReward += (double value) =>

#if UNITY_ANDROID
                if (GameManager.Instance.PlayCnt <= 0)
                {
                    gameOverScreen.SetActive(false);
                    PlayerUnitManager.UnitManager.HandleUnits(10);
                    if (PlayerUnitManager.hasBattleStarted)
                    {
                        PlayerUnitManager.UnitManager.AnimationPlay();
                        StartCoroutine(enemyAttack.AttackPlayer());
                    }
                    AudioManager.Instance.PlayBGM(AudioManager.Instance.battleBgm);
                    GameManager.Instance.PlayCnt = 1;
                    restartButton.interactable = false;
                    PlayerUnitManager.UnitManager.StartInvincibility();
                }
                Time.timeScale = 1;
#else
                _admobLibrary.onIntersitial += (bool b) =>
                {

                    if (GameManager.Instance.PlayCnt <= 0)
                    {
                        gameOverScreen.SetActive(false);
                        PlayerUnitManager.UnitManager.HandleUnits(10);
                        if (PlayerUnitManager.hasBattleStarted)
                        {
                            PlayerUnitManager.UnitManager.AnimationPlay();
                            StartCoroutine(enemyAttack.AttackPlayer());
                        }
                        AudioManager.Instance.PlayBGM(AudioManager.Instance.battleBgm);
                        GameManager.Instance.PlayCnt = 1;
                        restartButton.interactable = false;
                        PlayerUnitManager.UnitManager.StartInvincibility();
                    }
                    Time.timeScale = 1;
                };
#endif
                restartButton.interactable = false;


            }



        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            //UnityEditor.EditorApplication.isPlaying = false;
#endif
            //Application.Quit();
            // 時間を再開する
            PlayerUnitManager.UnitManager.isEnd = true;
            Time.timeScale = 1;
            AudioManager.Instance.StopBGM();
            fadeManager.StartFadeOut("MainMenu");
            //SceneManager.LoadScene("MainMenu");
        }
    }
}