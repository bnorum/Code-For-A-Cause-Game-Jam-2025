using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabManager : MonoBehaviour
{
    public Button searchTabButton;
    public Button emailTabButton;
    public GameObject searchScreen;
    public GameObject emailScreen;
    public Button escalatorButton;
    public GameObject escalatorScreen;
    public TextMeshProUGUI addressBar;

    private void Start()
    {
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
            emailTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            searchTabButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            searchTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.googleinHELL.com";

        }
        else if (tabToOpen == emailScreen) {
            searchTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            emailTabButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            emailTabButton.transform.SetAsLastSibling();
            addressBar.text = "https://www.evilmail.net";
        }
        else if (tabToOpen == escalatorScreen) {
            searchTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            emailTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            escalatorButton.transform.SetAsLastSibling();
            addressBar.text = "Escalator";
        }
    }
}
