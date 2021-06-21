using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcLooking : MonoBehaviour
{
    public void SetNameText(string value)
    {
        text.text = value;
    }

    public void SetLookingObject(GameObject value)
    {
        lookingObject = value;
    }

    public void SetNullLookingObject()
    {
        SetLookingObject(null);
    }


    [SerializeField] private Text text = null;
    [SerializeField] private float lookingSpeed = 90.0f;
    private GameObject lookingObject;


    private void FixedUpdate()
    {
        Quaternion target = this.transform.parent.rotation;

        if (lookingObject != null)
        {
            Vector3 dir = lookingObject.transform.position - this.transform.parent.position;
            dir.Normalize();

            if (Vector3.Dot(dir, this.transform.parent.forward) > 0)
            {
                target = Quaternion.LookRotation(dir, Vector3.up);
            }
        }

        if (target != this.transform.rotation)
        {
            float deltaSpeed = lookingSpeed * Time.fixedDeltaTime;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target, deltaSpeed);
        }

    }

}
