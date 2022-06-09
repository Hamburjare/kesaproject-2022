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

        ScoreData data = new ScoreData(scoreData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadScore()
    {
        if (File.Exists(scoreFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(scoreFilePath, FileMode.Open);
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
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
