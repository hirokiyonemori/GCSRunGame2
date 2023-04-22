using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaneGame
{
    public class HandleMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;

        private bool isOptionsMenuOpen;

        private void Start()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void HandleOptionsMenu()
        {
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
            Application.Quit();
        }
    }
}