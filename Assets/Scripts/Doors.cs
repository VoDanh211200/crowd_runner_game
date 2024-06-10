using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType { Addition, Difference, Product, Division }

public class Doors : MonoBehaviour
{
    public SpriteRenderer rightDoorRender;
    public SpriteRenderer leftDoorRender;
    public TextMeshPro rightDoorText;
    public TextMeshPro leftDoorText;
    public int rightAmount;
    public int leftAmount;
    public Color bonusColor;
    public Color penaltyColor;
    public Collider collider;

    [SerializeField] private BonusType rightBonus;
    [SerializeField] private BonusType leftBonus;

    void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        switch (rightBonus)
        {
            case BonusType.Addition:
                rightDoorRender.color = bonusColor;
                rightDoorText.text = "+" + rightAmount;
                break;
            case BonusType.Difference:
                rightDoorRender.color = penaltyColor;
                rightDoorText.text = "-" + rightAmount;
                break;
            case BonusType.Product:
                rightDoorRender.color = bonusColor;
                rightDoorText.text = "x" + rightAmount;
                break;
            case BonusType.Division:
                rightDoorRender.color = penaltyColor;
                rightDoorText.text = "/" + rightAmount;
                break;
        }

        switch (leftBonus)
        {
            case BonusType.Addition:
                leftDoorRender.color = bonusColor;
                leftDoorText.text = "+" + leftAmount;
                break;
            case BonusType.Difference:
                leftDoorRender.color = penaltyColor;
                leftDoorText.text = "-" + leftAmount;
                break;
            case BonusType.Product:
                leftDoorRender.color = bonusColor;
                leftDoorText.text = "x" + leftAmount;
                break;
            case BonusType.Division:
                leftDoorRender.color = penaltyColor;
                leftDoorText.text = ":" + leftAmount;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal int GetBonusAmount(float x)
    {
        return x > 0 ? rightAmount : leftAmount;
    }

    internal BonusType DetBonusType(float x)
    {
        return x > 0 ? rightBonus : leftBonus;
    }

    internal void Disable()
    {
        collider.enabled = false;
    }
}
