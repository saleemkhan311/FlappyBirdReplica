using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipe;

    public int delay = 3;

    public float maxHeight = 2.0f;
    public float minHeight = -1;

    private PlayerController playerController;
    
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        StartCoroutine(Coroutine());
        
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
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), 0), Quaternion.identity);

        while (playerController.isAlive)
        {
            yield return new WaitForSeconds(delay);
            Instantiate(pipe, new Vector3(transform.position.x,Random.Range(minHeight,maxHeight),0), Quaternion.identity);
        }
    }
}
