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
    public TextMeshProUGUI SurvivalTimeText;

    public SaveData data;

    public float survivalTime;
    private float currentTime;

    public int score;

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void Awake()
    {
        manager = this;
        SaveSystem.Initialize();
        
        data = new SaveData(0);
        currentTime = 0f;
    }

    public IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        deathScreen.SetActive(true);

        scoreText.text = "Счет: " + score.ToString();

        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if (data.highscore < score)
        {
            data.highscore = score;
        }
        HighscoreText.text = "Рекорд: " + data.highscore.ToString();

        string formattedTime = string.Format("{1:00}", Mathf.Floor(currentTime / 60), currentTime % 60); //{0:0}
        SurvivalTimeText.text = "Время выживания: " + formattedTime;

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
