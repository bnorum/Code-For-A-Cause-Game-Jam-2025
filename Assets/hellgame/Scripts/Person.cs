using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour
{
    public PersonSchema personSchema;
    public string personName;
    public int age;
    public string occupation;
    public int netWorth;

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private bool shouldMove = true;
    public float dragThreshold = 0.5f;
    private bool isFalling = false;

    private Rigidbody2D rb;
    void Start()
    {
        personName = personSchema.personName;
        age = personSchema.age;
        occupation = personSchema.occupation;
        netWorth = personSchema.netWorth;
    }
    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
    public void StartMovement(Vector3 endPosition, float duration)
    {
        StartCoroutine(MoveToPosition(endPosition, duration));
    }

    private IEnumerator MoveToPosition(Vector3 endPosition, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            if (!isDragging && shouldMove && !isFalling)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        if (!isDragging && !isFalling) 
        {
            transform.position = endPosition;
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (isFalling)
        {
            isFalling = false;
            shouldMove = false;
        }
        isDragging = true;
        shouldMove = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector3.zero;

        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        float distance = Vector3.Distance(transform.position, GetMouseWorldPos());

        if (distance > dragThreshold)
        {
            shouldMove = false;
            isFalling = true;
            rb.bodyType = RigidbodyType2D.Dynamic; 
            rb.gravityScale = 1;
        }
        else
        {
            shouldMove = true;
        }
    }
}
