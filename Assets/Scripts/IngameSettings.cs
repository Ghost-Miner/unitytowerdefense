using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class IngameSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;    //Quality dropdown

    [SerializeField] private Toggle SFXToggle;
    [SerializeField] private Toggle MusicToggle;

    [SerializeField] private GameObject settingPanel;

    private bool playMusic;
    private bool playSounds;

    // Variables used to save settings into a file
    private int  qualIndex;     // Quality index
    private bool playMus;
    private bool playSfx;

    private void OnEnable()
    {
        LoadSettimgs();
    }

    public void CloseAndSave()
    {
        SaveSettings();

        settingPanel.SetActive(false);
    }

    public void Cancel ()
    {
        settingPanel.SetActive(false);
    }

    #region Volume settings
    // Sound effects 
    public void ChangeVolumeSFX(bool _playSFX)
    {
        playSounds = _playSFX;
        playSfx = playSounds;

        //Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void ChangeVolumeMusic(bool _playMusic)
    {
        playMusic = _playMusic;
        playMus = playMusic;

        //Debug.Log("OPTIONS Music volume: " + Musicvolume);
    }
    #endregion

    // Change quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualIndex = qualityIndex;

        //Debug.Log("OPTIONS Quality: " + qualityIndex);
    }

    // Save settings function
    #region Save and load settings function
    public void SaveSettings()
    {
        SettingsData settingsData = new SettingsData
        {
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

        ChangeVolumeMusic(loadSettings.Music);
        ChangeVolumeSFX(loadSettings.Sound);

        SetQuality(loadSettings.quality);
        qualityDropdown.value = loadSettings.quality;

        SFXToggle.isOn = loadSettings.Sound;
        MusicToggle.isOn = loadSettings.Music;
    }

    // List of settings values
    public class SettingsData
    {
        public bool Sound;
        public bool Music;

        public int quality;
    }
    #endregion
}
