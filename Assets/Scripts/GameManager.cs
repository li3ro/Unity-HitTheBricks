using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text highScoreText;
    public int highScore;
    public string playerName;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load values from disk (safe anytime)
        LoadHighScore();

        // Re-bind UI when any scene finishes loading
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Try to bind for the current active scene as well
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var go = GameObject.Find("High Score Text");
        if (go != null)
        {
            highScoreText = go.GetComponent<TMP_Text>();
            UpdateHighScoreText();
        }
        else
        {
            Debug.Log($"GameManager: 'High Score Text' not found in scene '{scene.name}' (is it active and named exactly?)");
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string name;
    }

    public void SaveHighScore(int score)
    {
        if (score <= highScore)
            return;

        highScore = score;

        SaveData data = new SaveData
        {
            name = this.playerName,
            highScore = score
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        UpdateHighScoreText();
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.highScore = data.highScore;
            this.playerName = data.name;
        }

        // UI may not exist yet; UpdateHighScoreText will no-op if highScoreText is null
        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText == null)
        {
            Debug.Log("GameManager: highScoreText is null");
            return;
        }

        highScoreText.text = "Best Score: " + playerName + " : " + this.highScore;
    }
}
