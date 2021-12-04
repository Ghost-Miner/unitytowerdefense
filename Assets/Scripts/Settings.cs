﻿using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer   audioMixer;             //Audio mixer

    [SerializeField] private TMP_Dropdown qualityDropdown;        //Quality dropdown

    [SerializeField] private Slider   MusicVolumeSlider;      //Music Volume slider
    [SerializeField] private Slider   SFXVolumeSlider;        //SFX Volume slider

    [SerializeField] private Toggle SFXToggle;
    [SerializeField] private Toggle MusicToggle;

    private bool playMusic;
    private bool playSounds;

    // Variables used to save settings into a file
    private float   volMaster;     // Master Volume
    private float   volSFX;        // Sound effects Volume
    private float   volMusic;      // Music Volume
    private int     qualIndex;     // Quality index
    private bool    playMus;
    private bool    playSfx;

    private void OnDisable()
    {
        SaveSettings();
    }

    private void Awake()
    {
        var settingsFile = File.Exists(Application.dataPath + "/Game data" + "/Settings.json");
        if (!settingsFile)
        {
            Debug.Log("settingsFile sfile not fond");
            SetVolumeMusic(1.0f);
            SetVolumeSFX(1.0f);
            SetQuality(0);

            //SaveSettings();
            LoadSettimgs();
        }
    }

    void Start()
    {
        LoadSettimgs();
        Debug.Log("start");
    }

    // Change volume
    #region Volume settings
    // Sound effects 
    public void ChangeVolumeSFX(bool SFXvolume)
    {
        //audioMixer.SetFloat("soundVol", Mathf.Log10(SFXvolume) * 20);
        //SFXVolumeSliderText.text = (SFXvolume*100).ToString("0") + "%";

        //volSFX = SFXvolume;

        playSounds = SFXvolume;
        playSfx = playSounds;

        Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void ChangeVolumeMusic(bool Musicvolume)
    {
        //audioMixer.SetFloat("musicVol", Mathf.Log10(Musicvolume) * 20);
        //MusicVolumeSliderText.text = (Musicvolume*100).ToString("0") + "%";

        //volMusic = Musicvolume;

        playMusic = Musicvolume;
        playMus = playMusic;

        Debug.Log("OPTIONS Music volume: " + Musicvolume);
    }

    // Sound effects 
    public void SetVolumeSFX(float SFXvolume)
    {
        //audioMixer.SetFloat("soundVol", Mathf.Log10(SFXvolume) * 20);
        //SFXVolumeSliderText.text = (SFXvolume*100).ToString("0") + "%";

        volSFX = SFXvolume;

        Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void SetVolumeMusic(float Musicvolume)
    {
        audioMixer.SetFloat("musicVol", Mathf.Log10(Musicvolume) * 20);
        //MusicVolumeSliderText.text = (Musicvolume*100).ToString("0") + "%";

        volMusic = Musicvolume;

        Debug.Log("OPTIONS Music volume: " + Musicvolume);
    }
    #endregion

    // Change quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualIndex = qualityIndex;

        Debug.Log("OPTIONS Quality: " + qualityIndex);
    }


    // Save settings function
    #region Save and load settings function
    public void SaveSettings()
    {
        SettingsData settingsData = new SettingsData
        {
            SFXvolume = volSFX,
            Musicvolume = volMusic,
            quality = qualIndex,
            Sound = playSfx,
            Music = playMus
        };

        string json = JsonUtility.ToJson(settingsData);

        File.WriteAllText(Application.dataPath + "/Game data" + "/Settings.json", json);

        Debug.Log(json);
    }

    // Load settings function
    public void LoadSettimgs()
    {
        string json = File.ReadAllText(Application.dataPath + "/Game data" + "/Settings.json");

        SettingsData loadSettings = JsonUtility.FromJson<SettingsData>(json);
        //SetVolumeSFX(loadSettings.SFXvolume);
        //SetVolumeMusic(loadSettings.Musicvolume);

        ChangeVolumeMusic(loadSettings.Music);
        ChangeVolumeSFX(loadSettings.Sound);

        SFXVolumeSlider.value = loadSettings.SFXvolume;
        //SFXVolumeSliderText.text = (loadSettings.SFXvolume * 100).ToString("0") + "%";

        MusicVolumeSlider.value = loadSettings.Musicvolume;
        //'MusicVolumeSliderText.text = (loadSettings.Musicvolume * 100).ToString("0") + "%";

        SetQuality(loadSettings.quality);
        qualityDropdown.value = loadSettings.quality;

        SFXToggle.isOn = loadSettings.Sound;
        MusicToggle.isOn = loadSettings.Music;

        Debug.Log("LOAD SFX Volume: " + loadSettings.Sound);
        Debug.Log("LOAD Music Volume: " + loadSettings.Music);
        Debug.Log("LOAD Quality: " + loadSettings.quality);
    }

    // List of settings values
    public class SettingsData
    {
        public float SFXvolume;
        public float Musicvolume;
        public bool Sound;
        public bool Music;

        public int quality;
    }
    #endregion
}
