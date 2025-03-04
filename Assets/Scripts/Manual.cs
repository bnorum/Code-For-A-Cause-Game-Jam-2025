using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manual : MonoBehaviour
{
    private Vector3 startPosition;
    public Transform shownPosition;
    public bool isShown = false;

    public TextMeshProUGUI manualTextBox;
    public List<string> manualText;
    public int pagenumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShown)
        {
            transform.position = Vector3.Lerp(transform.position, shownPosition.position, Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }


    }

    public void ToggleShown() {
        isShown = !isShown;
    }

    public void SwitchPage(bool isForward) {
        if (isForward) {
            pagenumber++;
            if (pagenumber >= manualText.Count) {
                pagenumber = 0;
            }
        } else {
            pagenumber--;
            if (pagenumber < 0) {
                pagenumber = manualText.Count - 1;
            }
        }
        manualTextBox.text = manualText[pagenumber];
    }
}
