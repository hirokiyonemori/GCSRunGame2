using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using Deniverse.UnityLocalizationSample.Presentation.Presenter;
using UnityEngine.UI;

namespace LaneGame
{
    public class HandleMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;

        private bool isOptionsMenuOpen;


        /// <summary>
        /// �A�h���u�̃N���X
        /// </summary>
        private AdmobLibrary _admobLibrary;

        public FadeManager fadeManager;
        int selectedLanguageNo = 0;
        [SerializeField]
        SaveLoadManager saveLoadManager;

        [SerializeField]
        Button hardModeButton;

        [SerializeField]
        private List<GameObject> hardGameObjectList;


        [SerializeField]
        private List<GameObject> normalGameObjectList;


        [SerializeField]
        private List<Button> normalButtonList;



        private void Awake()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);

            _admobLibrary = new AdmobLibrary();
            _admobLibrary.FirstSetting();
            //Admob�o�i�[�쐬
            _admobLibrary.RequestBanner(GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Bottom);
            if (GameManager.Instance.StageClear >= StageManager.STAGE_MAX)
            {
                hardModeButton.interactable = true;
            }
            else
            {
                hardModeButton.interactable = false;
            }

            LogSystem.Log(" GameManager.Instance.StageClear " + GameManager.Instance.StageClear);
            addButton.interactable = true;
        }

        [SerializeField]
        private Button addButton;

        // インタースティシャル広告を再生し、プレイヤーの開始ユニットを1増やす方法を示しています。
        public void UnitStartAdd()
        {
            _admobLibrary.PlayInterstitial();
            int cnt = GameManager.Instance.GetPlayerStartUnit();
            GameManager.Instance.SetPlayerStartUnit(cnt + 1);
            addButton.interactable = false;
        }
        public void ChangeHardMode()
        {
            GameManager.Instance.HardMode = !GameManager.Instance.HardMode;
            ES3.Save("HardMode", GameManager.Instance.HardMode);
            if (GameManager.Instance.HardMode)
            {
                foreach (var gameObject in hardGameObjectList)
                {
                    gameObject.SetActive(true);
                }

                foreach (var gameObject in normalGameObjectList)
                {
                    gameObject.SetActive(false);
                }

            }
            else
            {
                //hardModeText.text = "Hard Mode: OFF";
                foreach (var gameObject in hardGameObjectList)
                {
                    gameObject.SetActive(false);
                }
                foreach (var gameObject in normalGameObjectList)
                {
                    gameObject.SetActive(true);
                }

            }
        }



        private async void Start()
        {
            // Localization の初期化が完了するまで待機します
            await LocalizationSettings.InitializationOperation.Task;
            fadeManager.StartFadeIn();
            // localizeation初期化が完了するまで
            saveLoadManager.Init();
        }

        public void StartGame(int no)
        {
            GameManager.Instance.SetStage(no);
            _admobLibrary.DestroyBanner();
            fadeManager.StartFadeOut("GameScene");
        }


        public void HandleOptionsMenu()
        {

            selectedLanguageNo = 0;
            //check if options are open or closed
            if (isOptionsMenuOpen)
            {
                //option menu is open so we need to close it and open the main menu
                isOptionsMenuOpen = !isOptionsMenuOpen; //set the bool to the opposite of its current value;
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
            else
            {
                isOptionsMenuOpen = !isOptionsMenuOpen; //set the bool to the opposite of its current value;
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
            }
        }



        public void QuitGame()
        {
            //this checks if we're in the editor or a build and quits appropriately
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            _admobLibrary.DestroyBanner();
            Application.Quit();
        }
    }
}