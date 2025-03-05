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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
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
}
