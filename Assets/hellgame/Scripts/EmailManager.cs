using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{

    public static EmailManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!



    public List<EmailSchema> emailSchemas;
    public GameObject emailPrefab;
    public GameObject emailContainer;
    public List<Email> currentEmails;
    public List<Email> allEmails;

    public List<Email> unreadEmails;
    public List<Email> readEmails;
    public List<Email> starredEmails;

    public TMP_Dropdown dropdownMenu;

    public AudioSource emailSound;

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

    // Update is called once per frame
    void Update()
    {
        if (dropdownMenu.value == 0) {
            currentEmails = allEmails;
        } else if (dropdownMenu.value == 1) {
            currentEmails = unreadEmails;
        } else if (dropdownMenu.value == 2) {
            currentEmails = readEmails;
        } else if (dropdownMenu.value == 3) {
            currentEmails = starredEmails;
        }

        foreach (Email email in allEmails)
        {
            email.gameObject.SetActive(currentEmails.Contains(email));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddEmail(emailSchemas[Random.Range(0, emailSchemas.Count)]);
        }
    }


    public void AddEmail(EmailSchema emailSchema) {
        emailSound.Play();
        GameObject newEmail = Instantiate(emailPrefab, transform);
        newEmail.transform.SetParent(emailContainer.transform, false);
        newEmail.transform.SetSiblingIndex(0);
        Email email = newEmail.GetComponent<Email>();
        email.emailSchema = emailSchema;
        allEmails.Add(email);
    }
}
