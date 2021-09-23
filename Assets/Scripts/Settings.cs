using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class Settings : MonoBehaviour
{
    public AudioMixer   audioMixer;             //Audio mixer

    public TMP_Dropdown qualityDropdown;        //Quality dropdown

    public Slider       MusicVolumeSlider;      //Music Volume slider
    // public TMP_Text     MusicVolumeSliderText;  //Music Volume slider text displaying volume level - Located in Volume slider >> Handle slide area >> Handle

    public Slider       SFXVolumeSlider;        //SFX Volume slider
    // public TMP_Text     SFXVolumeSliderText;    //SFX Volume slider text displaying volume level - Located in Volume slider >> Handle slide area >> Handle

    // Variables used to save settings into a file
    private float   volMaster;     // Master Volume
    private float   volSFX;        // Sound effects Volume
    private float   volMusic;      // Music Volume
    private int     qualIndex;     // Quality index

    private void OnDisable()
    {
        SaveSettings();
    }

    void Start()
    {
        LoadSettimgs();
    }

    void Update()
    {
        
    }

    // Change volume
    #region Volume settings
    // Sound effects
    public void SetVolumeSFX(float SFXvolume)
    {
        audioMixer.SetFloat("Sound volume", SFXvolume);
        //SFXVolumeSliderText.text = SFXvolume.ToString(); // Make it display values between 0% and 100% instead of the Audio mixer -80 to 0 values.

        volSFX = SFXvolume;

        Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void SetVolumeMusic(float Musicvolume)
    {
        audioMixer.SetFloat("Music volume", Musicvolume);
        //MusicVolumeSliderText.text = Musicvolume.ToString(); // Make it display values between 0% and 100% instead of the Audio mixer -80 to 0 values.

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
        SetVolumeSFX(loadSettings.SFXvolume);
        SetVolumeMusic(loadSettings.Musicvolume);

        SFXVolumeSlider.value = loadSettings.SFXvolume;
        //SFXVolumeSliderText.text = loadSettings.SFXvolume.ToString();

        MusicVolumeSlider.value = loadSettings.Musicvolume;
        //MusicVolumeSliderText.text = loadSettings.Musicvolume.ToString();

        SetQuality(loadSettings.quality);
        qualityDropdown.value = loadSettings.quality;

        Debug.Log("LOAD SFX Volume: " + loadSettings.SFXvolume);
        Debug.Log("LOAD Music Volume: " + loadSettings.Musicvolume);
        Debug.Log("LOAD Quality: " + loadSettings.quality);
    }

    // List of settings values
    public class SettingsData
    {
        public float SFXvolume;
        public float Musicvolume;

        public int quality;
    }
    #endregion
}
