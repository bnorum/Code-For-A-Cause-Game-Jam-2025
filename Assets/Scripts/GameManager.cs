using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public List<PersonSchema> allPeople = new List<PersonSchema>();
    public List<PersonSchema> chosenPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();

    public float time = 540f;
    public int datenum = 1;

    public float timeUntilNextEmail = 5f;
    public float timeUntilNextCoworker = 120f;

    public bool isPaused = false;
    public float difficultyScale = 1f;

    public Transform startPoint;
    public Transform endPoint;
    public GameObject physicalPerson;
    public float duration = 5.0f;
    public float spawnFrequency = 1.0f;
    public Collider2D personBounds;
    private float spawnTimer = 0.0f;
    public Transform peopleScreen2D;



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
        ChoosePeople(20);
        spawnInterval = (1440f - reservedEndTime) / totalSpawns;
        nextSpawnTime = time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        void Update()
        {
            if (!isPaused)
            {
                time += Time.deltaTime * difficultyScale;
                timeUntilNextEmail -= Time.deltaTime * difficultyScale;
                timeUntilNextCoworker -= Time.deltaTime * difficultyScale;

                // Check if it's time to spawn a new person
                if (time >= nextSpawnTime && time < 1440f - reservedEndTime)
                {
                    SpawnPerson();
                    nextSpawnTime += spawnInterval; // Schedule the next spawn
                }
            }

            if (time >= 1440f)
            {
                // Do some sort of cutscene, then load next day
            }

            if (timeUntilNextEmail <= 0)
            {
                timeUntilNextEmail = UnityEngine.Random.Range(20, 27);
                int spamChance = UnityEngine.Random.Range(0, 100);
                if (spamChance < 7)
                {
                    EmailManager.Instance.AddEmail(GetRandomSpamEmail());
                }
                else
                {//oh god
                    EmailManager.Instance.AddEmail(EmailManager.Instance.emailSchemas[UnityEngine.Random.Range(0, EmailManager.Instance.emailSchemas.Count)]);
                }
            }

            if (timeUntilNextCoworker <= 0 && CoworkerManager.Instance.isActive == false)
            {
                timeUntilNextCoworker = UnityEngine.Random.Range(120, 180);
                CoworkerManager.Instance.SummonCoworker();
            }
        }
    }


    void ChoosePeople(int cumulativeChallengeLevel) {

        // Randomize the allPeople list
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

    public List<EmailSchema> GetAllEmailSchemas() {
        List<EmailSchema> allEmailSchemas = new List<EmailSchema>();
        foreach (PersonSchema person in chosenPeople)
        {
            allEmailSchemas.AddRange(person.relatedEmails);
        }
        return allEmailSchemas;
    }

    public EmailSchema GetRandomSpamEmail() {
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
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, peopleScreen2D);
        Person personScript = obj.GetComponent<Person>();
        personScript.Init(chosenPeople[index], personBounds);
        personScript.StartMovement(endPoint.position, duration);
    }

}
