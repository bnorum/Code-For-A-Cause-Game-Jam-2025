using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{

    public List<Email> emails;
    public List<EmailSchema> emailSchemas;
    public GameObject emailPrefab;
    public GameObject emailContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddEmail(EmailSchema emailSchema) {
        GameObject newEmail = Instantiate(emailPrefab, transform);
        newEmail.transform.SetParent(emailContainer.transform, false);
        Email email = newEmail.GetComponent<Email>();
        email.emailSchema = emailSchema;
        emails.Add(email);
    }
}
