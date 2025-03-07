using TMPro;
using UnityEngine;
using System.Collections;

public class garbageBin : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float duration = 2f;
    
    public SpriteAnimator flames;

    void Start()
    {
        text.gameObject.SetActive(false);   
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            PersistentData.peopleDamned.Add(collision.gameObject.GetComponent<Person>().personSchema);
            if (!collision.gameObject.GetComponent<Person>().personSchema.shouldGoToHeaven) {
                PersistentData.peopleDeterminedCorrectly++;
            }
            flames.PlayAnim();
            OutOfBoundsScript.Instance.UpdateAlivePeople(collision.gameObject);
            collision.gameObject.GetComponent<Person>().GetBonuses();
            Destroy(collision.gameObject);

        }
    }

    public void DisplayStat(Vector2 linearVelocity, float angularVelocity, float distance, bool isDragging)
    {
        text.gameObject.SetActive(true);
        
        if (isDragging)
        {
            if (angularVelocity > PersistentData.bestAngularVelocity)
            {
                text.text = $"{angularVelocity:F2} rad/s! + PB";
                PersistentData.bestAngularVelocity = angularVelocity;
            }
            else
            {
                text.text = $"{angularVelocity:F2} rad/s!";
            }
        }
        else
        {
            bool isBestLinear = linearVelocity.magnitude > PersistentData.bestLinearVelocity;
            bool isBestDistance = distance > PersistentData.bestDistance;

            if (isBestLinear && isBestDistance)
            {
                if (Random.value > 0.5f)
                {
                    text.text = $"{linearVelocity.magnitude:F2} m/s! + PB";
                    PersistentData.bestLinearVelocity = linearVelocity.magnitude;
                }
                else
                {
                    text.text = $"{distance:F0} meters! + PB";
                    PersistentData.bestDistance = distance;
                }
            }
            else if (isBestLinear)
            {
                text.text = $"{linearVelocity.magnitude:F2} m/s! + PB";
                PersistentData.bestLinearVelocity = linearVelocity.magnitude;
            }
            else if (isBestDistance)
            {
                text.text = $"{distance:F0} meters! + PB";
                PersistentData.bestDistance = distance;
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    text.text = $"{linearVelocity.magnitude:F2} m/s!";
                }
                else
                {
                    text.text = $"{distance:F0} meters!";
                }
            }
        }

        StartCoroutine(AnimateText());
    }
    private IEnumerator AnimateText()
    {
        float elapsedTime = 0f;
        Color startColor = Color.black;
        Color endColor = new Color(0, 0, 0, 0);

        while (elapsedTime < duration)
        {
            // text.rectTransform.position = Vector3.Lerp(startLerp.position, endLerp.position, elapsedTime / duration);
            text.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // text.rectTransform.position = endLerp.position;
        text.color = endColor;
        text.gameObject.SetActive(false);

    }
}
