using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private bool isHaptics;

    void Start()
    {
        PlayerDetection.onDoorHit += Vibrate;
        Enemy.onRunnerDie += Vibrate;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.LevelComplete)
        {
            Vibrate();
        }
        else if (state == GameManager.GameState.Gameover)
        {
            Vibrate();
        }
    }

    private void Vibrate()
    {
        if (isHaptics)
            Taptic.Light();
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorHit -= Vibrate;
        Enemy.onRunnerDie -= Vibrate;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    internal void DisableVibrations()
    {
        isHaptics = false;
    }

    internal void EnableVibrations()
    {
        isHaptics = true;
    }
}
