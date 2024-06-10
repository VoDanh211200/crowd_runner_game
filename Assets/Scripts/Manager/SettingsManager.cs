using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Sprite optionsOnSprite;
    public Sprite optionsOffSprite;
    public Image soundsButtonImage;
    public Image hapticsButtonImage;
    public SoundManager soundManager;
    public VibrationManager vibrationManager;

    private bool soundsSate = true;
    private bool hapticsSate = true;

    private void Awake()
    {
        soundsSate = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsSate = PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (soundsSate)
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }

        if (hapticsSate)
        {
            EnableHaptics();
        }
        else
        {
            DisableHaptics();
        }
    }

    private void DisableHaptics()
    {
        vibrationManager.DisableVibrations();
        hapticsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableHaptics()
    {
        vibrationManager.EnableVibrations();
        hapticsButtonImage.sprite = optionsOnSprite;
    }

    public void ChangeeHapticsState()
    {
        if (hapticsSate)
        {
            DisableHaptics();
        }
        else
        {
            EnableHaptics();
        }
        hapticsSate = !hapticsSate;
        PlayerPrefs.SetInt("haptics", hapticsSate ? 1 : 0);
    }

    public void ChangeSoundState()
    {
        if (soundsSate)
        {
            DisableSounds();
        }
        else
        {
            EnableSounds();
        }
        soundsSate = !soundsSate;
        PlayerPrefs.SetInt("sounds", soundsSate ? 1 : 0);
    }

    private void EnableSounds()
    {
        soundManager.EnableSound();
        soundsButtonImage.sprite = optionsOnSprite;
    }

    private void DisableSounds()
    {
        soundManager.DisableSound();
        soundsButtonImage.sprite = optionsOffSprite;
    }
}
