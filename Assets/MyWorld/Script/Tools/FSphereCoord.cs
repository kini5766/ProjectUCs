using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --
// Roll�� ������ ���Ϸ� ��ǥ�� (degree���� ���)
// forward == GetDirection()�� �������� �������.
// --
public struct FSphereCoord
{
    public float Theta;
    public float Phi;

    public void SetDirection(Vector3 value)
    {
        value.Normalize();
        Phi = Mathf.Atan2(value.x, value.z);
        Theta = Mathf.Acos(value.y) + Mathf.PI * 0.5f;

        Phi *= Mathf.Rad2Deg;
        Theta *= Mathf.Rad2Deg;
    }

    public Vector3 GetDirection()
    {
        float radP = Phi * Mathf.Deg2Rad;
        float radT = Theta * Mathf.Deg2Rad - Mathf.PI * 0.5f;
        float sinP = Mathf.Sin(radP);
        float cosP = Mathf.Cos(radP);
        float sinT = Mathf.Sin(radT);
        float cosT = Mathf.Cos(radT);

        return new Vector3((sinT * sinP), (cosT), (sinT * cosP));
    }

    public void SetEuler(Vector3 euler)
    {
        Phi = euler.y; 
        Theta = euler.x; 
    }

    public Vector3 GetPitchYaw()
    {
        return new Vector3(Theta, Phi , 0.0f);
    }

    public void SetQuaternion(Quaternion q)
    {
        SetEuler(q.eulerAngles);
    }

    public Quaternion GetQuaternion()
    {
        return Quaternion.Euler(Theta, Phi, 0.0f);
    }

}
