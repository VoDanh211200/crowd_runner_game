using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimator : MonoBehaviour
{

    public Transform runnersParent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform child = runnersParent.GetChild(i);
            Animator animator = child.GetComponent<Animator>();
            animator.Play("Idle");
        }
    }

    internal void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform child = runnersParent.GetChild(i);
            Animator animator = child.GetComponent<Animator>();
            animator.Play("Fast Run");
        }
    }
}
