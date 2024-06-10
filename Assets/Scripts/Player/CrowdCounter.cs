using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    public TextMeshPro crownCounterText;
    public Transform runnersParent;

    void Update()
    {
        crownCounterText.text = runnersParent.childCount.ToString();
        if (runnersParent.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
