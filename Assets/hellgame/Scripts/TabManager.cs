using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class TabManager : MonoBehaviour
{
    public Button searchTabButton;
    public Button emailTabButton;
    public GameObject searchScreen;
    public GameObject emailScreen;
    public Button escalatorButton;
    public Button datetimeButton;
    public GameObject escalatorScreen;
    public GameObject datetimePanel;
    public TextMeshProUGUI addressBar;
    public Color onColor = new Color(1f, 1f, 1f, 1f);
    public Color offColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    public GameObject peopleHolder; 
    private Vector3 peopleHolderStartPosition;
    public static event Action OnEscalatorTabOpened;
    public static event Action OnEscalatorTabClosed;

    private void Start()
    {
        peopleHolderStartPosition = peopleHolder.transform.position;
        searchTabButton.onClick.AddListener(() => OpenTab(searchScreen));
        emailTabButton.onClick.AddListener(() => OpenTab(emailScreen));
        escalatorButton.onClick.AddListener(() => OpenTab(escalatorScreen));
        
        OpenTab(emailScreen);
        // Add hover listeners for datetimeButton
        AddEventTrigger(datetimeButton.gameObject, EventTriggerType.PointerEnter, () => ShowDateTimePanel(true));
        AddEventTrigger(datetimeButton.gameObject, EventTriggerType.PointerExit, () => ShowDateTimePanel(false));
        }
    
        private void AddEventTrigger(GameObject obj, EventTriggerType type, Action action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener((eventData) => action());
            trigger.triggers.Add(entry);
        }

    private void OpenTab(GameObject tabToOpen)
    {
        searchScreen.SetActive(tabToOpen == searchScreen);
        emailScreen.SetActive(tabToOpen == emailScreen);
        escalatorScreen.SetActive(tabToOpen == escalatorScreen);

        if (tabToOpen == searchScreen) {
            emailTabButton.GetComponent<Image>().color = offColor;
            searchTabButton.GetComponent<Image>().color = onColor;
            searchTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.googleinHELL.com";
            peopleHolder.transform.position = peopleHolderStartPosition + new Vector3(0, 0, 1);
        }
        else if (tabToOpen == emailScreen) {
            searchTabButton.GetComponent<Image>().color = offColor;
            emailTabButton.GetComponent<Image>().color = onColor;
            emailTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.evilmail.net";
            peopleHolder.transform.position = peopleHolderStartPosition + new Vector3(0, 0, 1);
        }
        else if (tabToOpen == escalatorScreen) {
            searchTabButton.GetComponent<Image>().color = offColor;
            emailTabButton.GetComponent<Image>().color = offColor;
            escalatorButton.transform.SetAsLastSibling();
            addressBar.text = "Escalator";
            peopleHolder.transform.position = peopleHolderStartPosition;
        }
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
