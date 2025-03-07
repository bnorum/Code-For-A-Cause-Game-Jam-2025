using UnityEngine;
using TMPro;

public class escalatorScreenManager : MonoBehaviour
{
    public TMP_Text uiText;
    public TMP_Text sentToHeavenText;
    public TMP_Text sentToHellText;
    private string[] loadingFrames = { ".", "..", "..." };
    private int currentFrame = 0;
    private float timer = 0f;
    private float frameRate = .75f; // Change frame every second
    public string text;

    void Start()
    {
        if (uiText != null)
        {
            uiText.text = text + loadingFrames[currentFrame];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uiText != null)
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % loadingFrames.Length;
                uiText.text = $"{text} {loadingFrames[currentFrame]}";
            }
        }
    }
}
