using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector3 size;
    public string getLevelSO;
    public Doors doors;
    public EnemyGroup enemy;
    
    public float GetLength()
    {
        return size.z;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
