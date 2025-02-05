using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Sprite[] skins;
    public SkinButton[] skinsButtons;
    public int skinPrice;
    public TMP_Text priceText;
    public Button purchaseButton;

    private void Awake()
    {
        priceText.text = skinPrice.ToString();
    }

    private void Start()
    {
        RewardedAdsButton.onRewardedAdRewared += RewardPlayer;
        ConfigureButtons();
        UpdatePurchaseButton();
    }

    private void OnDestroy()
    {
        RewardedAdsButton.onRewardedAdRewared -= RewardPlayer;
    }

    private void RewardPlayer()
    {
        DataManager.Instance.AddCoins(10);
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinsButtons.Length; i++)
        {
            bool unclocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinsButtons[i].Configure(skins[i], unclocked);
            int skinIndex = i;
            skinsButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinsButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < skinsButtons.Length; i++)
        {
            if (skinIndex == i)
            {
                skinsButtons[i].Select();
            }
            else
            {
                skinsButtons[i].Deselect();
            }
        }
    }

    public void PurchaseSkin()
    {
        List<SkinButton> buttons = new List<SkinButton>();
        for (int i = 0; i < skinsButtons.Length; i++)
            if (!skinsButtons[i].IsUnLocked())
                buttons.Add(skinsButtons[i]);
        if (buttons.Count <= 0)
            return;
        SkinButton random = buttons[UnityEngine.Random.Range(0, buttons.Count)];
        UnlockSkin(random);
        SelectSkin(random.transform.GetSiblingIndex());
        DataManager.Instance.UseCoins(skinPrice);
        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        purchaseButton.interactable = DataManager.Instance.GetCoins() >= skinPrice;
    }
}
