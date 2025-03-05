using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Canvas loadingCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNewDay(int daynum)
    {
        PersistentData.currentDay = daynum;
        enableLoadingCanvas();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }

    public void LoadMainMenu()
    {
        enableLoadingCanvas();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    public void LoadBetweenDay()
    {
        enableLoadingCanvas();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void enableLoadingCanvas()
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(true);
        }
    }
}
