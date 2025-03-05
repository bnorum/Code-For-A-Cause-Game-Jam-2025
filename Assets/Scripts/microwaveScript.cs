using UnityEngine;
using System.Collections;

public class microwaveScript : MonoBehaviour
{
    public float microwaveTime = 5.0f;
    public float vibrationIntensity = 0.1f;
    public float vibrationSpeed = 20.0f;
    private bool isMicrowaving = false;
    public GameObject childSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;

    void Start()
    {
        if (childSpriteRenderer != null)
        {
            childSpriteRenderer.SetActive(false); 
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Person")&& !isMicrowaving && collision.gameObject.GetComponent<Person>())
        {
            if(collision.gameObject.GetComponent<Person>().hasBeenMicrowaved)
            {
                SearchManager.Instance.DisplayProfile(collision.GetComponent<Person>());
            }
            else
            {
                originalPosition = transform.localPosition;
                childSpriteRenderer.SetActive(true);
                spriteRenderer.enabled = false;
                collision.gameObject.GetComponent<Person>().isMicrowaving = true;
                microwaveCoroutine = StartCoroutine(StartMicrowaveTimer(collision.gameObject));
            }
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Person") && collision.gameObject.GetComponent<Person>())
        {
            if (microwaveCoroutine != null)
            {
                StopCoroutine(microwaveCoroutine);
                microwaveCoroutine = null;
            }
            childSpriteRenderer.SetActive(false);
            isMicrowaving = false;
            collision.gameObject.GetComponent<Person>().isMicrowaving = false;
            transform.localPosition = originalPosition;
            spriteRenderer.enabled = true;
        }
    }
    private Coroutine microwaveCoroutine;
    IEnumerator StartMicrowaveTimer(GameObject person)
    {
        float elapsedTime = 0f;

        while (elapsedTime < microwaveTime)
        {
            transform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * vibrationIntensity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        childSpriteRenderer.SetActive(false);
        isMicrowaving = false;
        person.GetComponent<Person>().isMicrowaving = false;
        person.GetComponent<Person>().hasBeenMicrowaved = true;
        transform.localPosition = originalPosition;
        spriteRenderer.enabled = true;
        SearchManager.Instance.DisplayProfile(person.GetComponent<Person>());
    }
}
