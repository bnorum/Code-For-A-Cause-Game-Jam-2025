using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;
using System.Collections.Generic;

public class SearchManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject profilePanel;
    public GameObject searchPanel;
    public TextMeshProUGUI profileNameText;
    public Image profileImage;
    public Button backButton;
    public Button enterButton;
    public List<PersonSchema> people = new List<PersonSchema>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        profilePanel.SetActive(false);
        backButton.onClick.AddListener(OnBackButtonClicked);
        enterButton.onClick.AddListener(OnEnterButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterButtonClicked()
    {
        SearchForMatch(nameInputField.text);
        DisplayProfile(name);
    }

    private void SearchForMatch(string text)
    {
        foreach (PersonSchema person in people)
        {
            if (person.personName == text)
            {
                name = person.personName;
                break;
            }
            else
            {
                name = "No match found, must be exact name";
            }
        }
    }

    void DisplayProfile(string name)
    {
        profileNameText.text = name;
        profilePanel.SetActive(true);
        searchPanel.SetActive(false);
    }
    void DisplaySearch()
    {
        profilePanel.SetActive(false);
        searchPanel.SetActive(true);
    }
    void OnBackButtonClicked()
    {
        DisplaySearch();
    }

}
