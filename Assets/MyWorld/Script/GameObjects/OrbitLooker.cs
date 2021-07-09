using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLooker : MonoBehaviour
{
    public float ZoomLength { get => zoomLength; set => zoomLength = value; }
    public Vector3 Offset { get => offset; set => offset = value; }

    public void SetFocus(Transform value)
    {
        focus = value;
    }

    public void MoveLooking(Vector2 axis)
    {
        sphereCoord.Phi += axis.x;

	    float theta = sphereCoord.Theta + axis.y;

        if (theta > lookupMax)
            theta = lookupMax;
        else if (theta < lookupMin)
            theta = lookupMin;

        sphereCoord.Theta = theta;
    }


    Vector3 offset = Vector3.zero;
    Transform focus;
    FSphereCoord sphereCoord;
    float zoomLength = 10.0f;
    float lookupMax = 75.0f;
    float lookupMin = -15.0f;


    private void Start()
    {
        sphereCoord.SetQuaternion(transform.rotation);
    }

    private void FixedUpdate()
    {
        if (focus == null)
            return;

        Vector3 pos = offset + focus.transform.position;
        pos += sphereCoord.GetDirection() * zoomLength;
        this.transform.position = pos;
        this.transform.rotation = sphereCoord.GetQuaternion();
    }
}
