using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController controller;
    private Pipes pipes;
    [NonSerialized]
    public bool isStarted = false;
    public GameObject player;
    public GameObject flappyBird;
    private float amplitude = 0.2f; // Half of the total range (2.3 - 1.8) / 2
    private float speed = 5f;
    public Image black;
    public GameObject StartBtn;
    public GameObject tut;
    public Button PauseBtn;
    public Sprite playImg;
    public Sprite pauseImg;
    bool isToggled = true;
    Vector3 f ;
    Vector3 p ;
    private Vector3 playerOrigin;
    private Vector3 playerFirstPos;
    public static GameManager instance;

    // Score

    public GameObject LeaderBoard;
    public Image Medal;
    public Sprite[] medalSprites;

    // Sound
    private AudioSource audioSource;
    public AudioClip woosh;


    private void Start()
    {
        instance = this;
        controller = FindAnyObjectByType<PlayerController>();
        pipes = FindAnyObjectByType<Pipes>();
        isStarted = false;
         f = flappyBird.transform.position;
         p = player.transform.position;
        playerOrigin = new Vector3(-1.15f, .75f, 0f);
        playerFirstPos = new Vector3(2.11999989f, 2.0999999f, 0f);
        audioSource= gameObject.AddComponent<AudioSource>();


    }

    private void Update()
    {
        SplashAnim();
        
    }

    public void StartButton()
    {
        black.gameObject.SetActive(true);


        audioSource.PlayOneShot(woosh);
        StartCoroutine(Delay(.2f)); // 2f is the delay in seconds

        

    }



    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        PlayState();
        Spawner.instance.StartSpawning();
    }
    private void PlayState()
    {
      

        isStarted = true;
        player.transform.position = playerOrigin;
        player.transform.rotation = Quaternion.Euler(0,0,0);
        flappyBird.SetActive(false);
        StartBtn.SetActive(false);
        tut.SetActive(true);
        PauseBtn.gameObject.SetActive(true);
        ScoreManager.instance.SetScore();
        controller.isReady = false;
        
    }

    
    private void PauseState()
    {

    }

    public void PauseButton()
    {
        isToggled = !isToggled;

        if (!isToggled)
        {
            PauseBtn.image.sprite = playImg; 
            Time.timeScale = 0f;
        }else if (isToggled)
        {
            PauseBtn.image.sprite = pauseImg;
            Time.timeScale = 1f;
        }
    }
    private void SplashAnim()
    {
        if (!isStarted)
        {
            

            float flappyY = Mathf.Sin(Time.time * speed)*amplitude + f.y;
            float playerY = Mathf.Sin(Time.time *speed)*amplitude + p.y;

            flappyBird.transform.position = new Vector3(f.x, flappyY, f.z);
            player.transform.position = new Vector3(p.x, playerY, p.z);
            player.transform.rotation = Quaternion.identity;

        }
    }

    private IEnumerator DelayAnim(float delay)
    {
        yield return new WaitForSeconds(delay);

        Pipes[] pipes = UnityEngine.Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        player.transform.position = playerFirstPos;
        player.transform.rotation = Quaternion.identity;
        flappyBird.SetActive(true);
        PauseBtn.gameObject.SetActive(false);
        StartBtn.SetActive(true);

        isStarted = false;
        LeaderBoard.SetActive(false);

        controller.isAlive = true;
        controller.isGrounded = false;

    }
    public void RestartGame()
    {
        black.gameObject.SetActive(true);
        audioSource.PlayOneShot(woosh);
        StartCoroutine(DelayAnim(.2f)); 
        controller.isDead = false;
    }


    // Score

    public void SetLeaderBoard()
    {
        LeaderBoard.SetActive(true);
        ScoreManager.instance.GameOver();
    }

   

    

}
