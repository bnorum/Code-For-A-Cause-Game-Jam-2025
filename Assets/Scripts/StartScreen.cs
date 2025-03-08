using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public static StartScreen Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

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


    public Slider difficultySlider;
    public TextMeshProUGUI difficultyText;

    public GameObject StartMenu;
    public GameObject DifficultyMenu;

    public Transform targetPosition;
    public Transform hidePosition;

    public bool isDifficultyShown = false;

    public List<AudioSource> stems;

    public Canvas creditsCanvas;
    public Canvas gameOverCanvas;

    public Image titleImage;
    public Sprite titleSprite1;
    public Sprite titleSprite2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PersistentData.isGameOver) PlayMusic();

        StartCoroutine(titleSwap());

    }

    // Update is called once per frame
    void Update()
    {
        PersistentData.difficultyScale = difficultySlider.value;
        difficultyText.text = Mathf.RoundToInt(difficultySlider.value * 100f) + "%";

        if (isDifficultyShown) {
            StartMenu.transform.position = Vector3.Lerp(StartMenu.transform.position, hidePosition.position, Time.deltaTime * 5f);
            DifficultyMenu.transform.position = Vector3.Lerp(DifficultyMenu.transform.position, targetPosition.position, Time.deltaTime * 5f);
        } else {
            StartMenu.transform.position = Vector3.Lerp(StartMenu.transform.position, targetPosition.position, Time.deltaTime * 5f);
            DifficultyMenu.transform.position = Vector3.Lerp(DifficultyMenu.transform.position, hidePosition.position, Time.deltaTime * 5f);
        }
    }

    public void LoadDifficultyMenu()
    {
        isDifficultyShown = true;
    }

    public void LoadMainMenu()
    {
        isDifficultyShown = false;
    }

    public void ToggleCredits() {
        creditsCanvas.gameObject.SetActive(!creditsCanvas.gameObject.activeSelf);
    }

    public void ToggleGameOverScreen() {
        gameOverCanvas.gameObject.SetActive(!gameOverCanvas.gameObject.activeSelf);
    }

    public void PlayMusic() {
        foreach (AudioSource stem in stems) {
            stem.Play();
        }
    }

    public void OnLose() {
        foreach (AudioSource stem in stems) {
            stem.pitch = Random.Range(0.7f,1.3f);
        }
    }

    public void OnWin() {
        foreach (AudioSource stem in stems) {
            stem.pitch = 1;
        }
    }

    public IEnumerator titleSwap() {
        while (true) {
            yield return new WaitForSeconds(0.2f);
            titleImage.sprite = titleSprite1;
            yield return new WaitForSeconds(0.2f);
            titleImage.sprite = titleSprite2;
        }
    }
}
