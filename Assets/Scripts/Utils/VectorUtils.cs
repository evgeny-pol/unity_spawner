using UnityEngine;

public static class VectorUtils
{
    public static float HorizontalMagnitude(Vector3 vector)
    {
        vector.y = 0;
        return vector.magnitude;
    }

    public static float DistanceBetween(Vector3 point1, Vector3 point2)
    {
        return (point2 - point1).magnitude;
    }

    public static Vector3 HorizontalDirection(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        direction.y = 0;
        return direction;
    }
}
