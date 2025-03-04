using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Slider difficultySlider;
    public TextMeshProUGUI difficultyText;

    public GameObject StartMenu;
    public GameObject DifficultyMenu;

    public Transform targetPosition;
    public Transform hidePosition;

    public bool isDifficultyShown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        PersistentData.difficultyScale = difficultySlider.value;
        difficultyText.text = Mathf.RoundToInt(difficultySlider.value * 100f - 50) + "%";

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
}
