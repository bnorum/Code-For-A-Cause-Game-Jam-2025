using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject panel; // Reference to the panel (canvas)
    
    public void ShowPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.Instance.LoadMainMenu();
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
