using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class SearchManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject profilePanel;
    public GameObject searchPanel;
    public TextMeshProUGUI profileNameText;
    public Image profileImage;
    public Button backButton;
    public Button enterButton;

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
        string name = nameInputField.text;
        DisplayProfile(name);
    }

    void DisplayProfile(string name)
    {
        profileNameText.text = name;
        profilePanel.SetActive(true);
        searchPanel.SetActive(false);
    }

    void OnBackButtonClicked()
    {
        profilePanel.SetActive(false);
        searchPanel.SetActive(true);

    }

}
