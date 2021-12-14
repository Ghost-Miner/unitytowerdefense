using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class LvSelectButton : MonoBehaviour
{
    private Button button;

    private bool   levelFinished;
    private bool   levelUnlocked;
    private string dataFolder;
    private string saveFile;

    [SerializeField] private int levelNumber;

    void Start()
    {
        button = GetComponent<Button>();

        dataFolder = Application.dataPath + "/Game data" + "/";
        saveFile   = "Level_" + levelNumber + ".json";

        ReadJson();
    }

    private void ReadJson()
    {
        if (levelNumber == 1)
        {
            button.interactable = true;
            return;
        }

        if (!File.Exists(dataFolder + saveFile))
        {
            button.interactable = false;
            Debug.Log("File " + saveFile + " doesnt exist");
         
            return;
        }

        string json = File.ReadAllText(dataFolder + saveFile);

        LevelData levelData = JsonUtility.FromJson<LevelData>(json);
        Debug.Log(gameObject.name + ": " + json);

        levelFinished = levelData.finished;
        levelUnlocked = levelData.unlocked;

        if (!levelUnlocked)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void WriteJson ()
    {
        LevelData levelData = new LevelData
        {
            unlocked = false,
            finished = false,
        };

        string json = JsonUtility.ToJson(levelData);

        File.WriteAllText(dataFolder + saveFile, json);
    }

    // List of settings values
    public class LevelData
    {
        public bool unlocked;
        public bool finished;
    }
}
