using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public Button thisButton;
    public Image skinImage;
    public GameObject lockImage;
    public GameObject selectorImage;

    private bool unlocked;

    public void Configure(Sprite sprite, bool unlocked)
    {
        skinImage.sprite = sprite;
        this.unlocked = unlocked;

        if (unlocked)
        {
            Unlock();
        }
        else
        {
            Lock() ;
        }
    }

    private void Lock()
    {
        thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void Unlock()
    {
        thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);
        unlocked = true;
    }

    public void Select()
    {
        selectorImage.SetActive(true);
    }

    public void Deselect()
    {
        selectorImage.SetActive(false);
    }

    public bool IsUnLocked()
    {
        return unlocked;
    }

    internal Button GetButton()
    {
         return thisButton;
    }
}
