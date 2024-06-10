using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;
    public LevelSO[] levels;

    private GameObject finishLine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateLevel();
        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel() - 1;
        LevelSO level = levels[currentLevel];
        CreateLevel(level.chunks);
    }

    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = new(0, -5, 0);
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];
            if (i > 0) chunkPosition.z += chunkToCreate.GetLength() / 2;
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0) + 1;
    }
}
