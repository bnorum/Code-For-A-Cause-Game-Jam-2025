using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TextMeshProUGUI details2Text;
    public TextMeshProUGUI details3Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        detailsText.text = "Employee Performance Log\n" + "Day " + PersistentData.currentDay;
        details2Text.text = "People Saved:\n" + ListToString(PersistentData.peopleSaved) + "\nPeople Damned: \n" + ListToString(PersistentData.peopleDamned);
        details3Text.text = "People Who Should've Been Saved:\n" + ListToString(PersistentData.peopleShouldveSavedToday) + "\nPeople Who Should've Been Damned: \n" + ListToString(PersistentData.peopleShouldveDamnedToday);
    }

    string ListToString(List<PersonSchema> psList) {
        string returnString = "";
        foreach (PersonSchema ps in psList) {
            returnString += ps.personName + "\n";
        }
        return returnString;
    }

}
