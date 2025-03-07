using UnityEngine;

public class CreditsLinks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalamityYoutube() {
        Application.OpenURL("https://www.youtube.com/@Calamity-Rhapsody");
    }
    public void CalamityBandCamp() {
        Application.OpenURL("https://calamity-rhapsody.bandcamp.com/album/dolavendi-mirror-sky");
    }

    public void JessFloresArtStation() {
        Application.OpenURL("https://www.artstation.com/jessflores606");
    }
}
