using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float playerDistanceEndPosition = 100f;

    [SerializeField] private Transform mapStart;
    [SerializeField] private List<Transform> levelPrefabsList;

    [SerializeField] private GameObject player;

    private Vector3 lastEndPosition;
    private void Awake()
    {
        lastEndPosition = mapStart.Find("EndPosition").position;

        /* Generating 5 levels at the start of the game. */
        int startingLevelCount = 5;
        for (int i = 0; i < startingLevelCount; i++)
        {
            GenerateLevel();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < playerDistanceEndPosition)
        {
            GenerateLevel();
        }
    }

    private void GenerateLevel()
    {
        Transform randomLevelGenerator = levelPrefabsList[Random.Range(0, levelPrefabsList.Count)];
        Transform lastLevelEndTransform = GenerateLevel(randomLevelGenerator, lastEndPosition);
        lastEndPosition = lastLevelEndTransform.Find("EndPosition").position;
    }

    private Transform GenerateLevel(Transform levelToGenerate, Vector3 spawnPosition)
    {
        Transform levelTransform = Instantiate(levelToGenerate, spawnPosition, Quaternion.identity);
        return levelTransform;
    }

}
