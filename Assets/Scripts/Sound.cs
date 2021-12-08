using UnityEngine;
using UnityEngine.Audio;

//List of sounds
[System.Serializable]
public class Sound
{
    public string name;         //Sound name
    
    public AudioClip clip;      //Sound file

    [Range(0f,1f)]
    public float volume = 1f;   //Volume
    [Range(0.1f, 3f)]
    public float pitch = 1f;    //Pitch

    public bool loop = false;           //Loop

    public bool Music = false;

    [HideInInspector]
    public AudioSource source;  //Sound source
}
