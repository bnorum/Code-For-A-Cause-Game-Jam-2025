using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class TabManager : MonoBehaviour
{
    [Header("Tab Buttons")]
    public Button searchTabButton;
    public Button emailTabButton;
    public Button escalatorTabButton;

    [Header("Tab Screens")]
    public GameObject searchScreen;
    public GameObject emailScreen;
    public GameObject escalatorScreen;

    [Header("Tab Colliders")]
    public Collider2D searchTabCollider;
    public Collider2D emailTabCollider;
    public Collider2D escalatorTabCollider;

    [Header("DateTime")]
    public Button datetimeButton;
    public GameObject datetimePanel;

    [Header("UI Elements")]
    public TextMeshProUGUI addressBar;
    public Color onColor = new Color(1f, 1f, 1f, 1f);
    public Color offColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    [Header("Physics Holders")]
    public GameObject escalatorPeopleHolder;
    public GameObject searchPeopleHolder;
    public GameObject emailPeopleHolder;
    public GameObject escalatorPhysicsHiddenPosition;
    public GameObject searchPhysicsHiddenPosition;
    public GameObject emailPhysicsHiddenPosition;

    private GameObject lastActiveTab;

    private void Start()
    {
        searchTabButton.onClick.AddListener(() => OpenTab(searchScreen, null));
        emailTabButton.onClick.AddListener(() => OpenTab(emailScreen, null));
        escalatorTabButton.onClick.AddListener(() => OpenTab(escalatorScreen, null));

        OpenTab(emailScreen, null);

        AddEventTrigger(datetimeButton.gameObject, EventTriggerType.PointerEnter, () => ShowDateTimePanel(true));
        AddEventTrigger(datetimeButton.gameObject, EventTriggerType.PointerExit, () => ShowDateTimePanel(false));
        datetimePanel.SetActive(false);
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }

    public void OpenTab(GameObject tabToOpen, Person person)
    {

        searchScreen.SetActive(tabToOpen == searchScreen);
        emailScreen.SetActive(tabToOpen == emailScreen);
        escalatorScreen.SetActive(tabToOpen == escalatorScreen);
        
        lastActiveTab = tabToOpen;


        searchTabButton.GetComponent<Image>().color = (tabToOpen == searchScreen) ? onColor : offColor;
        emailTabButton.GetComponent<Image>().color = (tabToOpen == emailScreen) ? onColor : offColor;
        escalatorTabButton.GetComponent<Image>().color = (tabToOpen == escalatorScreen) ? onColor : offColor;

        if (tabToOpen == searchScreen)
        {
            addressBar.text = "https://www.googleinHELL.com";
            searchPeopleHolder.transform.position = new Vector3(2, -2, -1);
            emailPeopleHolder.transform.position = emailPhysicsHiddenPosition.transform.position;
            escalatorPeopleHolder.transform.position = escalatorPhysicsHiddenPosition.transform.position;
        }
        else if (tabToOpen == emailScreen)
        {
            addressBar.text = "https://www.evilmail.net";
            emailPeopleHolder.transform.position = new Vector3(0, 0, -1);
            searchPeopleHolder.transform.position = searchPhysicsHiddenPosition.transform.position;
            escalatorPeopleHolder.transform.position = escalatorPhysicsHiddenPosition.transform.position;
        }
        else if (tabToOpen == escalatorScreen)
        {
            addressBar.text = "C:/Users/Employee/ProgramFiles/escalator.exe";
            escalatorPeopleHolder.transform.position = new Vector3(0, 0, -1);
            searchPeopleHolder.transform.position = searchPhysicsHiddenPosition.transform.position;
            emailPeopleHolder.transform.position = emailPhysicsHiddenPosition.transform.position;
        }


        if (person != null)
        {// Parent the Person object if provided
            person.transform.SetParent(tabToOpen == searchScreen ? searchPeopleHolder.transform :
                                    tabToOpen == emailScreen ? emailPeopleHolder.transform :
                                    escalatorPeopleHolder.transform, false);
        }
    }


    public GameObject GetLastActiveTab()
    {
        return lastActiveTab;
    }

    private void ShowDateTimePanel(bool show)
    {
        datetimePanel.SetActive(show);
        if (show)
        {
            datetimePanel.transform.SetAsLastSibling();
        }
    }
}
