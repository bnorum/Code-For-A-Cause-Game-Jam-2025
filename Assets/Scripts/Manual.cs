using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public static Manual Instance { get; private set; }

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

    private Vector3 startPosition;
    public Transform shownPosition;
    public bool isShown = false;

    public TextMeshProUGUI manualTextBox;
    public List<string> manualText;
    public int pagenumber = 0;
    public TextMeshProUGUI pageNumberText;

    public GameObject notesPage;
    public bool notesShown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

        // manualText = new List<string> { "Employee Service Manual\n\nHell © 0 AD",
        //     "Congratulations, damned soul! \nYou’ve been selected for the prestigious role of Infernal Email Associate at Hell, LLC—The world’s premier soul-acquisition conglomerate. Your eternal tenure begins now.  \n\nThis handbook outlines your duties, expectations, and survival tips for navigating Hell’s bureaucracy. Failure to comply will result in immediate reassignment to the Eternal Spreadsheet Torture Division.",
        //     "Things to pay attention to:\n\n1. .gov emails are legit. Trust them with your life.\n2. The clock is ticking, you have until 5 PM to use your PC.\n3. Always spike your clients as hard as you can into Hell, we're a fast moving business.",
        //     "Commandments\n\nThese are our rules to the road. Every hear about someone lying? They go to hell. Stealing? Hell. Having another God than the one sending us emails? Strangely, straight to hell. The full list is below:\n\nNo lying.\nNo stealing.\nNo choosing other gods aside from God capital G.\nNo adultery.\nDo not exploit your neighbor.\nNo white after Labor Day (fashion sin).\nAnd no going out past midnight!",
        //     "Rotating Commandments: \n" + CommandmentsManager.Instance.DecideCommandments()
        //     };
                manualText = new List<string> {"Hell.gov - Purgatory Dept PC Operator's Manual",
            "Commandments\n\nAll People must pass these criteria. The full list is below:\n\nNo lying.\nNo stealing.\nNo choosing other gods aside from God capital G.\nNo adultery.\nDo not exploit your neighbor.",
            "Rotating Commandments: \n" + CommandmentsManager.Instance.DecideCommandments()
            };
    }

    // Update is called once per frame
    void Update()
    {
        if (isShown)
        {
            transform.position = Vector3.Lerp(transform.position, shownPosition.position, Time.deltaTime * 5f);
            transform.SetAsLastSibling();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }

        if (notesShown)
        {
            notesPage.transform.position = Vector3.Lerp(notesPage.transform.position, shownPosition.position, Time.deltaTime * 5f);
            notesPage.transform.SetAsLastSibling();
        }
        else
        {
            notesPage.transform.position = Vector3.Lerp(notesPage.transform.position, startPosition, Time.deltaTime * 5f);
        }

        pageNumberText.text = (pagenumber + 1) + "/" + manualText.Count;

    }

    public void ToggleShown() {
        isShown = !isShown;
        if (isShown) notesShown = false;
    }

    public void ShowNotesPage() {
        notesShown = !notesShown;
        if (notesShown) isShown = false;
    }

    public void SwitchPage(bool isForward) {
       if (isForward) {
            pagenumber++;
           if (pagenumber >= manualText.Count) {
               pagenumber = 0;
           }
       } else {
            pagenumber--;
            if (pagenumber < 0) {
                pagenumber = manualText.Count - 1;
           }
       }
       manualTextBox.text = manualText[pagenumber];
    }
}
