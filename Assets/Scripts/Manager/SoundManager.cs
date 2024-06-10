using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource doorHitSound;
    public AudioSource runnerDieSound;
    public AudioSource levelCompleteSound;
    public AudioSource gameOverSound;
    public AudioSource buttonSound;

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
        PlayerDetection.onDoorHit += PlayDoorHitSound;
        Enemy.onRunnerDie += PlayRunnerDieSound;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.LevelComplete)
        {
            levelCompleteSound.Play();
        }
        else if (state == GameManager.GameState.Gameover)
        {
            gameOverSound.Play();
        }
    }

    public void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    internal void EnableSound()
    {
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
        buttonSound.volume = 1;
    }

    internal void DisableSound()
    {
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
        buttonSound.volume = 0;
    }
}
