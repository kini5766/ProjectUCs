using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLooking : MonoBehaviour
{
    public void MoveLooking(Vector2 axis)
    {
        sphereCoord.Phi += axis.x;

	    float theta = sphereCoord.Theta + axis.y;

        if (theta > lookupMax)
            theta = lookupMax;
        else if (theta < lookupMin)
            theta = lookupMin;

        sphereCoord.Theta = theta;

        transform.rotation = sphereCoord.GetQuaternion();
    }


    FSphereCoord sphereCoord;
    float lookupMax = 75.0f;
    float lookupMin = -15.0f;

    private void Start()
    {
        sphereCoord.SetQuaternion(transform.rotation);
    }

}
