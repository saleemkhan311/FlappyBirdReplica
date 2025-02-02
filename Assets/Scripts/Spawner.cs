using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipe;

    public int delay = 3;

    public float maxHeight = 2.0f;
    public float minHeight = -1;

    private PlayerController playerController;

    public static Spawner instance;
    public int pipeCount;

    // Pipes

    private SpriteRenderer TopPipe;
    private SpriteRenderer BottomPipe;

    public Sprite top;
    public Sprite bottom;

    public float pipeIndex = 0;

    // [SerializeField] Sprite defaultTopSprite;
    // [SerializeField] Sprite defaultBottomSprite;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        instance = this;
        pipeCount = 0;
        
    }

    public void StartSpawning()
    {
        StartCoroutine(Coroutine());

    }

    public void StopSpawning()
    {
        StopCoroutine(Coroutine());
    }

    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {


        
    }

    IEnumerator Coroutine()
    {
        while (!playerController.isReady)
        {
            yield return null;

        }
        SpawnPipe();

        while (playerController.isAlive)
        {
            yield return new WaitForSeconds(delay);
            SpawnPipe();
        }

        


    }

   
    /*public Sprite defaultTopSprite;
    public Sprite defaultBottomSprite;
    public Sprite highScoreTopSprite;
    public Sprite highScoreBottomSprite;

    public void UpdatePipeAppearance(int pipeIndex)
    {
        // Ensure TopPipe and BottomPipe are set
        TopPipe = transform.GetChild(0).GetComponent<SpriteRenderer>();
        BottomPipe = transform.GetChild(2).GetComponent<SpriteRenderer>();

        // Check if this is the pipe just before reaching high score
        if (pipeIndex == ScoreManager.instance.highScore - 1)
        {
            TopPipe.sprite = highScoreTopSprite;
            BottomPipe.sprite = highScoreBottomSprite;
        }
        else
        {
            TopPipe.sprite = defaultTopSprite;
            BottomPipe.sprite = defaultBottomSprite;
        }
    }*/

    
    private void SpawnPipe()
    {
        pipeCount++;

        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), 0), Quaternion.identity);


        SpriteRenderer topPipeRenderer = newPipe.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer bottomPipeRenderer = newPipe.transform.GetChild(2).GetComponent<SpriteRenderer>();

        if(pipeCount == ScoreManager.instance.highScore )
        {
            if (pipeCount > ScoreManager.instance.highScore) { return; }
            topPipeRenderer.sprite = top;
            bottomPipeRenderer.sprite = bottom;
            
        }

        if (playerController.isDead) {pipeCount=0;}
    }
}
