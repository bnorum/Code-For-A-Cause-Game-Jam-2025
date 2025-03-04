using UnityEngine;
using System.Collections;

public class microwaveScript : MonoBehaviour
{
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    public float microwaveTime = 5.0f;
    public float vibrationIntensity = 0.1f;
    public float vibrationSpeed = 20.0f;
    private bool isMicrowaving = false;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    public GameObject childSpriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        if (childSpriteRenderer != null)
        {
            childSpriteRenderer.SetActive(false); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person") && spriteRenderer != null && !isMicrowaving)
        {
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.SetActive(true);
                spriteRenderer.sprite = null;
            }
            StartCoroutine(StartMicrowaveTimer(other.gameObject));
        }
    }
    void OTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Person") && isMicrowaving)
        {
            StopAllCoroutines();
            transform.localPosition = originalPosition;
            transform.localScale = originalScale;
            spriteRenderer.sprite = originalSprite;
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.SetActive(false);
            }
            isMicrowaving = false;
        }

    }

    IEnumerator StartMicrowaveTimer(GameObject person)
    {
        isMicrowaving = true;
        float elapsedTime = 0f;

        while (elapsedTime < microwaveTime)
        {
            transform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * vibrationIntensity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        transform.localScale = originalScale;
        spriteRenderer.sprite = originalSprite;
        if (childSpriteRenderer != null)
        {
            childSpriteRenderer.SetActive(false);
        }
        isMicrowaving = false;
        StopAllCoroutines();
        SearchManager.Instance.DisplayProfile(person.GetComponent<Person>());
    }
}
