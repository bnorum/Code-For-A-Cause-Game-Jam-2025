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

    public TextMeshProUGUI emailCountText;

    public ScrollRect scrollView;
    public bool canScroll = true;

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
        emailSchemas = GameManager.Instance.GetAllEmailSchemas();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canScroll)
            scrollView.vertical = false;
        else
            scrollView.vertical = true;

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

        if (unreadEmails.Count == 0)
        {
            emailCountText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            emailCountText.transform.parent.gameObject.SetActive(true);
            emailCountText.text = unreadEmails.Count.ToString();
        }
        emailCountText.text = unreadEmails.Count.ToString();
    }

    public void AddEmail(EmailSchema emailSchema) {
        emailSound.Play();
        GameObject newEmail = Instantiate(emailPrefab, transform);
        newEmail.transform.SetParent(emailContainer.transform, false);
        newEmail.transform.SetSiblingIndex(0);
        Email email = newEmail.GetComponent<Email>();
        email.emailSchema = emailSchema;

        allEmails.Add(email);
        unreadEmails.Add(email);
        emailSchemas.Remove(emailSchema);
    }
    
}
