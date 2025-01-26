using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movements

    private Vector3 velocity;

    public float strength = 5.0f;

    public float gravity = -9.8f;

    private Rigidbody2D rb;

    //Animation

    public float animSpeed = .15f;

    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private int spriteIndex;

    private float animTimer = .15f;
    public float tiltAngle = 30.0f;

    //Other

    public bool isReady;
    public float rotationSpeed = 10000f;
    public bool isAlive;
    public bool isGrounded = false;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        InvokeRepeating(nameof(playerAnime),animSpeed,animSpeed);
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        playerClamp();
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
       

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!isAlive) { return; }
            velocity.y = strength;
            isReady = true;
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

        if (animTimer >= animSpeed)
        {

            spriteIndex++;
            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[spriteIndex] != null)
                {
                    spriteRenderer.sprite = sprites[spriteIndex];
                }
            }
        }
        float tilt = Mathf.Clamp(velocity.y * tiltAngle / strength, -tiltAngle, tiltAngle - 60);
        float targetTilt = Mathf.Lerp(transform.rotation.z, tilt, Time.deltaTime * rotationSpeed);


        transform.rotation = Quaternion.Euler(0, 0, tilt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
           
            isAlive = false;
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.name == "Ground")
            {
                isGrounded = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }

    }

}
