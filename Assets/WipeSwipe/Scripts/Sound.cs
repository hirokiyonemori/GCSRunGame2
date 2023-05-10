using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    //Deals with every sound clip assigned in the sounds array in the audio manager script
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
    
}
