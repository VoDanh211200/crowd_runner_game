using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimator : MonoBehaviour
{

    public Transform runnersParent;

    internal void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform child = runnersParent.GetChild(i);
            Animator animator = child.GetComponent<Animator>();
            animator.Play("Fast Run");
        }
    }

    internal void Dancing()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform child = runnersParent.GetChild(i);
            Animator animator = child.GetComponent<Animator>();
            animator.Play("Hip Hop Dancing");
        }
    }
}
