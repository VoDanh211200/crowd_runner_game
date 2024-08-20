using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform enemiesParent;
    public int amount;
    public float radius;
    public float angle;

    void Start()
    {
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 child = GetRunnerLocalPosition(i);
            Vector3 enemyWorldPosition = transform.TransformPoint(child);
            Instantiate(enemyPrefab, enemyWorldPosition, enemyPrefab.transform.rotation, enemiesParent);
        }
    }

    private Vector3 GetRunnerLocalPosition(int i)
    {
        float x = radius * Mathf.Sqrt(i) * Mathf.Cos(Mathf.Deg2Rad * i * angle);
        float z = radius * Mathf.Sqrt(i) * Mathf.Sin(Mathf.Deg2Rad * i * angle);
        return new Vector3(x, 0, z);
    }
}
