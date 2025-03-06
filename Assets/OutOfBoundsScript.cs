using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class OutOfBoundsScript : MonoBehaviour
{
    public static OutOfBoundsScript Instance { get; private set; }
    public List<GameObject> alivePeople = new List<GameObject>();
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

    void Update()
    {
        StartCoroutine(CheckOutOfBounds());
    }

    private IEnumerator CheckOutOfBounds()
    {
        while (true)
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
                    StartCoroutine(OutOfBoundsTimer(obj));
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator OutOfBoundsTimer(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        bool isStillOutOfBounds = true;
        foreach (var collider in new BoxCollider2D[] { boundaryCollider1, boundaryCollider2, boundaryCollider3, boundaryCollider4, boundaryCollider5 })
        {
            if (collider.bounds.Contains(obj.transform.position))
            {
                isStillOutOfBounds = false;
                break;
            }
        }
        if (isStillOutOfBounds)
        {
            obj.GetComponent<Person>().ResetToDefault();
            obj.transform.SetParent(limboPeopleHolder.transform);
            obj.transform.position = respawn.position;
        }
    }


    public void UpdateAlivePeople(GameObject obj)
    {
        if (alivePeople.Contains(obj))
        {
            alivePeople.Remove(obj);
        }
        else
        {
            alivePeople.Add(obj);
        }
    }
}
