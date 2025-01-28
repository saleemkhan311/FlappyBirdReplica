using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScore;
    public Text NewScore;

     int score = 0;
     int highScore = 0;

    public GameObject LeaderBoard;
    public Image Medal;
    public Sprite[] medalSprites;
    public static ScoreManager instance;
    private void Start()
    {
        instance = this;
       
        
    }

    public void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("highScore",score);
    }

    public void SetScore()
    {
        score= 0;
        ScoreText.text = score.ToString();
        ScoreText.gameObject.SetActive(true);
        LeaderBoard.SetActive(false);
    }

    public void GameOver()
    {
        highScore = PlayerPrefs.GetInt("highScore",0);
        HighScore.gameObject.SetActive(true);
        HighScore.text = highScore.ToString();
        ScoreText.gameObject.SetActive(false);
        NewScore.gameObject.SetActive(true);
        NewScore.text = score.ToString();
    }

   

}
