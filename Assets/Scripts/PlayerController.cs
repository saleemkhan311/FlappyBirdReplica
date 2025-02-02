using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Movements

    private Vector3 velocity;

   [SerializeField] private float strength = 5.0f;

   [SerializeField] private float gravity = -20f;

    private Rigidbody2D rb;

    
    //Other

    [NonSerialized] public bool isReady;
    [NonSerialized] public bool isAlive;
    [NonSerialized] public bool isGrounded = false;


    //Sound

    private AudioSource audioSource;
    public AudioClip flap;
    public AudioClip hit;
    public AudioClip scoreCollect;
    public AudioClip fall;

    //Animation

    private float animSpeed = .06f;

    private SpriteRenderer spriteRenderer;

    float r; 

    [SerializeField] private float upTiltSmoothTime = 0.2f; // Adjust for smooth upward tilt
    [SerializeField] private float downTiltSmoothTime = 0.05f; // Lower for fast downward tilt


    private int spriteIndex;

    private float animTimer = .15f;
    private float tiltAngle = 90.0f;

    public Image white;
    public Sprite[] sprites;

   
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
       
    }
    void Start()
    {
       //InvokeRepeating(nameof(playerAnime),animSpeed,animSpeed);
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        playerClamp();
        playerAnime();


    }
    
    private void playerClamp()
    {
        if(transform.position.y >= 4.7f)
        {
            transform.position = new Vector3(transform.position.x,4.7f,transform.position.z);
        }
    }

    private void Jump()
    {
        if (!GameManager.instance.isStarted) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAlive) { return; }
            velocity.y = strength;
            audioSource.PlayOneShot(flap);
            if (!isReady) { isReady = true; }
            
            GameManager.instance.tut.SetActive(false);
        }

        if (isReady & !isGrounded)
        {
            
            
            velocity.y += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

        
        
    }

    private void playerAnime()
    {
        if (isGrounded) { return; }
        

        animTimer += Time.deltaTime;

        if (animTimer >= animSpeed && isAlive)
        {
            

            spriteIndex++;
            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }

           
                if (sprites[spriteIndex] != null)
                {
                    spriteRenderer.sprite = sprites[spriteIndex];
                }
            

                animTimer = 0;
        }

        if (!GameManager.instance.isStarted) { return; }
        if (isReady & !isGrounded)
        {
            /*float tilt = Mathf.Clamp(velocity.y * tiltAngle / strength, -tiltAngle, tiltAngle - 60);
            //float targetTilt = Mathf.Lerp(transform.rotation.z, tilt, Time.deltaTime / rotationSpeed);
            float smoothAgnle = Mathf.SmoothDampAngle(transform.eulerAngles.z, tilt, ref r, rotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, smoothAgnle);

            Debug.Log($"Tilt: {tilt} Velocity: {velocity.y} Smooth Angle: {smoothAgnle}");*/


            float targetTilt = Mathf.Clamp(velocity.y * tiltAngle / strength, -tiltAngle, tiltAngle-60);
            float currentRotationSpeed = (velocity.y < 0) ? downTiltSmoothTime : upTiltSmoothTime;
            float smoothedTilt = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetTilt, Time.deltaTime * currentRotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, smoothedTilt);



        }
    }

    private void GameOver()
    {
        GameManager.instance.SetLeaderBoard();
        Spawner.instance.StopSpawning();
        GameManager.instance.PauseBtn.gameObject.SetActive(false);
    }

   public bool isDead = false;

    private void PlayDelay(){ audioSource.PlayOneShot(fall);}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isAlive) 
            { 
                white.gameObject.SetActive(true);
                GameOver();
            }

            isAlive = false;
            if (!isDead && (collision.gameObject.name == "Top" || collision.gameObject.name =="Bottom"))
            {
                audioSource.PlayOneShot(hit);
                Invoke("PlayDelay", hit.length - .2f);
                isDead = true;
            }
            


            if (collision.gameObject.name == "Ground")
            {
                isGrounded = true;
                isReady = false;
                if (!isDead)
                {
                    audioSource.PlayOneShot(hit);
                    isDead = true;
                }
                
            }

           
            
            

            
            
            
        }





        if (collision.gameObject.CompareTag("ScoreT"))
        {
            ScoreManager.instance.AddScore();
            audioSource.PlayOneShot(scoreCollect);
        }

    
    }

}
