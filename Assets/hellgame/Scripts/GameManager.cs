using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!

    public List<PersonSchema> allPeople = new List<PersonSchema>();
    private List<PersonSchema> chosenPeople = new List<PersonSchema>();
    public List<EmailSchema> spamEmails = new List<EmailSchema>();


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
        ChoosePeople(10);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ChoosePeople(int cumulativeChallengeLevel) {

        // Randomize the allPeople list
        for (int i = 0; i < allPeople.Count; i++)
        {
            PersonSchema temp = allPeople[i];
            int randomIndex = Random.Range(i, allPeople.Count);
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
        return spamEmails[Random.Range(0, spamEmails.Count)];
    }
}
