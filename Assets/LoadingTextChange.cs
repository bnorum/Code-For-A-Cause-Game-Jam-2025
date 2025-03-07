using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTextChange : MonoBehaviour
{
    public List<string> loadingTexts;
    public TextMeshProUGUI loadingTextUI;
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadingTexts = new List<string>
        {
            "Loading.",
            "Loading..",
            "Loading..."
        };

    }

    void Awake()
    {
        StartCoroutine(ChangeLoadingText(0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        loadingTextUI.text = loadingTexts[index];
    }

    public System.Collections.IEnumerator ChangeLoadingText(float waitTime)
    {
        while (true)
        {
            // Update the loading text
            index = (index + 1) % loadingTexts.Count;

            // Wait for the specified time
            yield return new WaitForSeconds(waitTime);
        }
    }
}
