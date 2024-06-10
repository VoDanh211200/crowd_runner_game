using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public TMP_Text[] coinsText;

    private int coins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void Start()
    {
        UpdateCoinsTexts();
    }

    private void UpdateCoinsTexts()
    {
        foreach (var coin in coinsText)
        {
            coin.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }

    internal int GetCoins()
    {
        return coins;
    }

    internal void UseCoins(int skinPrice)
    {
        coins -= skinPrice;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }
}
