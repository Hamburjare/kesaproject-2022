using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* A static variable that is accessible from anywhere in the code. */
    public static GameManager Instance;

    public float score { get; private set; }

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
        score += Random.Range(250, 501) * Time.deltaTime;

        score = Mathf.Round(score);
        Debug.Log(score);
    }
}
