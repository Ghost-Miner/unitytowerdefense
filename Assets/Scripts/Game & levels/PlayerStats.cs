using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int lives;

    [SerializeField] private int startingMoney = 400;
    [SerializeField] private int startingLives = 20;

    private void Start()
    {
        money = startingMoney;
        lives = startingLives;
    }
}
