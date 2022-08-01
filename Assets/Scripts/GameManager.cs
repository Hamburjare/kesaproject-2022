using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    /* A static variable that is accessible from anywhere in the code. */
    public static GameManager Instance;

    public float score { get; private set; }

    public ulong money { get; private set; }

    public ulong diamonds { get; private set; }

    float lastSpeedChangeScore;

    public float highScore;

    public bool gameStarted;

    [SerializeField]
    double _infectionSpeed;

    public double infectionSpeed
    {
        get
        {
            return _infectionSpeed;
        }

        set
        {
            if (value >= 1.0)
            {
                _infectionSpeed = value;
            }
            else _infectionSpeed = 1.0f;
        }
    }

    [SerializeField]
    double _playerSpeed;

    public double playerSpeed
    {
        get
        {
            return _playerSpeed;
        }

        set
        {
            if (value >= 1.0)
            {
                _playerSpeed = value;
            }
            else _infectionSpeed = 1.0f;
        }
    }

    [SerializeField]
    double _jumpForce;

    public double jumpForce
    {
        get
        {
            return _jumpForce;
        }

        set
        {
            if (value >= 1.0)
            {
                _jumpForce = value;
            }
            else _infectionSpeed = 1.0f;
        }
    }

    public int selectedSkinIndex;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);

        gameStarted = false;

        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            score += Random.Range(10, 151) * Time.deltaTime;
            score = Mathf.Round(score);
            Debug.Log(score);
        }

        /* This is checking if the score is divisible by 150 and if it is not the same as the last time
        the speed was changed. If it is, then the speed is increased by a random amount between 0.01
        and 0.5. */
        if (score % 150 == 0 && score != lastSpeedChangeScore)
        {
            PlayerController.Instance.moveSpeed += Random.Range(0.01f, 0.5f);
            lastSpeedChangeScore = score;
        }


        if (Input.GetKey(KeyCode.E))
        {
            SavePlayerData(money, score, diamonds);
        }

    }

    public void SetMoney(char m, ulong p)
    {
        if (m == '-')
        {
            if (money >= p)
            {
                money -= p;
            }
        }
        else if (m == '+')
        {
            money += p;
        }
        else
        {
            Debug.LogError($"{m} is not a valid");
        }

        SavePlayerData(money, score, diamonds);
    }
    public void SetDiamonds(char m, ulong p)
    {
        if (m == '-')
        {
            if (diamonds >= p)
            {
                diamonds -= p;
            }
        }
        else if (m == '+')
        {
            diamonds += p;
        }
        else
        {
            Debug.LogError($"{m} is not a valid");
        }

        SavePlayerData(money, score, diamonds);
    }


    [System.Serializable]
    class SavePlayer
    {
        public ulong money;
        public float highScore;
        public ulong diamonds;
    }

    public void SavePlayerData(ulong money, float score, ulong diamonds)
    {
        SavePlayer data = new SavePlayer();
        data.highScore = highScore;
        if (score > highScore) data.highScore = score;
        data.money = money;
        data.diamonds = diamonds;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(Application.persistentDataPath + "/player.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePlayer data = JsonUtility.FromJson<SavePlayer>(json);

            highScore = data.highScore;
            money = data.money;
            diamonds = data.diamonds;
        }
        else
        {
            string json = "{\"money\":0,\"highScore\":0.0,\"diamonds\":0}";
            File.WriteAllText(path, json);
        }
    }

}
