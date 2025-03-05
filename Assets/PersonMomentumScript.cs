using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public SpringJoint2D hinge;
    public GameObject parent;

    void Update()
    {
        if (parent.GetComponent<Person>().isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hinge.connectedAnchor = new Vector2(mousePos.x, mousePos.y);
            hinge.enabled = true;
        }
        else
        {
            hinge.enabled = false;
        }
    }
}
