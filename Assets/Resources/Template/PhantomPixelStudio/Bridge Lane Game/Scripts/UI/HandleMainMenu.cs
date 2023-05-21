using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using Deniverse.UnityLocalizationSample.Presentation.Presenter;

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

        private void Awake()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);

            _admobLibrary = new AdmobLibrary();
            _admobLibrary.FirstSetting();
            //Admob�o�i�[�쐬
            _admobLibrary.RequestBanner(GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Bottom);
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