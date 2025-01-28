using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverAnimation : MonoBehaviour
{
    public Image gameOverImage;  // Reference to the GameOver Image UI element
    public RectTransform leaderBoard;  // Reference to the LeaderBoard UI element
    public Button restartButton;  // Reference to the Restart Button

    private Vector3 gameOverStartPos = new Vector3(0, 455, 0);  // Initial position of GameOver Image (X remains 0)
    private Vector3 gameOverEndPos = new Vector3(0, 365, 0);    // Final position of GameOver Image (X remains 0)

    private Vector3 leaderBoardStartPos = new Vector3(0, -1200, 0);  // Initial position of LeaderBoard
    private Vector3 leaderBoardEndPos = new Vector3(0, 0, 0);        // Final position of LeaderBoard

    [SerializeField] private float moveDuration = 1f; // Duration for the move animation
    [SerializeField] private float moveGDuration = .2f; // Duration for the move animation
    [SerializeField] private float fadeDuration = 1f; // Duration for the fade in animation

    private void OnEnable()
    {
        // Start the Game Over animation sequence when the script is enabled
        leaderBoard.localPosition = leaderBoardStartPos;
        restartButton.gameObject.SetActive(false);
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // Start the fade-in and move animations for GameOver UI
        yield return StartCoroutine(FadeInAndMoveGameOver());

        // Start the leaderboard animation (Move from y=-1200 to y=0)
        yield return StartCoroutine(MoveLeaderboard());

        // Activate the Restart button when the Leaderboard reaches its final position
        
    }

    private IEnumerator FadeInAndMoveGameOver()
    {
        // Set initial positions and transparency
        gameOverImage.rectTransform.localPosition = gameOverStartPos;
        gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, 0); // Fully transparent

        float elapsed = 0f;
        // Fade in and move GameOver image simultaneously
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration); // Fade in
            gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, alpha);
            gameOverImage.rectTransform.localPosition = Vector3.Lerp(gameOverStartPos, gameOverEndPos, elapsed / moveGDuration); // Move GameOver Image

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and full opacity
        gameOverImage.rectTransform.localPosition = gameOverEndPos;
        gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, 1f);
    }

    private IEnumerator MoveLeaderboard()
    {
        float elapsed = 0f;

        // Move Leaderboard UI from y = -1200 to y = 0
        while (elapsed < moveDuration)
        {
            leaderBoard.localPosition = Vector3.Lerp(leaderBoardStartPos, leaderBoardEndPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }


        // Ensure final position
        leaderBoard.localPosition = leaderBoardEndPos;
        yield return new WaitForSeconds(0.5f);
        restartButton.gameObject.SetActive(true);
    }
}
