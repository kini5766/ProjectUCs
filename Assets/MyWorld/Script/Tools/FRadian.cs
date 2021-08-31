using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --
// 각도 관련 간편 함수들
// --
public struct FRadian
{
    // -- 정적 함수들 -- //

    public static float NormalizeRadian(float rad)
    {
        float pi2 = Mathf.PI * 2.0f;
        float pi3 = Mathf.PI * 3.0f;
        return (rad % pi2 + pi3) % pi2 - Mathf.PI;
    }

    public static float LerpBetweenTwoAngles(float rad_1, float rad_2, float t)
    {
        rad_1 = NormalizeRadian(rad_1);
        rad_2 = NormalizeRadian(rad_2);
        if (Mathf.Abs(rad_1 - rad_2) >= Mathf.PI)
        {
            if (rad_1 > rad_2)
                rad_1 -= 2.0f * Mathf.PI;
            else
                rad_2 -= 2.0f * Mathf.PI;
        }
        return (1 - t) * rad_1 + t * rad_2;
    }

    public static Vector3 GetForward(in Quaternion q)
    {
        float xz2 = 2 * q.x * q.z;
        float yz2 = 2 * q.y * q.z;

        float xx2 = 2 * q.x * q.x;
        float yy2 = 2 * q.y * q.y;

        float xw2 = 2 * q.x * q.w;
        float yw2 = 2 * q.y * q.w;

	    return new Vector3(xz2 + yw2, yz2 - xw2, 1.0f - xx2 - yy2);
    }


    public static float GetRadian(in Vector3 value)
    {
        return -Mathf.Atan2(value.z, value.x) + Mathf.PI * 0.5f;
    }

    // -- 맴버들 -- //
    
}
