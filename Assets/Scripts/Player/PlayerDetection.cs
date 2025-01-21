
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{

    public CrowdSystem crowdSystem;
    public static Action onDoorHit;
    public GameObject wheel;

    void Update()
    {
        if (GameManager.Instance.IsGameState())
            DetectColliders();
    }

    private void DetectColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);
                doors.Disable();
                onDoorHit?.Invoke();
                crowdSystem.ApplyBonus(bonusAmount, bonusType);
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Finish"))
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
            else if (collider.CompareTag("Coin"))
            {
                SoundManager.Instance.PlayDoorHitSound();
                Destroy(collider.gameObject);
                DataManager.Instance.AddCoins(1);
            }
            else if (collider.CompareTag("Gift"))
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                wheel.SetActive(true);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
            else if (collider.CompareTag("Abyss") || collider.CompareTag("Cactus"))
            {
                crowdSystem.ApplyBonus(1, BonusType.Difference);
            }
        }
    }
}
