using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = FindAnyObjectByType<PlayerController>();
    }
}
