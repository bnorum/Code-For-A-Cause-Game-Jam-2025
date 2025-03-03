using UnityEngine;
using TMPro;

public class escalatorScreenManager : MonoBehaviour
{
    public TMP_Text uiText;
    private string[] loadingFrames = { "--", "\\", "|", "/" };
    private int currentFrame = 0;
    private float timer = 0f;
    private float frameRate = 0.2f;
    void Start()
    {
        if (uiText != null)
        {
            uiText.text = "excalator.exe running... " + loadingFrames[currentFrame];
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
                uiText.text = "excalator.exe running... " + loadingFrames[currentFrame];
            }
        }
    }
}
