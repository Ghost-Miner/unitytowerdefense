using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startingMoney = 400;

    public static int lives;
    public int startingLives = 20;

    private void Start()
    {
        money = startingMoney;
        lives = startingLives;
    }
}
