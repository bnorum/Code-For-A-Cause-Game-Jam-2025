using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource baseSong;
    public AudioSource[] stemSongs;


    private float fadeSpeed = 20f;
    private float maxVolume = 0.4f;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        StartMusic();
    }


    public void StartMusic() {
        baseSong.Play();
        foreach (AudioSource stemSong in stemSongs) {
            stemSong.Play();
        }
    }

    void Update()
    {
         if (TabManager.Instance.currentTabIndex == 0) {
            stemSongs[0].volume = Mathf.Lerp(stemSongs[0].volume, maxVolume, Time.deltaTime * fadeSpeed);
            stemSongs[1].volume = Mathf.Lerp(stemSongs[1].volume, 0, Time.deltaTime * fadeSpeed);
            stemSongs[2].volume = Mathf.Lerp(stemSongs[2].volume, 0, Time.deltaTime * fadeSpeed);
         }

         if (TabManager.Instance.currentTabIndex == 1) {
            stemSongs[0].volume = Mathf.Lerp(stemSongs[0].volume, 0, Time.deltaTime * fadeSpeed);
            stemSongs[1].volume = Mathf.Lerp(stemSongs[1].volume, maxVolume, Time.deltaTime * fadeSpeed);
            stemSongs[2].volume = Mathf.Lerp(stemSongs[2].volume, 0, Time.deltaTime * fadeSpeed);
         }

         if (TabManager.Instance.currentTabIndex == 2) {
            stemSongs[0].volume = Mathf.Lerp(stemSongs[0].volume, 0, Time.deltaTime * fadeSpeed);
            stemSongs[1].volume = Mathf.Lerp(stemSongs[1].volume, 0, Time.deltaTime * fadeSpeed);
            stemSongs[2].volume = Mathf.Lerp(stemSongs[2].volume, maxVolume, Time.deltaTime * fadeSpeed);
         }


    }

    




}
