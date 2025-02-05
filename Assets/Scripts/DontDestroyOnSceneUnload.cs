using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnSceneUnload : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
