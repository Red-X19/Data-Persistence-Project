using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InformationHandler : MonoBehaviour
{
    public static InformationHandler Instance;
    
    public string playerName;
    public int highScore;
    public string championName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log(playerName);
    }


    [System.Serializable]
    class SaveData
    {
        public int HighScore;

        public string HighScoreName;
    }

    public void SaveHighScore(int score, string name)
    {
        SaveData data = new SaveData();
        data.HighScore = score;
        data.HighScoreName = name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.HighScore;
            championName = data.HighScoreName;
        }
        else
        {
            highScore = 0;
            championName = "";
        }
    }

}
