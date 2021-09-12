using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;

    private Text nameText;
    public Text highScoreText;

    public string myName;

    public string highScoreName;
    public int highScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadGame();
    }

    private void Start()
    {
        Text textComponent = GameObject.Find("EnterName").GetComponent<InputField>().textComponent;

        if (textComponent != null)
            nameText = textComponent;
        else
            return;

        highScoreText.text = "Highscore: " + highScoreName + ": " + highScore;

    }

    public void StartGame()
    {
        if (nameText.text == "")
        {
            myName = "Player";
        }
        else
        {
            myName = nameText.text;
        }

        SceneManager.LoadScene(1);
    }



    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.highScore = highScore;
        data.playerName = myName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.playerName;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
