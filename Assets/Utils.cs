using System;
using UnityEngine;

class Utils
{
    public static bool IsZero(Vector3 vec)
    {
        return IsZero(vec.x) && IsZero(vec.y) && IsZero(vec.z);
    }

    public static bool IsEqual(float f1, float f2)
    {
        return IsZero(f1 - f2);
    }

    public static bool IsZero(float f)
    {
        const float epsilon = 0.00001f;
        if (f < 0.0f) f *= -1.0f;
        return (f < epsilon);
    }

    public static float GetCosine(Vector3 v1, Vector3 v2)
    {
        float dot = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        float v1Len = (float)Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        float v2Len = (float)Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z);
        return dot / (v1Len * v2Len);
    }

    public static bool IsNotTouchingUI(Vector3 cursorPos)
    {
        float w = cursorPos.x / Screen.width;
        float h = cursorPos.y / Screen.height;
        return w < 0.85f || h < 0.85f;
    }

    public static Vector2 ToScreenSpace(Vector3 rasterCoord)
    {
        return new Vector2(rasterCoord.x / Screen.width, rasterCoord.y / Screen.height);
    }
}

