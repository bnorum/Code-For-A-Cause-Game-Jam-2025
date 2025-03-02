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

    [Header("People Holder")]
    public GameObject peopleHolder;
    private Vector3 peopleHolderStartPosition;

    private GameObject lastActiveTab;

    private void Start()
    {
        peopleHolderStartPosition = peopleHolder.transform.position;
        searchTabButton.onClick.AddListener(() => OpenTab(searchScreen));
        emailTabButton.onClick.AddListener(() => OpenTab(emailScreen));
        escalatorTabButton.onClick.AddListener(() => OpenTab(escalatorScreen));

        OpenTab(emailScreen);

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

    public void OpenTab(GameObject tabToOpen)
    {
        searchScreen.SetActive(tabToOpen == searchScreen);
        emailScreen.SetActive(tabToOpen == emailScreen);
        escalatorScreen.SetActive(tabToOpen == escalatorScreen);

        lastActiveTab = tabToOpen;

        if (tabToOpen == searchScreen)
        {
            emailTabButton.GetComponent<Image>().color = offColor;
            escalatorTabButton.GetComponent<Image>().color = offColor;
            searchTabButton.GetComponent<Image>().color = onColor;
            searchTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.googleinHELL.com";
            peopleHolder.transform.position = peopleHolderStartPosition + new Vector3(0, 0, 1);
        }
        else if (tabToOpen == emailScreen)
        {
            searchTabButton.GetComponent<Image>().color = offColor;
            escalatorTabButton.GetComponent<Image>().color = offColor;
            emailTabButton.GetComponent<Image>().color = onColor;
            emailTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.evilmail.net";
            peopleHolder.transform.position = peopleHolderStartPosition + new Vector3(0, 0, 1);
        }
        else if (tabToOpen == escalatorScreen)
        {
            searchTabButton.GetComponent<Image>().color = offColor;
            emailTabButton.GetComponent<Image>().color = offColor;
            escalatorTabButton.GetComponent<Image>().color = onColor;
            escalatorTabButton.transform.SetAsLastSibling();
            addressBar.text = "C:/Users/Employee/ProgramFiles/escalator.exe";
            peopleHolder.transform.position = peopleHolderStartPosition;
        }
    }

    public void OpenTabByCollider(Collider2D collider)
    {
        if (collider == searchTabCollider)
        {
            OpenTab(searchScreen);
        }
        else if (collider == emailTabCollider)
        {
            OpenTab(emailScreen);
        }
        else if (collider == escalatorTabCollider)
        {
            OpenTab(escalatorScreen);
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
