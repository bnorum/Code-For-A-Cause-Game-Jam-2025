using UnityEngine;

public class heavenCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            PersistentData.peopleSaved.Add(collision.gameObject.GetComponent<Person>().personSchema);
            if (collision.gameObject.GetComponent<Person>().personSchema.shouldGoToHeaven) {
                PersistentData.peopleDeterminedCorrectly++;
            }
            PersistentData.peopleSavedToday.Add(collision.gameObject.GetComponent<Person>().personSchema);
            OutOfBoundsScript.Instance.UpdateAlivePeople(collision.gameObject);
            collision.gameObject.GetComponent<Person>().GetBonuses(false);
            Destroy(collision.gameObject);
            Destroy(collision.gameObject.GetComponent<Person>().startPointGameRef);
            Destroy(collision.gameObject.GetComponent<Person>().endPointGameRef);

        }
    }
}
