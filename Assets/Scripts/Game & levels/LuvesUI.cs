using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuvesUI : MonoBehaviour
{
    public TMP_Text livesText;

    
    void Update()
    {
        livesText.text = PlayerStats.lives.ToString();
    }
}
