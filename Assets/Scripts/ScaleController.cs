using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    private Vector3 originalObjScale;

    // Start is called before the first frame update
    void Start()
    {
        originalObjScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        Vector2 primaryInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        gameObject.transform.localScale = originalObjScale * (1 + primaryInput.y);
    }
}
