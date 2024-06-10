using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState { Menu, Game, LevelComplete, Gameover }
    public static Action<GameState> onGameStateChanged;

    private GameState gameState;

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
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        onGameStateChanged?.Invoke(gameState);
    }

    internal bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
