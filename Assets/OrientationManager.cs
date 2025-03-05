using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class OrientationManager : MonoBehaviour
{

    public List<VideoClip> videos;
    public List<string> associatedDetails;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI associatedDetailsText;
    public int index = 0;
    public float screenShakeTimer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        screenShakeTimer -= Time.deltaTime;
        if (screenShakeTimer < 0.0f)
        {
            StartCoroutine(ScreenShake());
            screenShakeTimer = Random.Range(15,20);
        }
        associatedDetailsText.text = associatedDetails[index];
    }


    public void RollBackText()
    {
        index--;
        if (index < 0) index = videos.Count - 1;
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }

    public void AdvanceText()
    {
        index = (index + 1) % videos.Count;
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }

    public IEnumerator ScreenShake() {
        Vector3 originalPosition = videoPlayer.transform.position;
        float elapsed = 0.0f;

        while (elapsed < 0.1f)
        {
            float x = Random.Range(-0.05f, 0.05f);
            float y = Random.Range(-0.05f, 0.05f);

            videoPlayer.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += 0.05f;

            yield return new WaitForSeconds(0.05f);
        }

        videoPlayer.transform.position = originalPosition;
    }
}
