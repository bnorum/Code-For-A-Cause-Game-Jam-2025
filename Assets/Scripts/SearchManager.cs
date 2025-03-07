using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;
using System.Collections.Generic;

public class SearchManager : MonoBehaviour
{
    public static SearchManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public GameObject profilePanel;
    public TextMeshProUGUI nameAge;
    public TextMeshProUGUI addressZip;
    public TextMeshProUGUI email;
    public TextMeshProUGUI occupationField;
    public TextMeshProUGUI netWorthField;
    public TextMeshProUGUI marriages;
    public TextMeshProUGUI divorces;
    public TextMeshProUGUI handed;
    public TextMeshProUGUI hobby;
    public TextMeshProUGUI worstThing;
    public GameObject searchPanel;
    public Image profileImage;
    public GameObject successResults;
    public GameObject failedResults;

    public List<PersonSchema> people = new List<PersonSchema>();
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
    void Start()
    {
        profilePanel.SetActive(false);
        searchPanel.SetActive(true);
    }


    public void DisplayProfile(Person person)
    {
        profilePanel.SetActive(true);
        searchPanel.SetActive(false);
        if(person.personSchema.isEmailPerson)
        {
            failedResults.SetActive(true);
            successResults.SetActive(false);
            nameAge.text = person.personSchema.personName;
        }
        else
        {
            failedResults.SetActive(false);
            successResults.SetActive(true);
            nameAge.text = $"{person.personSchema.personName}, {person.personSchema.age}";
            addressZip.text = $"{person.personSchema.address} {person.personSchema.zipCode}";
            email.text = $"{person.personSchema.emailHandle}{person.personSchema.emailDomain}";
            switch (person.personSchema.netWorth)
            {
                case 1:
                netWorthField.text = "Wealth Bracket: Lower Class";
                break;
                case 2:
                netWorthField.text = "Wealth Bracket: Middle Class";
                break;
                case 3:
                netWorthField.text = "Wealth Bracket: Upper Class";
                break;
                case 4:
                netWorthField.text = "Wealth Bracket: Billionaire";
                break;
                default:
                netWorthField.text = "Wealth Bracket: Unknown";
                break;
            }
            marriages.text = $"Total Marriages: {person.personSchema.marriageNum}";
            divorces.text = $"Total Divorces: {person.personSchema.divorceNum}";
            handed.text = person.personSchema.isLefty ? "Left Handed" : "Right Handed";
            hobby.text = $"Hobby: {person.personSchema.hobby}";
            worstThing.text = $"Worst Thing ever done:\n {person.personSchema.worstThing}";   
        }
    }
    public void DisplaySearch()
    {
        profilePanel.SetActive(false);
        searchPanel.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            DisplayProfile(collision.gameObject.GetComponent<Person>());
        }     
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            DisplaySearch();
        }         

    }

}
