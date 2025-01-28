using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnim : MonoBehaviour
{
    public Image flashImage; // Assign your Image component here
    public float flashDuration = 0.5f; // Total duration of the flash effect
    public Color flashColor = Color.white; // Default flash color (can be changed dynamically)

    private void OnEnable()
    {
        // Start flash when the object is enabled
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        // Set initial color and activate the image
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        flashImage.gameObject.SetActive(true);

        float halfDuration = flashDuration / 2f;
        float elapsed = 0f;

        // Fade in (alpha 0 -> 1)
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / halfDuration);
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha); // Adjust alpha
            yield return null;
        }

        elapsed = 0f;

        // Fade out (alpha 1 -> 0)
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / halfDuration);
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha); // Adjust alpha
            yield return null;
        }

        // Deactivate the image after the flash
        flashImage.gameObject.SetActive(false);
    }

    // Public method to change the flash color dynamically
    public void SetFlashColor(Color newColor)
    {
        flashColor = newColor;
    }
}
