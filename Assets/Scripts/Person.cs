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
    public bool isFalling = false;
    private Vector2 storedVelocity;
    public float dampener = 3.0f;
    public BoxCollider2D personCollider;
    private BoxCollider2D boundsCollider;
    private float durationReference;
    private float elapsedTime = 0.0f;
    public bool isBeingTransported = false;
    public GameObject nameTagObject;
    public Transform nameTagBounds;
    private Transform nameTagDefaultLocation;
    private GameObject startPointGameRef;
    private GameObject endPointGameRef;
    public bool isMicrowaving = false;
    public bool hasBeenMicrowaved = false;
    public GameObject cursorPoint;
    public SpringJoint2D springJoint;

    public void Init(PersonSchema personSchema, BoxCollider2D collider, GameObject startPoint, GameObject endPoint)
    {
        this.personSchema = personSchema;
        boundsCollider = collider;
        startPointGameRef = startPoint;
        endPointGameRef = endPoint;
        nameTagObject.SetActive(false);
        nameTagDefaultLocation = nameTagObject.transform;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        springJoint.enabled = false; // Disable initially
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
            rb.angularVelocity *= 0.7f; // Stronger damping to prevent excessive spinning
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -200f, 200f); // Limit max spin speed
        }
        CheckMouseHover();
        elapsedTime += Time.deltaTime;
        if (isBeingTransported && !isDragging && !isFalling)
        {
            MoveToPosition(durationReference);
        }
    }

    public void StartMovement(float duration)
    {
        durationReference = duration;
        elapsedTime = 0.0f;
        isBeingTransported = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void RestartMovement()
    {
        isBeingTransported = true;
        isDragging = false;
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void MoveToPosition(float duration)
    {
        float t = elapsedTime / duration;
        transform.position = Vector3.Lerp(startPointGameRef.transform.position, endPointGameRef.transform.position, t);
        Vector3 newPosition = transform.position;
        newPosition.z = transform.parent.position.z;
        transform.position = newPosition;

        if (t >= 1.0f)
        {
            if (transform.position == endPointGameRef.transform.position)
            {
                PersistentData.peopleSaved.Add(personSchema);
                Destroy(gameObject);
            }
            else
            {
                PersistentData.peopleDamned.Add(personSchema);
                Destroy(gameObject);
            }
        }
    }
    private void TryStartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider == personCollider && !isMicrowaving)
        {
            Debug.Log("Dragging started");
            storedVelocity = Vector2.zero; // Reset velocity to prevent unwanted movement
            rb.angularVelocity = 0; // Stop any spinning immediately
            isDragging = true;
            isFalling = false;
            cursorPoint.transform.position = GetMouseWorldPos(cursorPoint.transform); // Move cursorPoint to mouse position
            
            // Enable and configure the Spring Joint immediately
            springJoint.enabled = true;
            springJoint.autoConfigureDistance = false;
            springJoint.distance = 0.05f; // Reduce the distance for tighter control
            springJoint.connectedBody = null;
            springJoint.connectedAnchor = cursorPoint.transform.position; // Use world space position
        }
    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPos = GetMouseWorldPos(cursorPoint.transform);
        cursorPoint.transform.position = mouseWorldPos; // Move cursorPoint in real time
        springJoint.connectedAnchor = cursorPoint.transform.position; // Keep anchor in sync
    }

    private void DropObject()
    {
        isDragging = false;
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = dampener;
        rb.linearVelocity = storedVelocity / dampener;
        rb.angularVelocity = 0; // Reset angular velocity on drop
        springJoint.enabled = false; // Disable the spring joint
        cursorPoint.transform.position = transform.position; // Reset cursorPoint to parent transform
    }

    private Vector3 GetMouseWorldPos(Transform transform = null)
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

    private float hoverTime = 0.0f;
    private float hoverThreshold = 1.0f;

    private void CheckMouseHover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject && hit.collider == personCollider)
        {
            hoverTime += Time.deltaTime;
            if (hoverTime >= hoverThreshold)
            {
                nameTagObject.SetActive(true);
            }
        }
        else
        {
            hoverTime = 0.0f;
            nameTagObject.SetActive(false);
        }
    }
}
