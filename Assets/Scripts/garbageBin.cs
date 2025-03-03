using UnityEngine;

public class garbageBin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person" && gameObject.transform.position.z == collision.gameObject.transform.position.z)
        {
            PersistentData.peopleDamned.Add(collision.gameObject.GetComponent<Person>().personSchema);
            Destroy(collision.gameObject);

        }
    }
}
