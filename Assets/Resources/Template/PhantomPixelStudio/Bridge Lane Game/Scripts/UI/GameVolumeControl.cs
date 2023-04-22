using System.Collections;
using System.Collections.Generic;
using LaneGame;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace LaneGame
{
//Controls volume values through a scriptable object that carries the values between scenes
    public class GameVolumeControl : MonoBehaviour
    {
        [SerializeField] private GameVolumeSO volumeSettings;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private bool debugMode;


        /// <summary>
        /// アドモブのクラス
        /// </summary>
        private AdmobLibrary _admobLibrary;


        private void Start()
        {
            LoadVolumes();
            SetVolumes();
        }

        private void LoadVolumes()
        {
            //Check if we have any saved volumes, since Master is the main one, we only have to check for it
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                //we do, so make sure our scriptableObject values match the saved values
                if (debugMode)
                    Debug.Log("Saved variables found, loading them...");
                volumeSettings.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                volumeSettings.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
                volumeSettings.effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
            }
            else
            {
                //we do not have any saved so set them to default values
                if (debugMode)
                    Debug.Log("No saved variables found, loading defaults...");
                volumeSettings.masterVolume = 0.20f;
                volumeSettings.musicVolume = 0.20f;
                volumeSettings.effectsVolume = 0.20f;
            }

            //Admobバナー作成
            _admobLibrary.RequestBanner(GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Bottom);
        }

        private void SetVolumes()
        {
            //change volumes to match the scriptable objects values and also the sliders so they appear in the correct place
            ChangeMasterVolume(volumeSettings.masterVolume);
            ChangeMusicVolume(volumeSettings.musicVolume);
            ChangeEffectsVolume(volumeSettings.effectsVolume);
            masterSlider.value = volumeSettings.masterVolume;
            musicSlider.value = volumeSettings.musicVolume;
            effectsSlider.value = volumeSettings.effectsVolume;
            if (debugMode)
                Debug.Log("Volume values and slider values have been set...");
        }

        public void ChangeMasterVolume(float value)
        {
            //change volume of master (also called during the OnChange inside the Slider)
            volumeSettings.masterVolume = value;
            masterSlider.value = volumeSettings.masterVolume;
            mixer.SetFloat("MasterVolume", Mathf.Log10(volumeSettings.masterVolume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volumeSettings.masterVolume);
            if (debugMode)
                Debug.LogFormat("Master Volume Saved: {0}", volumeSettings.masterVolume);
            PlayerPrefs.Save();
        }

        public void ChangeMusicVolume(float value)
        {
            //change volume of music (also called during the OnChange inside the Slider)
            volumeSettings.musicVolume = value;
            musicSlider.value = volumeSettings.musicVolume;
            mixer.SetFloat("MusicVolume", Mathf.Log10(volumeSettings.musicVolume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volumeSettings.musicVolume);
            if (debugMode)
                Debug.LogFormat("MusicVolume Saved: {0}", volumeSettings.musicVolume);
            PlayerPrefs.Save();
        }

        public void ChangeEffectsVolume(float value)
        {
            //change volume of effects (also called during the OnChange inside the Slider)
            volumeSettings.effectsVolume = value;
            effectsSlider.value = volumeSettings.effectsVolume;
            mixer.SetFloat("EffectsVolume", Mathf.Log10(volumeSettings.effectsVolume) * 20);
            PlayerPrefs.SetFloat("EffectsVolume", volumeSettings.effectsVolume);
            if (debugMode)
                Debug.LogFormat("Effects Volume Saved: {0}", volumeSettings.effectsVolume);
            PlayerPrefs.Save();
        }
    }
}