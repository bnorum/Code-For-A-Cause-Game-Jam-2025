using UnityEngine;
using System.Collections.Generic;

public class OutOfBoundsScript : MonoBehaviour
{
    public static OutOfBoundsScript Instance { get; private set; }
    private List<GameObject> alivePeople = new List<GameObject>();
    public BoxCollider2D boundaryCollider1;
    public BoxCollider2D boundaryCollider2;
    public BoxCollider2D boundaryCollider3;
    public BoxCollider2D boundaryCollider4;
    public BoxCollider2D boundaryCollider5;
    public Transform respawn;
    public GameObject limboPeopleHolder;

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
    }

    void Update()
    {
        foreach (var obj in alivePeople)
        {
            bool isWithinBounds = false;
            foreach (var collider in new BoxCollider2D[] { boundaryCollider1, boundaryCollider2, boundaryCollider3, boundaryCollider4, boundaryCollider5 })
            {
            if (collider.bounds.Contains(obj.transform.position))
            {
                isWithinBounds = true;
                break;
            }
            }

            if (!isWithinBounds)
            {
            obj.transform.position = respawn.position;
            obj.GetComponent<Person>().ResetToDefault();
            obj.transform.SetParent(limboPeopleHolder.transform);
            }
        }
    }

    public void UpdateAlivePeople(GameObject obj)
    {
        if (!alivePeople.Contains(obj))
            alivePeople.Add(obj);
        else
            alivePeople.Remove(obj);
    }
}
