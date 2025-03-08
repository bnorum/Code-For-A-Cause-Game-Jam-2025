using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // SINGLETON

    public List<PersonSchema> allEmailPeople = new List<PersonSchema>();
    public List<PersonSchema> allParameterPeople = new List<PersonSchema>();
    
    public List<PersonSchema> chosenEmailPeople = new List<PersonSchema>();
    public List<PersonSchema> chosenParameterPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();

    public List<PersonSchema> day5People = new List<PersonSchema>();

    public float time = 540f;
    public int datenum;
    public int chosenEmailCount;
    public int chosenParameterCount;
    public float timeUntilNextEmail = 5f;
    public float timeUntilNextCoworker = 60f;
    public float timeUntilNextEmailPerson = 0f;
    public float timeUntilNextParameterPerson = 0f;
    public float spawnEmailInterval;
    public float spawnParameterInterval;

    public bool isPaused = false;
    public float difficultyScale = 1f;

    public Transform startPoint;
    public Transform endPoint;
    public GameObject physicalPerson;
    public float escalatorTravelDuration = 5.0f;
    public BoxCollider2D personBounds;
    [SerializeField] private int totalEmailSpawns;
    [SerializeField] private int totalParameterSpawns;
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
    public int currentEmailPersonSpawned=0;
    public int currentParameterPersonSpawned=0;
    public AudioSource plop;
    public AudioSource succeeded;
    public AudioSource missed;


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
        PersistentData.peopleShouldveDamnedToday = new List<PersonSchema>();
        PersistentData.peopleShouldveSavedToday = new List<PersonSchema>();
        PersistentData.peopleDamnedToday = new List<PersonSchema>();
        PersistentData.peopleSavedToday = new List<PersonSchema>();

        endDayCanvas.SetActive(false);
        clockOutCanvas.SetActive(false);
        clockOutScreenDarken.color = new Color(0, 0, 0, 0);


        if (Application.isEditor && PersistentData.currentDay == 0)
        {
            PersistentData.currentDay = 1;
        }

        if (PersistentData.currentDay == 1)
        {
            PersistentData.remainingEmailPeople = allEmailPeople;
            PersistentData.remainingParameterPeople = allParameterPeople;
        }
        allEmailPeople = PersistentData.remainingEmailPeople;
        allParameterPeople = PersistentData.remainingParameterPeople;
        datenum = PersistentData.currentDay;
        ChoosePeople();

        foreach (PersonSchema  ps in chosenEmailPeople) {
            if (ps.shouldGoToHeaven) {
                PersistentData.peopleShouldveSavedToday.Add(ps);
            } else {
                PersistentData.peopleShouldveDamnedToday.Add(ps);
            }
        }
        foreach (PersonSchema  person in chosenParameterPeople) {
            if (person.shouldGoToHeaven) {
                PersistentData.peopleShouldveSavedToday.Add(person);
            } else {
                PersistentData.peopleShouldveSavedToday.Add(person);
            }
        
        }

        RefreshEmailsUsed();
        totalEmailSpawns = chosenEmailPeople.Count;
        totalParameterSpawns = chosenParameterPeople.Count;
        difficultyScale = PersistentData.difficultyScale;


        if (PersistentData.currentDay == 5) //BOSS TIME!
        {
            spawnEmailInterval = 20f;
            escalatorTravelDuration = 380f;
            SendQuadrupletsEmail();
        }
        // Ensure at least one spawn
        SpawnEmailPerson();
        timeUntilNextEmailPerson = spawnEmailInterval;
        totalEmailSpawns--;
    }

    void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime * difficultyScale;
            timeUntilNextEmail -= Time.deltaTime * difficultyScale;
            timeUntilNextCoworker -= Time.deltaTime * difficultyScale;
            timeUntilNextEmailPerson -= Time.deltaTime * difficultyScale;
            timeUntilNextParameterPerson -=Time.deltaTime * difficultyScale;
            // DateTimeManager.Instance.UpdateProgress(chosenPeople.Count);
            // Check if it's time to spawn the next person
            if (timeUntilNextEmailPerson < 0 && totalEmailSpawns >= 1)
            {
                SpawnEmailPerson();
                timeUntilNextEmailPerson = spawnEmailInterval;
                totalEmailSpawns--;
            }
            if (timeUntilNextParameterPerson < 0 && totalParameterSpawns >= 1)
            {
                SpawnParameterPerson();
                timeUntilNextParameterPerson = spawnParameterInterval;
                totalParameterSpawns--;
            }
        }

        if (time >= endTime - 30 && !isDayOver)
        {
            isDayOver = true;
            // Call the function to end the day
            RefreshEmailsUsed();
            StartCoroutine(EndDay());
        }

        if (timeUntilNextEmail <= 0)
        {
            timeUntilNextEmail = UnityEngine.Random.Range(24, 28);
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
            chosenEmailPeople = day5People;
            for (int i = 0; i < chosenParameterCount; i++)
            {
                if (allParameterPeople.Count == 0) break;
                int index = UnityEngine.Random.Range(0, allParameterPeople.Count);
                PersonSchema selectedPerson = allParameterPeople[index];
                chosenParameterPeople.Add(selectedPerson);
                allParameterPeople.RemoveAt(index);
                PersistentData.remainingParameterPeople.Remove(selectedPerson);
            }
            return;
        }
        for (int i = 0; i < chosenEmailCount; i++)
        {
            if (allEmailPeople.Count == 0) break;
            int index = UnityEngine.Random.Range(0, allEmailPeople.Count);
            PersonSchema selectedPerson = allEmailPeople[index];
            chosenEmailPeople.Add(selectedPerson);
            allEmailPeople.RemoveAt(index);
            PersistentData.remainingEmailPeople.Remove(selectedPerson);
        }
        for (int i = 0; i < chosenParameterCount; i++)
        {
            if (allParameterPeople.Count == 0) break;
            int index = UnityEngine.Random.Range(0, allParameterPeople.Count);
            PersonSchema selectedPerson = allParameterPeople[index];
            chosenParameterPeople.Add(selectedPerson);
            allParameterPeople.RemoveAt(index);
            PersistentData.remainingParameterPeople.Remove(selectedPerson);
        }
    }

    public List<EmailSchema> GetAllEmailSchemas()
    {
        List<EmailSchema> allEmailSchemas = new List<EmailSchema>();
        foreach (PersonSchema person in chosenEmailPeople)
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
        if (time is > 540 and < 650)
        {
            return SelectEmailFromPool(0);
        }
        else if (time is > 650 and < 760 )
        {
            return SelectEmailFromPool(1);
        }
        else if (time is > 760 and < 870)
        {
            return SelectEmailFromPool(2);
        }
        else if (time is > 870 and < 980)
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

    void SpawnEmailPerson()
    {
        if (chosenParameterPeople.Count == 0) return;

        int index = currentEmailPersonSpawned;
        currentEmailPersonSpawned++;
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, escalatorWindowPersonHolder);
        Person personScript = obj.GetComponent<Person>();
        personScript.Init(chosenEmailPeople[index], personBounds, startPoint.gameObject, endPoint.gameObject);
        personScript.StartMovement(escalatorTravelDuration);
    }
    void SpawnParameterPerson()
    {
        if(chosenParameterPeople.Count == 0) return;

        int index = currentParameterPersonSpawned;
        currentParameterPersonSpawned++;
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, escalatorWindowPersonHolder);
        Person personScript = obj.GetComponent<Person>();
        plop.Play();
        personScript.Init(chosenParameterPeople[index], personBounds, startPoint.gameObject, endPoint.gameObject);
        personScript.StartMovement(escalatorTravelDuration);
        Debug.Log("Spawned Person");
    }

    EmailSchema SelectEmailFromPool(int personIndex) {
        if (!chosenEmailPeople[personIndex].relatedEmailsUsed.Contains(false))
        {
            return null;
        }
        int index = UnityEngine.Random.Range(0, chosenEmailPeople[personIndex].relatedEmails.Count);
        while (chosenEmailPeople[personIndex].relatedEmailsUsed[index] && chosenEmailPeople[personIndex].relatedEmailsUsed.Contains(false))
        {
            index = UnityEngine.Random.Range(0, chosenEmailPeople[personIndex].relatedEmails.Count);
        }

        chosenEmailPeople[personIndex].relatedEmailsUsed[index] = true;
        return chosenEmailPeople[personIndex].relatedEmails[index];
    }

    public void RefreshEmailsUsed()
    {
        Debug.Log("Refreshing Emails");
        foreach (PersonSchema person in chosenEmailPeople)
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
    public IEnumerator DeterminedCorrectly(bool didSucceed)
    {
        yield return new WaitForSeconds(1f);
        if(didSucceed) succeeded.Play();
        else missed.Play();
    }
}
