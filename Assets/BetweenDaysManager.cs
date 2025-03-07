using System.Collections;
using System.Collections.Generic;
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

    public TextMeshProUGUI detailsTopText;
    public TextMeshProUGUI detailsLeftText;
    public TextMeshProUGUI detailsRightText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        detailsTopText.text = "Employee Performance Log\n" + "Day " + PersistentData.currentDay;
        detailsLeftText.text = "People Saved: \n" + ListToText(PersistentData.peopleSavedToday) + "\n" + "People Damned: \n" + ListToText(PersistentData.peopleDamnedToday);
        detailsRightText.text = "People Who Needed Saving: \n" + ListToText(PersistentData.peopleWhoShouldBeSavedToday) + "\n" + "People Who Needed Damning: \n" + ListToText(PersistentData.peopleWhoShouldBeDamnedToday);
    }


    public string ListToText(List<PersonSchema> psList) {
        string returnString = "";
        foreach (PersonSchema ps in psList) {
            returnString += ps.name + "\n";
        }
        return returnString;

    }

}
