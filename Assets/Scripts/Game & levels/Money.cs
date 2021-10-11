using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = PlayerStats.money.ToString();   
    }
}
