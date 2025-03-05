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
    public float dampener = 1.5f;
    public float startGravity = 1.0f;
    public float endGravity = 5.0f;
    private float elapsedGravityTime = 0.0f;
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
    public float angularVelocityCap = 200f;

    public void Init(PersonSchema personSchema, BoxCollider2D collider, GameObject startPoint, GameObject endPoint)
    {
        this.personSchema = personSchema;
        boundsCollider = collider;
        startPointGameRef = startPoint;
        endPointGameRef = endPoint;
        nameTagObject.SetActive(false);
        nameTagDefaultLocation = nameTagObject.transform;
        OutOfBoundsScript.Instance.UpdateAlivePeople(gameObject);
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        springJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDragging();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            FlingObject();
        }
        if (isDragging)
        {
            FollowMouse();
            rb.angularVelocity *= 0.5f;
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -angularVelocityCap, angularVelocityCap);
        }
        CheckMouseHover();
        elapsedTime += Time.deltaTime;
        if (isBeingTransported && !isDragging && !isFalling)
        {
            MoveToPosition(durationReference);
        }
        if (isFalling)
        {
            UpdateGravityScale();
        }
    }

    public void StartMovement(float duration)
    {
        durationReference = duration;
        elapsedTime = 0.0f;
        isBeingTransported = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.bodyType = RigidbodyType2D.Kinematic;

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
                OutOfBoundsScript.Instance.UpdateAlivePeople(gameObject);
                Destroy(gameObject);
            }
            else
            {
                PersistentData.peopleDamned.Add(personSchema);
                OutOfBoundsScript.Instance.UpdateAlivePeople(gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void TryStartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if ((hit.collider == personCollider || hit.collider == gameObject.GetComponent<Collider2D>()) && !isMicrowaving)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.angularVelocity = 0;
            isDragging = true;
            isFalling = false;
            isBeingTransported = false;
            cursorPoint.transform.position = GetMouseWorldPos(cursorPoint.transform);
            springJoint.enabled = true;
            springJoint.autoConfigureDistance = false;
            springJoint.distance = 0.05f;
            springJoint.connectedBody = null;
            springJoint.connectedAnchor = cursorPoint.transform.position;
            EmailManager.Instance.canScroll = false;
        }
    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPos = GetMouseWorldPos(cursorPoint.transform);
        cursorPoint.transform.position = ClampPositionToBounds(mouseWorldPos);
        springJoint.connectedAnchor = cursorPoint.transform.position;
        Vector3 clampedPosition = ClampPositionToBounds(transform.position);
        transform.position = clampedPosition;
        storedVelocity = rb.linearVelocity;
    }

    private void FlingObject()
    {
        elapsedGravityTime = 0f;
        isDragging = false;
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = startGravity;
        rb.linearVelocity = storedVelocity / dampener;
        springJoint.enabled = false;
        cursorPoint.transform.position = transform.position;
        EmailManager.Instance.canScroll = true;

    }

    private void UpdateGravityScale()
    {
        elapsedGravityTime += Time.deltaTime;
        rb.gravityScale = Mathf.Lerp(startGravity, endGravity, elapsedGravityTime);
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
        if (hit.collider != null && hit.collider == personCollider)
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

    public void ResetToDefault()
    {
        isDragging = false;
        isFalling = true;
        isBeingTransported = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = startGravity;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        springJoint.enabled = false;
        cursorPoint.transform.position = transform.position;
        EmailManager.Instance.canScroll = true;
    }
}