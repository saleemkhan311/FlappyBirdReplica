using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScore;
    public Text NewScore;
    public Image NewBestScoreLabel;
    public int score = 0;
    public int highScore = 0;

    public GameObject LeaderBoard;
    public Image Medal;
    public Sprite[] medalSprites;
    public static ScoreManager instance;


    private RectTransform medalArea; // Assign your circular GameObject's RectTransform
    public float animationDuration = 1f; // Time for scaling animation
    public float maxRadius = 100f; // Maximum radius within the circular area

    public RectTransform imageRect;

    
    IEnumerator AnimateAndMove()
    {
        while (true)
        {
            // Set a new random position inside the circle
            Vector2 randomPos = GetRandomPointInCircle();
            imageRect.anchoredPosition = randomPos;

            // Scale up from 0 to 1
            yield return StartCoroutine(ScaleImage(0f, 1f, animationDuration));

            // Hold at full scale for a moment
            yield return new WaitForSeconds(0.2f);

            // Scale down from 1 to 0
            yield return StartCoroutine(ScaleImage(1f, 0f, animationDuration));

            // Wait a little before moving again
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ScaleImage(float startScale, float endScale, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float scale = Mathf.Lerp(startScale, endScale, time / duration);
            imageRect.localScale = new Vector3(scale, scale, 1f);
            time += Time.deltaTime;
            yield return null;
        }
        imageRect.localScale = new Vector3(endScale, endScale, 1f);
    }

    Vector2 GetRandomPointInCircle()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float radius = Random.Range(0f, maxRadius);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector2(x, y);
    }


    private void Start()
    {
        instance = this;

       medalArea = Medal.rectTransform;
        StartCoroutine(AnimateAndMove());
        PlayerPrefs.SetInt("highScore", 3);
        PlayerPrefs.Save();
        highScore = PlayerPrefs.GetInt("highScore", 0);
         
    }

    public void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
       // PlayerPrefs.SetInt("highScore",score);
    }

    public void SetScore()
    {
        score= 0;
        ScoreText.text = score.ToString();
        ScoreText.gameObject.SetActive(true);
        LeaderBoard.SetActive(false);
    }

    private void Update()
    {
       
    }

    public void GameOver()
    {
        highScore = PlayerPrefs.GetInt("highScore",0);
        HighScore.gameObject.SetActive(true);
        HighScore.text = highScore.ToString();
        ScoreText.gameObject.SetActive(false);
        NewScore.gameObject.SetActive(true);
        NewScore.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            HighScore.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();

            NewBestScoreLabel.gameObject.SetActive(true);
        }
        else { NewBestScoreLabel.gameObject.SetActive(false); }

        if(score > 9)
        {
            Medal.gameObject.SetActive(true);
            StartCoroutine(AnimateAndMove());
            if (score >= 10 && score <= 19)
            {
                Medal.sprite = medalSprites[0];
            }
            else if(score >= 20 && score <= 29)
            {
                Medal.sprite = medalSprites[1];
            }
            else if(score >= 30 && score <= 39)
            {
                Medal.sprite = medalSprites[2];
            }
            else if(score >= 40)
            {
                Medal.sprite = medalSprites[3];
            }
        }
        else { Medal.gameObject.SetActive(false);}
       
    }

   

}
