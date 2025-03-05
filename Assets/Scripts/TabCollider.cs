using UnityEngine;

public class TabCollider : MonoBehaviour
{
    public GameObject tabToOpen;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Person>())
        {
            Person myPerson = collision.gameObject.GetComponent<Person>();

            if (myPerson.isDragging && !myPerson.isFalling)
            {
                FindFirstObjectByType<TabManager>().OpenTab(tabToOpen, myPerson);
            }
        }
    }


}
