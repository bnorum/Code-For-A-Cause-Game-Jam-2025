using System.Collections.Generic;
using UnityEngine;

public class CommandmentsGameManager : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public List<GameObject> objectPrefabs = new List<GameObject>();
    public float duration = 5.0f;
    public float spawnFrequency = 1.0f; // Frequency in seconds
    private float spawnTimer = 0.0f;

    void Start()
    {
        SpawnObject(); // Initial spawn
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
        if (objectPrefabs.Count == 0) return;
        int index = Random.Range(0, objectPrefabs.Count);

        GameObject obj = Instantiate(objectPrefabs[index], startPoint.position, Quaternion.identity);
        
        Person personScript = obj.GetComponent<Person>();

        if (personScript != null)
        {
            personScript.StartMovement(endPoint.position, duration); // Move person to endPoint
        }
        else
        {
            Debug.LogError("Spawned object is missing Person script!");
        }
    }
}
