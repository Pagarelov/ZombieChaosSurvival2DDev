using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;

    public GameObject deathScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighscoreText;

    public SaveData data;
    

    public int score;

    private void Awake()
    {
        manager = this;
        SaveSystem.Initialize();
        
        data = new SaveData(0);
    }

    public IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        deathScreen.SetActive(true);

        scoreText.text = "Score: " + score.ToString();

        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if (data.highscore < score)
        {
            data.highscore = score;
        }
        HighscoreText.text = "Highscore: " + data.highscore.ToString();

        string saveData = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveData);
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}

[System.Serializable]
public class SaveData
{
    public int highscore;

    public SaveData(int _hs)
    {
        highscore = _hs;
    }
}
