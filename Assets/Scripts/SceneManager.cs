using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
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
    public Canvas OnOpenCanvas;

    public GameObject clockOutMachineBill;
    public GameObject clockOutMachineBillTarget;
    public bool startedLoadingBetweenDay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (OnOpenCanvas != null && ((UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0 && PersistentData.isGameOver) || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0))
            StartCoroutine(OnOpen());
    }

    // Update is called once per frame
    void Update()
    {
        if (startedLoadingBetweenDay) {
            clockOutMachineBill.transform.position = Vector3.Lerp(clockOutMachineBill.transform.position, clockOutMachineBillTarget.transform.position, Time.deltaTime * 5f);


        }
    }

    public void LoadNewDay()
    {
        enableLoadingCanvas(1);
        PersistentData.currentDay++;
    }

    public void LoadNewGame()
    {
        enableLoadingCanvas(3);
        PersistentData.currentDay=0;
    }

    public void LoadMainMenu(bool isEndGame = false)
    {

        PersistentData.isGameOver = true;
        enableLoadingCanvas(0);
    }

    public void LoadBetweenDay()
    {
        startedLoadingBetweenDay = true;
        if (PersistentData.currentDay == 5)
        {
            LoadMainMenu(true);
        } else {
            enableLoadingCanvas(2);
        }
    }

    public void SkipToDay5() {
        PersistentData.currentDay = 4;
        LoadNewDay();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void enableLoadingCanvas(int scenenum)
    {
        StartCoroutine(FadeInLoadingCanvasIntoScene(scenenum));
    }

    IEnumerator FadeInLoadingCanvasIntoScene(int scenenum)
    {
        CanvasGroup canvasGroup = loadingCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = loadingCanvas.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        loadingCanvas.gameObject.SetActive(true);

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / 1.0f; // 1 second fade in
            yield return null;
        }

        canvasGroup.alpha = 1;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenenum);

    }


    public IEnumerator OnOpen() {
        OnOpenCanvas.gameObject.SetActive(true);
        CanvasGroup canvasGroup = OnOpenCanvas.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            yield return new WaitForSeconds(1);
            while (canvasGroup.alpha > 0)
            {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
            }
            canvasGroup.alpha = 0;
        }
        OnOpenCanvas.gameObject.SetActive(false);
    }






}
