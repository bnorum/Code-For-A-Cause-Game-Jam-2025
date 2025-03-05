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
    private Vector3 offset;
    private Vector2 storedVelocity;
    public float dampener = 3.0f;
    private Collider2D boundsCollider;
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
    public GameObject springPoint;

    public void Init(PersonSchema personSchema, Collider2D personBounds, GameObject startPoint, GameObject endPoint)
    {
        this.personSchema = personSchema;
        boundsCollider = personBounds;
        startPointGameRef = startPoint;
        endPointGameRef = endPoint;
        nameTagObject.SetActive(false);
        nameTagDefaultLocation = nameTagObject.transform;
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
        SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
        springJoint.connectedBody = springPoint.GetComponent<Rigidbody2D>();
        springJoint.autoConfigureDistance = false;
        springJoint.distance = 0.5f;
        springJoint.dampingRatio = 0.7f;
        springJoint.frequency = 1.0f;
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
        float t = elapsedTime / (duration/GameManager.Instance.difficultyScale);
        transform.position = Vector3.Lerp(startPointGameRef.transform.position, endPointGameRef.transform.position, t);
        Vector3 newPosition = transform.position;
        newPosition.z = transform.parent.position.z;
        transform.position = newPosition;

        if (t >= 1.0f)  
        {//time expired
            if(transform.position == endPointGameRef.transform.position)
            {
                PersistentData.peopleSaved.Add(personSchema);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                //TODO: Add to list of people who were not saved
                PersistentData.peopleDamned.Add(personSchema);
            }
        }
    }

    private void TryStartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject && !isMicrowaving)
        {
            storedVelocity = rb.linearVelocity;
            isDragging = true;
            isBeingTransported = false;
            isFalling = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
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
        rb.linearVelocity = (newPosition - transform.position) * 10f;
    }

    private void DropObject()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isDragging = false;
        isFalling = true;

        rb.gravityScale = dampener;
        rb.linearVelocity = storedVelocity / dampener;

        SpringJoint2D spring = GetComponent<SpringJoint2D>();
        if (spring != null)
        {
            spring.distance = 0.5f; // Reset to default value
        }
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

    private float hoverTime = 0.0f;
    private float hoverThreshold = 1.0f;

    private void CheckMouseHover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
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