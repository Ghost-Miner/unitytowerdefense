using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;               //List of sounds

    public static AudioManager instance; //Audioanager reference

    //public AudioMixer audioMixer;

    public static bool playMusic;
    public static bool playSound;

    void Awake()
    {
        //Check if Audio manager already exists 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        //Get list of sounds
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        LoadSettimgs();

        Play("kevin_breaktime"); //Play music
    }

    //Play the sound
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //Display warning if sound was not found
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
         
        if (s.Music && playMusic)
        {
            s.source.Play();
        }
        else if (!s.Music && playSound)
        {
            s.source.Play();
        }
    }

    public void StopSounds ()
    {
        LoadSettimgs();

        foreach (Sound snd in sounds)
        {
            if (snd.Music && !playMusic)
            {
                snd.source.Stop();
                //Debug.Log("AM: Stopping music" + snd.name);
            }
            else if (!snd.Music && !playSound)
            {
                snd.source.Stop();
                //Debug.Log("AM: Stopping sfx" + snd.name);
            }
        }
    }

    // Load settings function
    public void LoadSettimgs()
    {
        string json = File.ReadAllText(Application.dataPath + "/Game data" + "/Settings.json");

        SettingsData loadSettings = JsonUtility.FromJson<SettingsData>(json);

        playMusic = loadSettings.Music;
        playSound = loadSettings.Sound;
    }

    // List of settings values
    public class SettingsData
    {
        public bool Sound;
        public bool Music;
    }
}
