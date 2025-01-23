using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movements

    private Vector3 velocity;

    public float strength = 5.0f;

    public float gravity = -9.8f;

    //Animation

    public float animSpeed = .15f;

    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating(nameof(playerAnime),animSpeed,animSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)  || Input.GetMouseButtonDown(0)) 
        {
            Jump();
        }

        velocity.y  += gravity * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;


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
        velocity.y = strength;
        
    }

    private void playerAnime()
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
}
