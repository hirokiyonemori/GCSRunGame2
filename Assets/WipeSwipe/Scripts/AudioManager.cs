using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    //This class Handles all the Sounds in the games, we can play any audio just by calling play method of this class and Passing the name of sound
    public Sound[] sounds;

    public static AudioManager Instance;

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //only make one instance of this class
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //if there are more than 1 instances destroy this one
            Destroy(gameObject);
            return;
        }
     
        foreach(Sound s in sounds)
        {
            //Storing each sound in the aray, assiging its clip, setting its volume pitch and loop status
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        //Play background music on the start of scene
       Play("background");
     
    }

    //Play the sound and pass it's name
    public void Play(string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found.");
            return;
        }
       
        s.source.Play(); 
    } 
    //Stop/Pause the sound and pass the name of sound that you wan't to stop
    public void Stop(string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found.");
            return;
        }
        
        s.source.Pause();
   
    }

}
