using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour
{   
    // Person Variables
    private PersonSchema personSchema;
    public string personName;
    public int age;
    public string occupation;
    public int netWorth;

    // Movement Variables
    private Camera mainCamera;
    private Rigidbody2D rb;
    private bool isDragging = false;
    private bool shouldMove = true;
    private bool isFalling = false;
    private Vector3 offset;
    private Vector2 storedVelocity;
    public float dampener = 3.0f;

    // Collider for bounds
    private Collider2D boundsCollider;

    public void Init(PersonSchema personSchema, Collider2D personBounds)
    {
        this.personSchema = personSchema;
        personName = personSchema.personName;
        age = personSchema.age;
        occupation = personSchema.occupation;
        netWorth = personSchema.netWorth;
        boundsCollider = personBounds;
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
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                Vector3 newPosition = transform.position;
                newPosition.z = transform.parent.position.z;
                transform.position = newPosition;
                storedVelocity = (transform.position - startPosition) / Time.deltaTime;

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

    private void TryStartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            storedVelocity = rb.linearVelocity;
            isDragging = true;
            shouldMove = false;

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

        // Clamp position within bounds
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

        Destroy(gameObject, 5.0f);
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
}
