using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movements

    private Vector3 velocity;

    public float strength = 5.0f;

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
