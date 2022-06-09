using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* A static variable that is accessible from anywhere in the code. */
    public static GameManager Instance;

    public float score { get; private set; }

    float lastSpeedChangeScore;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        score += Random.Range(10, 151) * Time.deltaTime;
        score = Mathf.Round(score);


        /* This is checking if the score is divisible by 150 and if it is not the same as the last time
        the speed was changed. If it is, then the speed is increased by a random amount between 0.01
        and 0.5. */
        if (score % 150 == 0 && score != lastSpeedChangeScore)
        {
            PlayerController.Instance.moveSpeed += Random.Range(0.01f, 0.5f);
            lastSpeedChangeScore = score;
        }


        // Debug.Log(score);
    }

    public void SaveScore()
    {
        SaveSystem.SaveScore(this);
    }

    public void LoadScore()
    {
        ScoreData data = SaveSystem.LoadScore();

        score = data.highScore;
    }
}
