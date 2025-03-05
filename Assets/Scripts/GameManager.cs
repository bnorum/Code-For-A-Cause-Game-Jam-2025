using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // SINGLETON

    public List<PersonSchema> allPeople = new List<PersonSchema>();
    public List<PersonSchema> chosenPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();

    public float time = 540f;
    public int datenum;

    public float timeUntilNextEmail = 5f;
    public float timeUntilNextCoworker = 120f;
      public float timeUntilNextPerson = 0f; // debugging, doesnt matter

    public bool isPaused = false;
    public float difficultyScale = 1f;

    public Transform startPoint;
    public Transform endPoint;
    public GameObject physicalPerson;
    public float escalatorTravelDuration = 5.0f;
    public Collider2D personBounds;
    private float spawnTimer = 0.0f;
    private float spawnInterval;
    [SerializeField] private float reservedEndTime;
    private int totalSpawns;
    private float nextSpawnTime;
    public Transform escalatorWindowPersonHolder;
    [SerializeField] private float endTime = 1440f;

    public GameObject endDayCanvas;
    public bool isDayOver = false;

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
        datenum = PersistentData.currentDay;
        ChoosePeople(20);
        totalSpawns = chosenPeople.Count;

        difficultyScale = PersistentData.difficultyScale;
        // Ensure at least one spawn
        if (totalSpawns > 0)
        {
            spawnInterval = (endTime - time - reservedEndTime) / (totalSpawns - 1);
            nextSpawnTime = time; // First spawn happens immediately
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime * difficultyScale;
            timeUntilNextEmail -= Time.deltaTime * difficultyScale;
            timeUntilNextCoworker -= Time.deltaTime * difficultyScale;
            timeUntilNextPerson = Mathf.Max(0, nextSpawnTime - time);
            DateTimeManager.Instance.UpdateProgress(chosenPeople.Count);
            // Check if it's time to spawn the next person
            if (time >= nextSpawnTime && totalSpawns > 1)
            {
                SpawnPerson();
                nextSpawnTime = time + spawnInterval; // Schedule the next spawn
                totalSpawns--;
            }
        }

        if (time >= endTime + 10 && !isDayOver)
        {
            isDayOver = true;
            // Call the function to end the day
            SceneManager.Instance.LoadBetweenDay();
        }
        {
            // End of day logic (e.g., cutscene, transition to next day)
        }

        if (timeUntilNextEmail <= 0)
        {
            timeUntilNextEmail = UnityEngine.Random.Range(20, 27);
            int spamChance = UnityEngine.Random.Range(0, 100);
            if (spamChance < 7)
            {
                EmailManager.Instance.AddEmail(GetRandomSpamEmail());
            }
            else if(EmailManager.Instance.emailSchemas.Count > 0)
            {
                EmailManager.Instance.AddEmail(EmailManager.Instance.emailSchemas[UnityEngine.Random.Range(0, EmailManager.Instance.emailSchemas.Count-1)]);
            }
        }

        if (timeUntilNextCoworker <= 0 && CoworkerManager.Instance.isActive == false)
        {
            timeUntilNextCoworker = UnityEngine.Random.Range(120, 180);
            CoworkerManager.Instance.SummonCoworker();
        }
    }

    void ChoosePeople(int cumulativeChallengeLevel)
    {
        for (int i = 0; i < allPeople.Count; i++)
        {
            PersonSchema temp = allPeople[i];
            int randomIndex = UnityEngine.Random.Range(i, allPeople.Count);
            allPeople[i] = allPeople[randomIndex];
            allPeople[randomIndex] = temp;
        }

        int currentChallengeLevel = 0;
        foreach (PersonSchema person in allPeople)
        {
            if (person.challengeLevel + currentChallengeLevel <= cumulativeChallengeLevel)
            {
                currentChallengeLevel += person.challengeLevel;
                chosenPeople.Add(person);
            }

            if (chosenPeople.Count == 4)
            {
                break;
            }
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

        int index = UnityEngine.Random.Range(0, chosenPeople.Count);
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, escalatorWindowPersonHolder);
        Person personScript = obj.GetComponent<Person>();
        personScript.Init(chosenPeople[index], personBounds, startPoint.gameObject, endPoint.gameObject);
        personScript.StartMovement(escalatorTravelDuration);
        Debug.Log("Spawned Person");
    }
}
