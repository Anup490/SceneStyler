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
}

