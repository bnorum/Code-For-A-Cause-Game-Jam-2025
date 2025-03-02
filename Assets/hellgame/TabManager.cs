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
    }
}
