using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool isTarget;

    internal bool IsTarget()
    {
        return isTarget;
    }

    internal void SetTarget()
    {
        isTarget = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
