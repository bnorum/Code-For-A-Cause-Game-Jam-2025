using UnityEngine;

public class personDropScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var person = collision.gameObject.GetComponent<Person>();
        if (collision.CompareTag("Person") && !person.isBeingTransported && !person.isDragging && person.isFalling)
        {
            collision.gameObject.GetComponent<Person>().RestartMovement();
        }
    }
}
