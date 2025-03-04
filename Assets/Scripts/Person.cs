using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour
{
    [Header("Person Vars")]
    public PersonSchema personSchema;

    [Header("Movement Vars")]
    private Camera mainCamera;
    private Rigidbody2D rb;
    public bool isDragging = false;
    private bool shouldMove = true;
    public bool isFalling = false;
    private Vector3 offset;
    private Vector2 storedVelocity;
    public float dampener = 3.0f;
    private Collider2D boundsCollider;
    private Vector3 endPointReference;
    private Vector3 startPointReference;
    private float durationReference;
    public bool isBeingTransported = false;
    public GameObject childObject;
    private Transform childDefaultPoint;

    public void Init(PersonSchema personSchema, Collider2D personBounds)
    {
        this.personSchema = personSchema;
        boundsCollider = personBounds;
        childObject.SetActive(false); // Initially set the child object to inactive
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
        startPointReference = transform.position;
        childDefaultPoint = childObject.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDragging();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DropObject();
        }
        if (isDragging)
        {
            FollowMouse();  
        }
        CheckMouseHover();
    }

    public void StartMovement(Vector3 endPosition, float duration)
    {
        endPointReference = endPosition;
        durationReference = duration;
        isBeingTransported = true;
        StartCoroutine(MoveToPosition(endPosition, duration));
    }

    public void RestartMovement()
    {
        isBeingTransported = true;
        isDragging = false;
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(MoveToPosition(endPointReference, durationReference));
    }

    private IEnumerator MoveToPosition(Vector3 endPosition, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            if (!isDragging && isBeingTransported && !isFalling)
            {
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                Vector3 newPosition = transform.position;
                newPosition.z = transform.parent.position.z;
                transform.position = newPosition;

                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }

        if (transform.position == endPosition && isBeingTransported)
        {
            PersistentData.peopleSaved.Add(personSchema);
            Destroy(gameObject);
        }
    }

    private void TryStartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            storedVelocity = rb.linearVelocity;
            isDragging = true;
            isBeingTransported = false;
            isFalling = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            Vector3 mouseWorldPosition = GetMouseWorldPos();
            offset = transform.position - mouseWorldPosition;
        }
    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPos();
        Vector3 newPosition = mouseWorldPosition + offset;
        newPosition = ClampPositionToBounds(newPosition);

        storedVelocity = (newPosition - transform.position) / Time.deltaTime;
        transform.position = newPosition;
    }

    private void DropObject()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isDragging = false;
        isFalling = true;
        shouldMove = false;

        rb.gravityScale = dampener;
        rb.linearVelocity = storedVelocity / dampener;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        Bounds bounds = boundsCollider.bounds;
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        return position;
    }

    private void CheckMouseHover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject && !isDragging)
        {
            childObject.SetActive(true);
            if(childObject.transform.position != ClampPositionToBounds(childObject.transform.position))
            {
                childObject.transform.position = ClampPositionToBounds(childObject.transform.position);
            }
            else
            {
                childObject.transform.position = childDefaultPoint.position;
            }
        }
        else
        {
            childObject.SetActive(false);
        }
    }
}
