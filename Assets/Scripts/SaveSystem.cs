using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string scoreFilePath = Application.persistentDataPath + "/score.keskikalja";

    public static void SaveScore(GameManager scoreData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(scoreFilePath, FileMode.Create);

        PlayerData data = new PlayerData(scoreData);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadScore()
    {
        if (File.Exists(scoreFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(scoreFilePath, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in" + scoreFilePath);
            return null;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public float highScore;

    public PlayerData(GameManager scoreData)
    {
        if (scoreData.score > highScore)
        {
            highScore = scoreData.score;
        }
    }
}

