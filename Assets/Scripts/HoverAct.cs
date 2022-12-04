using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAct : MonoBehaviour
{
    public Transform objectToHover;
    public Transform anchor;
    public float hoverHeight = 1.0f;
    public float hoverSpeed = 1.0f;

    private float originalAnchorHeight;
    private Vector3 vel;

    private void Start()
    {
        originalAnchorHeight = objectToHover.position.y;
        anchor.position = objectToHover.position;
    }

    private void Update()
    {
        objectToHover.position = Vector3.SmoothDamp(objectToHover.position, anchor.position, ref vel, hoverSpeed);
    }

    public void startHovering()
    {
        anchor.position = new Vector3(anchor.position.x, originalAnchorHeight + hoverHeight, anchor.position.z);
    }

    public void endHovering()
    {
        anchor.position = new Vector3(anchor.position.x, originalAnchorHeight, anchor.position.z);
    }
}
