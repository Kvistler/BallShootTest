using UnityEngine;

public static class Helper
{
    public static float Distance2D(this Vector3 v1, Vector3 v2)
    {
        return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
    }
}