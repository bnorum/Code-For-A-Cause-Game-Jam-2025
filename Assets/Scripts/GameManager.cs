using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // SINGLETON

    public List<PersonSchema> allPeople = new List<PersonSchema>();
    public List<PersonSchema> chosenPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();

    public List<PersonSchema> day5People = new List<PersonSchema>();

    public float time = 540f;
    public int datenum;

    public float timeUntilNextEmail = 5f;
    public float timeUntilNextCoworker = 60f;
    public float timeUntilNextPerson = 0f; // debugging, doesnt matter //brady: SIKE!!!!!!! MATTERS NOW! //yippee

    public bool isPaused = false;
    public float difficultyScale = 1f;

    public Transform startPoint;
    public Transform endPoint;
    public GameObject physicalPerson;
    public float escalatorTravelDuration = 5.0f;
    public BoxCollider2D personBounds;
    public float spawnInterval;
    [SerializeField] private float reservedEndTime;
    private int totalSpawns;
    private float nextSpawnTime;
    public Transform escalatorWindowPersonHolder;
    [SerializeField] private float endTime = 1020f;

    public GameObject endDayCanvas;
    public GameObject clockOutCanvas;
    public GameObject clockOutMachine;
    public GameObject clockOutMachineTarget;
    public UnityEngine.UI.Image clockOutScreenDarken;
    public EmailSchema clockoutEmail;
    public bool isDayOver = false;

    public EmailSchema quadrupletsEmail;
    public int currentPersonSpawned=0;

    //HACK: I fucked up big time and have to use these four arrays. They tell if an email has been sent yet

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {

        endDayCanvas.SetActive(false);
        clockOutCanvas.SetActive(false);
        clockOutScreenDarken.color = new Color(0, 0, 0, 0);


        if (Application.isEditor && PersistentData.currentDay == 0)
        {
            PersistentData.currentDay = 1;
        }

        if (PersistentData.currentDay == 1)
        {
            PersistentData.remainingPeople = allPeople;
        }
        allPeople = PersistentData.remainingPeople;
        datenum = PersistentData.currentDay;
        ChoosePeople();

        RefreshEmailsUsed();
        totalSpawns = chosenPeople.Count;
        difficultyScale = PersistentData.difficultyScale;


        if (PersistentData.currentDay == 5) //BOSS TIME!
        {
            spawnInterval = 20f;
            escalatorTravelDuration = 380f;
            SendQuadrupletsEmail();
        }
        // Ensure at least one spawn
        SpawnPerson();
        timeUntilNextPerson = spawnInterval;
        totalSpawns--;
    }

    void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime * difficultyScale;
            timeUntilNextEmail -= Time.deltaTime * difficultyScale;
            timeUntilNextCoworker -= Time.deltaTime * difficultyScale;
            timeUntilNextPerson -= Time.deltaTime * difficultyScale;
            // DateTimeManager.Instance.UpdateProgress(chosenPeople.Count);
            // Check if it's time to spawn the next person
            if (timeUntilNextPerson < 0 && totalSpawns > 1)
            {
                SpawnPerson();
                timeUntilNextPerson = spawnInterval;
                totalSpawns--;
            }
        }

        if (time >= endTime - 30 && !isDayOver)
        {
            isDayOver = true;
            // Call the function to end the day
            RefreshEmailsUsed();
            StartCoroutine(EndDay());
        }
        {
            // End of day logic (e.g., cutscene, transition to next day)
        }

        if (timeUntilNextEmail <= 0)
        {
            timeUntilNextEmail = UnityEngine.Random.Range(15, 20);
            int spamChance = UnityEngine.Random.Range(0, 100);
            if (spamChance < 7)
            {
                EmailManager.Instance.AddEmail(GetRandomSpamEmail());
            }
            else if(EmailManager.Instance.emailSchemas.Count > 0)
            {
                EmailSchema email = GetPseudoRandomEmail();
                if (email != null)
                {
                    EmailManager.Instance.AddEmail(email);
                }
            }
        }

        if (timeUntilNextCoworker <= 0 && CoworkerManager.Instance.isActive == false)
        {
            timeUntilNextCoworker = UnityEngine.Random.Range(110, 130);
            CoworkerManager.Instance.SummonCoworker();
        }
    }

    void ChoosePeople()
    {
        if (PersistentData.currentDay == 5) {
            chosenPeople = day5People;
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            if (allPeople.Count == 0) break;
            int index = UnityEngine.Random.Range(0, allPeople.Count);
            PersonSchema selectedPerson = allPeople[index];
            chosenPeople.Add(selectedPerson);
            allPeople.RemoveAt(index);
            PersistentData.remainingPeople.Remove(selectedPerson);
        }

    }

    public List<EmailSchema> GetAllEmailSchemas()
    {
        List<EmailSchema> allEmailSchemas = new List<EmailSchema>();
        foreach (PersonSchema person in chosenPeople)
        {
            allEmailSchemas.AddRange(person.relatedEmails);
        }
        return allEmailSchemas;
    }

    public EmailSchema GetRandomSpamEmail()
    {
        return spamEmails[UnityEngine.Random.Range(0, spamEmails.Count)];
    }

    public EmailSchema GetPseudoRandomEmail() {
        if (time is > 540 and < 620 && chosenPeople[0].relatedEmails.Count > 0 )
        {
            return SelectEmailFromPool(0);
        }
        else if (time is > 620 and < 700 )
        {
            if (UnityEngine.Random.Range(0, 100) < 70 && chosenPeople[0].relatedEmails.Count > 0) {
                return SelectEmailFromPool(0);
            }
            else
            {
                return SelectEmailFromPool(1);
            }
        }
        else if (time is > 700 and < 780)
        {
            if (UnityEngine.Random.Range(0, 100) < 70 && chosenPeople[1].relatedEmails.Count > 0) {
                return SelectEmailFromPool(1);
            }
            else
            {
                return SelectEmailFromPool(2);
            }
        }
        else if (time is > 860 and < 940)
        {
            if (UnityEngine.Random.Range(0, 100) < 70 && chosenPeople[2].relatedEmails.Count > 0) {
                return SelectEmailFromPool(2);
            }
            else
            {
                return SelectEmailFromPool(3);
            }

        }
        else if (time is > 940 and < 1020 && chosenPeople[3].relatedEmails.Count > 0)
        {
            return SelectEmailFromPool(3);
        }
        else
        {
            return GetRandomSpamEmail();
        }

    }

    public string TimeToString()
    {
        if (time > 780f)
        {
            return ((int)time / 60 % 12).ToString("00") + ":" + ((int)time % 60).ToString("00");
        }
        else
        {
            return ((int)time / 60).ToString("00") + ":" + ((int)time % 60).ToString("00");
        }
    }

    public void DecideFateOfPerson(PersonSchema person, bool save)
    {
        if (save)
        {
            PersistentData.peopleSaved.Add(person);
        }
        else
        {
            PersistentData.peopleDamned.Add(person);
        }
    }

    void SpawnPerson()
    {
        if (chosenPeople.Count == 0) return;

        int index = currentPersonSpawned;
        currentPersonSpawned++;
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, escalatorWindowPersonHolder);
        Person personScript = obj.GetComponent<Person>();
        personScript.Init(chosenPeople[index], personBounds, startPoint.gameObject, endPoint.gameObject);
        personScript.StartMovement(escalatorTravelDuration);
        Debug.Log("Spawned Person");
    }

    EmailSchema SelectEmailFromPool(int personIndex) {
        if (!chosenPeople[personIndex].relatedEmailsUsed.Contains(false))
        {
            return null;
        }
        int index = UnityEngine.Random.Range(0, chosenPeople[personIndex].relatedEmails.Count);
        while (chosenPeople[personIndex].relatedEmailsUsed[index] && chosenPeople[personIndex].relatedEmailsUsed.Contains(false))
        {
            index = UnityEngine.Random.Range(0, chosenPeople[personIndex].relatedEmails.Count);
        }

        chosenPeople[personIndex].relatedEmailsUsed[index] = true;
        return chosenPeople[personIndex].relatedEmails[index];
    }

    public void RefreshEmailsUsed()
    {
        Debug.Log("Refreshing Emails");
        foreach (PersonSchema person in chosenPeople)
        {

            for (int i = 0; i < person.relatedEmailsUsed.Count; i++)
            {
                person.relatedEmailsUsed[i] = false;
            }

        }
    }

    void SendClockOutEmail() {
        EmailManager.Instance.AddEmail(clockoutEmail);
    }

    void SendQuadrupletsEmail() {
        EmailManager.Instance.AddEmail(quadrupletsEmail);
    }

    public System.Collections.IEnumerator EndDay() {
        SendClockOutEmail();
        yield return new WaitForSeconds(30);
        clockOutCanvas.SetActive(true);

        while (Vector3.Distance(clockOutMachine.transform.position, clockOutMachineTarget.transform.position) > 0.1f)
        {
            if (clockOutScreenDarken.color.a < 0.5f)
            {
                clockOutScreenDarken.color = new Color(0, 0, 0, clockOutScreenDarken.color.a + Time.deltaTime / 1.0f);
            }
            clockOutMachine.transform.position = Vector3.Lerp(clockOutMachine.transform.position, clockOutMachineTarget.transform.position, Time.deltaTime * 2.0f);
            yield return null;
        }

    }
}
