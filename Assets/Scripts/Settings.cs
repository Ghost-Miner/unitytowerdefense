using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class Settings : MonoBehaviour
{
    //[SerializeField] private AudioMixer   audioMixer;         //Audio mixer
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private TMP_Dropdown qualityDropdown;    //Quality dropdown

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

    [SerializeField] private TMP_Text[] textsArray;
    [SerializeField] private Color textColor;

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
            SetQuality(0);

            SaveSettings();
            LoadSettimgs();
        }
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Debug.Log("Audiomanager: " + audioManager.gameObject.name);

        LoadSettimgs();

        /*for (int i = 0; i < textsArray.Length; i++)
        {
            textsArray[i].color = textColor;
        }*/
    }

    // Change volume
    #region Volume settings
    // Sound effects 
    public void ChangeVolumeSFX(bool _playSFX)
    {
        playSounds = _playSFX;
        playSfx = playSounds;

        audioManager.StopSounds();

        //Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void ChangeVolumeMusic(bool _playMusic)
    {
        playMusic = _playMusic;
        playMus = playMusic;

        audioManager.StopSounds();

        //Debug.Log("OPTIONS Music volume: " + Musicvolume);
    }

    // Sound effects 
    /*public void SetVolumeSFX(float SFXvolume)
    {
        //audioMixer.SetFloat("soundVol", Mathf.Log10(SFXvolume) * 20);
        //SFXVolumeSliderText.text = (SFXvolume*100).ToString("0") + "%";

        volSFX = SFXvolume;

        //Debug.Log("OPTIONS SFX volume: " + SFXvolume);
    }

    // Music
    public void SetVolumeMusic(float Musicvolume)
    {
        //audioMixer.SetFloat("musicVol", Mathf.Log10(Musicvolume) * 20);
        //MusicVolumeSliderText.text = (Musicvolume*100).ToString("0") + "%";

        volMusic = Musicvolume;

        //Debug.Log("OPTIONS Music volume: " + Musicvolume);
    }*/
    #endregion

    // Change quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        qualIndex = qualityIndex;

        //Debug.Log("OPTIONS Quality: " + qualityIndex);
    }

    public void ResetSettings ()
    {
        SettingsData settingsData = new SettingsData
        {
            Sound = true,
            Music = true,
            quality = 0
        };

        string json = JsonUtility.ToJson(settingsData);

        File.WriteAllText(Application.dataPath + "/Game data" + "/Settings.json", json);

        Debug.Log(json);

        LoadSettimgs();
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

        //Debug.Log("LOAD SFX Volume: " + loadSettings.Sound);
        //Debug.Log("LOAD Music Volume: " + loadSettings.Music);
        //Debug.Log("LOAD Quality: " + loadSettings.quality);
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
