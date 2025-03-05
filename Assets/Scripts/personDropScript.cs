using UnityEngine;

public class personDropScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Person>() == null) return;

        Person myPerson = collision.gameObject.GetComponent<Person>();
        if (!myPerson.isBeingTransported && !myPerson.isDragging && myPerson.isFalling && myPerson.rb.linearVelocity.magnitude < myPerson.escalatorThresholdToTrigger)
        {
            myPerson.RestartMovement();
        }
    }
}
