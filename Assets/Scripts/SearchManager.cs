using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;
using System.Collections.Generic;

public class SearchManager : MonoBehaviour
{
    public static SearchManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public GameObject profilePanel;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI ageField;
    public TextMeshProUGUI occupationField;
    public TextMeshProUGUI netWorthField;
    public GameObject searchPanel;
    public Image profileImage;

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
        nameField.text = $"Name: {person.personSchema.personName}";
        ageField.text = $"Age: {person.personSchema.age}";
        occupationField.text = $"Occupation: {person.personSchema.occupation}";
        netWorthField.text = $"Net Worth: {person.personSchema.netWorth}";
    }
    void DisplaySearch()
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
