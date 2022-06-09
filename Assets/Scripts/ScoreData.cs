using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public float highScore;

    public ScoreData(GameManager scoreData)
    {
        if (scoreData.score > highScore)
        {
            highScore = scoreData.score;
        }
    }
}
