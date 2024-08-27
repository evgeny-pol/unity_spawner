using UnityEngine;

public static class QuaternionUtils
{
    public static Quaternion RotateTowards(Quaternion fromRotation, Vector3 toDirection, float maxDelta)
    {
        Quaternion toTargetRotation = Quaternion.LookRotation(toDirection);
        return Quaternion.RotateTowards(fromRotation, toTargetRotation, maxDelta);
    }
}
