using System.Collections;
using UnityEngine;
using TMPro;

public class JuggleScript : MonoBehaviour
{
    public float juggleTimeThreshold = 1.5f;
    private int juggleCount = 0;
    private bool isJuggling = false;
    private float timeSinceLastFling = 0f;

    private Person person;
    public GameObject textPrefab;
    public Transform spawnPoint;

    private void Awake()
    {
        person = GetComponent<Person>();
    }

    private void Update()
    {
        if (!person.isDragging && person.isFalling && !person.isBeingTransported)
        {
            if (!isJuggling)
            {
                isJuggling = true;
                timeSinceLastFling = 0f;
            }
            else
            {
                timeSinceLastFling += Time.deltaTime;
            }
        }

        if (person.isDragging && isJuggling && timeSinceLastFling >= juggleTimeThreshold)
        {
            juggleCount++;
            SpawnFloatingText(juggleCount);
            isJuggling = false;
        }
    }

    private void SpawnFloatingText(int count)
    {
        if (textPrefab == null || spawnPoint == null) return;

        GameObject textObj = Instantiate(textPrefab, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
        TextMeshProUGUI textComponent = textObj.GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            textComponent.text = count + "!";
            StartCoroutine(FadeAndDestroy(textComponent));
        }
    }

    private IEnumerator FadeAndDestroy(TextMeshProUGUI text)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < duration)
        {
            text.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            text.rectTransform.position += Vector3.up * Time.deltaTime * 50f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(text.gameObject);
    }
}
