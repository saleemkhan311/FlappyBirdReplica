using UnityEngine;

public class Background : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;
    public float animSpeed = 0.1f;
    private PlayerController playerController;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (playerController.isAlive) { m_MeshRenderer.material.mainTextureOffset += new Vector2(animSpeed * Time.deltaTime, 0); }
        
    }
}
