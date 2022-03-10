using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public Text userName;
    public Text bestScore;
    public int highScore;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (bestScore != null){
            highScore = LoadHighScore();
            string highScoreUser = GetUsername();

            string bestsource = $"Best Score: {highScore} by {highScoreUser}";
            bestScore.text = bestsource;
        }
        else
        {
            LoadHighScore();
        }

    }

    public void GetUserName(Text name)
    {
        userName = name;
        Debug.Log(userName.text);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    [System.Serializable]
    class SaveData
    {
        public string username;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData savedata = new SaveData();
        savedata.highScore = highScore;
        savedata.username = userName.text.ToString();

        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
        print($"This is the current useranme: {userName}");
        print(json);

    }

    public int LoadHighScore()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;

            return highScore;
        }

        return 0;
    }

    public string GetUsername()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.username.ToString();
        }

        return null;
    }


}
