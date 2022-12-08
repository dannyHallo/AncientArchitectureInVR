using UnityEngine;

// this class is used in each of the building selection buttons
public class HoverAct : MonoBehaviour
{
    // the little building model the button is displaying
    public Transform objectToHover;
    // an pre-set anchor which sits at the origin of the button
    public Transform anchor;
    public float hoverHeight = 1.0f;
    public float hoverSpeed = 1.0f;

    private float originalAnchorHeight;
    private Vector3 vel;

    // record the original height
    private void Start()
    {
        originalAnchorHeight = objectToHover.position.y;
        anchor.position = objectToHover.position;
    }

    // let the little building always chase the anchor, to create a smooth hovering effect
    private void Update()
    {
        objectToHover.position = Vector3.SmoothDamp(objectToHover.position, anchor.position, ref vel, hoverSpeed);
    }

    public void startHovering()
    {
        // in this function, only the anchor's position is teleported to the aim position of the little building, let the chasing function in Update() do its job
        anchor.position = new Vector3(anchor.position.x, originalAnchorHeight + hoverHeight, anchor.position.z);
    }

    public void endHovering()
    {
        anchor.position = new Vector3(anchor.position.x, originalAnchorHeight, anchor.position.z);
    }
}
