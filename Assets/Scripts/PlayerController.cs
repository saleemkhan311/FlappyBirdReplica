using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Movements

    private Vector3 velocity;

    private float strength = 5.0f;

    private float gravity = -12f;

    private Rigidbody2D rb;

    
    //Other

    [NonSerialized] public bool isReady;
   [SerializeField] private float rotationSpeed = 1f;
   [NonSerialized] public bool isAlive;
   [NonSerialized] public bool isGrounded = false;

    //Animation

    private float animSpeed = .06f;

    private SpriteRenderer spriteRenderer;



    private int spriteIndex;

    private float animTimer = .15f;
    private float tiltAngle = 90.0f;

    public Image white;
    public Sprite[] sprites;

   
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
       
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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!isAlive) { return; }
            velocity.y = strength;
            isReady = true;
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
            float tilt = Mathf.Clamp(velocity.y * tiltAngle / strength, -tiltAngle, tiltAngle - 60);
            float targetTilt = Mathf.Lerp(transform.rotation.z, tilt, Time.deltaTime / rotationSpeed);


            transform.rotation = Quaternion.Euler(0, 0, tilt);
        }
    }

    private void GameOver()
    {
        GameManager.instance.SetLeaderBoard();
        Spawner.instance.StopSpawning();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isAlive) { white.gameObject.SetActive(true); }

            isAlive = false;
            
            if (collision.gameObject.name == "Ground")
            {
                isGrounded = true;
                isReady = false;
                GameOver();
            }
        }





        if (collision.gameObject.CompareTag("ScoreT"))
        {
            ScoreManager.instance.AddScore();
        }

    
    }

}
