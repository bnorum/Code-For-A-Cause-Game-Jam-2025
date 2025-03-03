using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class DateTimeManager : MonoBehaviour
{

    public static DateTimeManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public TextMeshProUGUI dateTimeText;

    public string time;

    public string date;

    public List<string> dateList;

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

    void DisplayDateTime() {
        if (GameManager.Instance.time > 720) dateTimeText.text = date +"	" + time + " PM";
        else dateTimeText.text = date + "	" + time + " AM";

    }
}
