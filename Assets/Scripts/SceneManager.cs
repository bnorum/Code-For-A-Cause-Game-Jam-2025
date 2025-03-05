using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

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

    public Canvas loadingCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNewDay()
    {
        enableLoadingCanvas();
        PersistentData.currentDay++;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }

    public void LoadNewGame()
    {
        enableLoadingCanvas();
        PersistentData.currentDay=1;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }

    public void LoadMainMenu(bool isEndGame = false)
    {
        enableLoadingCanvas();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    public void LoadBetweenDay()
    {
        enableLoadingCanvas();
        if (PersistentData.currentDay == 5)
        {
            LoadMainMenu(true);
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }
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
