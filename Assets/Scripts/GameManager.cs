using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public List<PersonSchema> allPeople = new List<PersonSchema>();
    private List<PersonSchema> chosenPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();

    public float time = 540f;
    public int datenum = 1;

    public float timeUntilNextEmail = 5f;

    public bool isPaused = false;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) {
            time += Time.deltaTime;
            timeUntilNextEmail -= Time.deltaTime;
        }
        if (time >= 1440f)
        {
            //do some sort of cutscene, then load next day
        }

        if (timeUntilNextEmail <= 0)

        {
            timeUntilNextEmail = UnityEngine.Random.Range(15, 35);
            int spamChance = UnityEngine.Random.Range(0, 100);
            if (spamChance < 10)
            {
                EmailManager.Instance.AddEmail(GetRandomSpamEmail());
            }
            else
            {
                //worst line EVER!
                EmailManager.Instance.AddEmail(EmailManager.Instance.emailSchemas[UnityEngine.Random.Range(0, EmailManager.Instance.emailSchemas.Count)]);
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
        return ((int)time / 60 % 12).ToString("00") + ":" + ((int)time % 60).ToString("00");
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
}
