using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;
using System.Collections.Generic;

public class SearchManager : MonoBehaviour
{
    public GameObject profilePanel;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI ageField;
    public TextMeshProUGUI occupationField;
    public TextMeshProUGUI netWorthField;
    public GameObject searchPanel;
    public Image profileImage;

    public List<PersonSchema> people = new List<PersonSchema>();
    void Start()
    {
        profilePanel.SetActive(false);
        searchPanel.SetActive(true);
    }


    void DisplayProfile(Person person)
    {
        profilePanel.SetActive(true);
        searchPanel.SetActive(false);
        profileImage.sprite = person.personSchema.profileImage;
        nameField.text = person.personSchema.name;
        ageField.text = person.personSchema.age.ToString();
        occupationField.text = person.personSchema.occupation;
        netWorthField.text = person.personSchema.netWorth.ToString();
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
