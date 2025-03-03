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

    void Start()
    {
        SpawnObject();
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
