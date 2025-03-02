using System.Collections.Generic;
using UnityEngine;

public class CommandmentsGameManager : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject objectPrefab;
    public float duration = 5.0f;
    public float spawnFrequency = 1.0f; // Frequency in seconds
    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    private float spawnTimer = 0.0f;

    void Start()
    {
        // Initial spawn
        SpawnObject();
    }

    void Update()
    {
        // Handle spawning based on frequency
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnFrequency)
        {
            SpawnObject();
            spawnTimer = 0.0f;
        }

        // Move objects
        if (objectQueue.Count > 0)
        {
            GameObject obj = objectQueue.Peek();
            float step = Time.deltaTime / duration;
            obj.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, step);

            if (Vector3.Distance(obj.transform.position, endPoint.position) < 0.01f)
            {
                Destroy(objectQueue.Dequeue());
            }
        }
    }

    void SpawnObject()
    {
        GameObject obj = Instantiate(objectPrefab, startPoint.position, Quaternion.identity);
        objectQueue.Enqueue(obj);
    }
}
