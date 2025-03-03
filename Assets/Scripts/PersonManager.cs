using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public List<PersonSchema> personSchemas = new List<PersonSchema>();
    public GameObject physicalPerson;
    public float duration = 5.0f;
    public float spawnFrequency = 1.0f;
    public Collider2D personBounds;
    
    private float spawnTimer = 0.0f;
    public Transform peopleScreen2D;
    public static PersonManager Instance { get; private set; } //SINGLETON!!!!!!!!!!!!!!
    
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
    void Start()
    {
        SpawnObject();
        personSchemas = GameManager.Instance.chosenPeople;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnFrequency)
        {
            SpawnObject();
            spawnTimer = 0.0f;
        }
    }

    void SpawnObject()
    {
        if (personSchemas.Count == 0) return;
        int index = Random.Range(0, personSchemas.Count);
        GameObject obj = Instantiate(physicalPerson, startPoint.position, Quaternion.identity, peopleScreen2D);
        Person personScript = obj.GetComponent<Person>();

        if (personScript != null)
        {
            personScript.Init(personSchemas[index], personBounds);
            personScript.StartMovement(endPoint.position, duration);
        }
    }
}
