using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static bool Lose;
    public static bool Wictory;

    void Start()
    {
        Lose = false;
        Wictory = false;
        Money = startMoney;
    }

}
