using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class DateTimeManager : MonoBehaviour
{

    public static DateTimeManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public TextMeshProUGUI dateTimeText;

    public RectTransform damnedProgressBar;
    public RectTransform damnedProgressBarFill;
    public RectTransform savedProgressBar;
    public RectTransform savedProgressBarFill;
    private string time;
    private string date;

    public List<string> dateList;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dateList = new List<string> {"Monday the 9th", "Tuesday the 10th", "Wednesday the 11th", "Thursday the 12th", "Friday the 13th"};
    }

    // Update is called once per frame
    void Update()
    {
        time = GameManager.Instance.TimeToString();
        date = dateList[GameManager.Instance.datenum - 1];

        DisplayDateTime();
    }

    void DisplayDateTime()
    {
        if (GameManager.Instance.time > 720) dateTimeText.text = date +"	" + time + " PM";
        else dateTimeText.text = date + "	" + time + " AM";

    }
    public void UpdateProgress(int total)
    {
        int peopleDamned = PersistentData.peopleDamned.Count;
        int peopleSaved = PersistentData.peopleSaved.Count;

        float damnedProgress = (float)peopleDamned / total;
        float savedProgress = (float)peopleSaved / total;

        if (damnedProgressBar != null)
        damnedProgressBarFill.localScale = new Vector3(1, damnedProgress, 1);

        if (savedProgressBar != null)
        savedProgressBarFill.localScale = new Vector3(1, savedProgress, 1);
    }
}
