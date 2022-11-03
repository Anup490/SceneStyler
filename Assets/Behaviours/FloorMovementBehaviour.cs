using UnityEngine;

public class FloorMovementBehaviour : AssetBehaviour
{
    const float minx = -3.0f;
    const float maxx = 3.0f;
    const float minz = -9.0f;
    const float maxz = -4.5f;

    public override void Displace(Vector3 targetPosition)
    {
        Vector3 newPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        if (IsWithinBounds(newPosition))
            transform.position = newPosition;
    }

    bool IsWithinBounds(Vector3 newPos)
    {
        float zdiff = transform.position.z - minz;
        float actualMinx = minx - zdiff;
        float actualMaxx = maxx + zdiff;
        return newPos.x > actualMinx && newPos.x < actualMaxx && newPos.z < maxz && newPos.z > minz;
    }
}
