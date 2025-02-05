using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompeletePanel;
    public GameObject settingsPanel;
    public GameObject shopPanel;
    public ShopManager shopManager;
    public Slider slider;
    public Text levelText;

    void Start()
    {
        slider.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
        levelText.text = "Level " + ChunkManager.Instance.GetLevel();
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Gameover) ShowGameover();
        else if (state == GameManager.GameState.LevelComplete) ShowGameCompelete();
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState()) return;
        float progress = PlayerController.Instance.transform.position.z / ChunkManager.Instance.GetFinishZ();
        slider.value = progress;
    }

    public void RetryButtonReleased()
    {
        InterstitialAd.Instance.ShowAd();
        SceneManager.LoadScene(0);
    }

    public void ShowGameover()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowGameCompelete()
    {
        gamePanel.SetActive(false);
        levelCompeletePanel.SetActive(true);
    }

    public void ShowSettingPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }
}
