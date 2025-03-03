using UnityEngine;

public class TabCollider : MonoBehaviour
{
    public GameObject tabToOpen;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Person" && collision.gameObject.GetComponent<Person>().isDragging)
            FindFirstObjectByType<TabManager>().OpenTab(tabToOpen, collision.gameObject.GetComponent<Person>());
    }
}
