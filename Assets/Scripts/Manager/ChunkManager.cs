using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;
    public LevelSO[] levels;
    public CrowdSystem crowdSystem;

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
        finishLine = GameObject.FindWithTag("Finish") != null ? 
            GameObject.FindWithTag("Finish") : GameObject.FindWithTag("Gift");
    }

    private void GenerateLevel()
    {
        //Generate Chunks
        int currentLevel = GetLevel() - 1;
        currentLevel = 2;
        CreateLevel(levels[currentLevel]);
        //Generate Players
        crowdSystem.ApplyBonus((currentLevel + 1) * 2, BonusType.Addition);
        crowdSystem.PlaceRunners();
    }

    private void CreateLevel(LevelSO level)
    {
        Vector3 chunkPosition = new(0, 5, 0);
        for (int i = 0; i < level.chunks.Length; i++)
        {
            //set amount enemy
            if (level.chunks[i].enemy != null)
            {
                level.chunks[i].enemy.amount = int.Parse(level.setup[i]);
            }

            //set caculator door
            if (level.chunks[i].doors != null)
            {
                string[] doorSetup = level.setup[i].Split(' ');

                string rightDoorSetup = doorSetup[0];
                ParseDoorSetup(rightDoorSetup, out level.chunks[i].doors.rightBonus, out level.chunks[i].doors.rightAmount);

                string leftDoorSetup = doorSetup[1];
                ParseDoorSetup(leftDoorSetup, out level.chunks[i].doors.leftBonus, out level.chunks[i].doors.leftAmount);
            }

            //generate chunk 
            Chunk chunkToCreate = level.chunks[i];
            if (i > 0) chunkPosition.z += chunkToCreate.GetLength() / 2;
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    private void ParseDoorSetup(string setup, out BonusType bonusType, out int amount)
    {
        char operatorChar = setup[0];
        string amountString = setup.Substring(1);

        switch (operatorChar)
        {
            case '+':
                bonusType = BonusType.Addition;
                break;
            case '-':
                bonusType = BonusType.Difference;
                break;
            case 'x':
                bonusType = BonusType.Product;
                break;
            case '/':
                bonusType = BonusType.Division;
                break;
            default:
                bonusType = BonusType.Addition;
                Debug.LogWarning($"Unknown operator '{operatorChar}', defaulting to Addition");
                break;
        }

        if (!int.TryParse(amountString, out amount))
        {
            Debug.LogError($"Failed to parse amount from '{amountString}'");
            amount = 0;
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
