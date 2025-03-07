using System.Collections;
using TMPro;
using UnityEngine;

public class BetweenDaysManager : MonoBehaviour
{
    public static BetweenDaysManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

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

    public TextMeshProUGUI detailsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        detailsText.text = "Employee Performance Log\n" + "Day " + PersistentData.currentDay + "\n\nPeople Saved: " + PersistentData.peopleSaved.Count + "\nPeople Damned: " + PersistentData.peopleDamned.Count + "\nCoworkers Schmoozed: " + (20-PersistentData.remainingCoworkers.Count);
    }


}
