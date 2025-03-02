using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public Button searchTabButton;
    public Button emailTabButton;
    public GameObject searchScreen;
    public GameObject emailScreen;

    private void Start()
    {
        searchTabButton.onClick.AddListener(() => OpenTab(searchScreen));
        emailTabButton.onClick.AddListener(() => OpenTab(emailScreen));
        OpenTab(emailScreen);

    }

    private void OpenTab(GameObject tabToOpen)
    {

        searchScreen.SetActive(tabToOpen == searchScreen);
        emailScreen.SetActive(tabToOpen == emailScreen);

        if (tabToOpen == searchScreen) {
            emailTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            searchTabButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            searchTabButton.transform.SetAsLastSibling();

        }
        else if (tabToOpen == emailScreen) {
            searchTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            emailTabButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            emailTabButton.transform.SetAsLastSibling();

        }
    }
}
