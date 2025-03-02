using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; // Import for Action events

public class TabManager : MonoBehaviour
{
    public Button searchTabButton;
    public Button emailTabButton;
    public GameObject searchScreen;
    public GameObject emailScreen;
    public Button escalatorButton;
    public GameObject escalatorScreen;
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
}
