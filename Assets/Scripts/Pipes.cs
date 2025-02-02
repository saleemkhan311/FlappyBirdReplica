using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pipes : MonoBehaviour
{
    public float Speed = 10.0f  ;
    private PlayerController playerController ;

    private float leftEdge;

   

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
       leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x-1f;

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive)
        {
            
            if (!GameManager.instance.isStarted) { return; }
            transform.position += Vector3.left * Speed * Time.deltaTime;

           
        }

       
      

        if (transform.position.x < leftEdge) 
        {
            Destroy(this.gameObject);
        }
        
    }

   


}
