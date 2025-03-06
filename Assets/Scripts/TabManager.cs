using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class TabManager : MonoBehaviour
{

    public static TabManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

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

    [Header("UI Elements")]
    public TextMeshProUGUI addressBar;

    [Header("Physics Holders")]
    public GameObject escalatorPhysicsLayer;
    public GameObject searchPhysicsLayer;
    public GameObject emailPhysicsLayer;
    public GameObject garbageBinPhysicsLayer;
    public GameObject escalatorPhysicsHiddenPosition;
    public GameObject searchPhysicsHiddenPosition;
    public GameObject emailPhysicsHiddenPosition;
    private GameObject lastActiveTab;
    public int currentTabIndex = 1;
    
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
    
    private void Start()
    {
        searchTabButton.onClick.AddListener(() => OpenTab(searchScreen, null));
        emailTabButton.onClick.AddListener(() => OpenTab(emailScreen, null));
        escalatorTabButton.onClick.AddListener(() => OpenTab(escalatorScreen, null));
        OpenTab(emailScreen, null);
    }

    private void Update()
    {
        if (searchScreen.activeSelf) {currentTabIndex = 0;}
        if (emailScreen.activeSelf) {currentTabIndex = 1;}
        if (escalatorScreen.activeSelf) {currentTabIndex = 2;}
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

        SetTabSprites(searchTabButton.gameObject, tabToOpen == searchScreen);
        SetTabSprites(emailTabButton.gameObject, tabToOpen == emailScreen);
        SetTabSprites(escalatorTabButton.gameObject, tabToOpen == escalatorScreen);

        if (tabToOpen == searchScreen)
        {
            addressBar.text = "https://www.googleinHELL.com";
            searchPhysicsLayer.transform.position = new Vector3(0, -0, -1);
            emailPhysicsLayer.transform.position = emailPhysicsHiddenPosition.transform.position;
            escalatorPhysicsLayer.transform.position = escalatorPhysicsHiddenPosition.transform.position;

            searchTabButton.transform.SetAsLastSibling();
        }
        else if (tabToOpen == emailScreen)
        {
            addressBar.text = "https://www.evilmail.net";
            emailPhysicsLayer.transform.position = new Vector3(0, 0, -1);
            searchPhysicsLayer.transform.position = searchPhysicsHiddenPosition.transform.position;
            escalatorPhysicsLayer.transform.position = escalatorPhysicsHiddenPosition.transform.position;

            emailTabButton.transform.SetAsLastSibling();
        }
        else if (tabToOpen == escalatorScreen)
        {
            addressBar.text = "C:/Users/Employee/ProgramFiles/escalator.exe";
            escalatorPhysicsLayer.transform.position = new Vector3(0, 0, -1);
            searchPhysicsLayer.transform.position = searchPhysicsHiddenPosition.transform.position;
            emailPhysicsLayer.transform.position = emailPhysicsHiddenPosition.transform.position;

            escalatorTabButton.transform.SetAsLastSibling();
        }

        if (person != null)
        {
            person.transform.SetParent(tabToOpen == searchScreen ? searchPhysicsLayer.transform :
                                    tabToOpen == emailScreen ? emailPhysicsLayer.transform :
                                    escalatorPhysicsLayer.transform, false);
        }
    }

    private void SetTabSprites(GameObject tabButton, bool isActive)
    {
        tabButton.transform.GetChild(0).gameObject.SetActive(isActive);
        tabButton.transform.GetChild(1).gameObject.SetActive(!isActive);
    }

    public GameObject GetLastActiveTab()
    {
        return lastActiveTab;
    }
}
