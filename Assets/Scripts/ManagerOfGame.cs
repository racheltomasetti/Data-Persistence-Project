using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ManagerOfGame : MonoBehaviour
{
    public static ManagerOfGame Instance;
    public Text inputText;
    public int highScore;
    public string currentPlayer;
    public string recordHolder;
    public TMP_Text bestScore;
    public Text placeHolder;

    void Awake()
    {
        if(ManagerOfGame.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        LoadPreviousScore();

    }
    public void SaveScore(string name, int score)
    {
        SaveData data = new SaveData();
        data.highScore = score;
        data.playerName = name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadPreviousScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.highScore = data.highScore;
            this.recordHolder = data.playerName;
        }
        bestScore.text = $"Best Score : {recordHolder} : {highScore}";
        placeHolder.text = recordHolder;
    }
        [System.Serializable]
        public class SaveData
    {
        public int highScore;
        public string playerName;
    }
    public void ReadStringInput()
    {
        if (inputText != null)
        {
            currentPlayer = inputText.text;
        }
    }
}
