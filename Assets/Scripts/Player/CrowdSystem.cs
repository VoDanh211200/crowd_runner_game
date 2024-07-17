using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    public float radius;
    public float angle;
    public Transform runnersParent;
    public GameObject runnersPrefab;
    public PlayAnimator playAnimator;

    void Update()
    {
        if (!GameManager.Instance.IsGameState()) return;
        PlaceRunners();
        if (runnersParent.childCount <= 0)
            GameManager.Instance.SetGameState(GameManager.GameState.Gameover);
    }

    public void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 child = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = child;
        }
    }

    private Vector3 GetRunnerLocalPosition(int i)
    {
        float x = radius * Mathf.Sqrt(i) * Mathf.Cos(Mathf.Deg2Rad * i * angle);
        float z = radius * Mathf.Sqrt(i) * Mathf.Sin(Mathf.Deg2Rad * i * angle);
        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    internal void ApplyBonus(int bonusAmount, BonusType bonusType)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;
            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnersToRemove);
                break;
        }
    }

    private void AddRunners(int bonusAmount)
    {
        for (int i = 0; i < bonusAmount; i++)
        {
            Instantiate(runnersPrefab, runnersParent);
        }
        if (GameManager.Instance.IsGameState()) playAnimator.Run();
    }

    private void RemoveRunners(int bonusAmount)
    {
        if (bonusAmount > runnersParent.childCount)
            bonusAmount = runnersParent.childCount;

        int runnersAmount = runnersParent.childCount;
        int i = runnersAmount - 1;
        int runnersToRemove = bonusAmount;

        while (runnersToRemove > 0 && i >= 0)
        {
            Transform runnerDestroy = runnersParent.GetChild(i);
            runnerDestroy.SetParent(null);
            Destroy(runnerDestroy.gameObject);
            i--;
            runnersToRemove--;
        }
    }
}
