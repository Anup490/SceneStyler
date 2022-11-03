using UnityEngine;

public class WallMovementBehaviour : AssetBehaviour
{
    const float minx = -9.0f;
    const float maxx = 9.0f;
    const float miny = 0.5f;
    const float maxy = 4.5f;

    public override void Displace(Vector3 targetPosition)
    {
        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        if (IsWithinBounds(newPosition))
            transform.position = newPosition;
    }

    bool IsWithinBounds(Vector3 newPos)
    {
        return newPos.x > minx && newPos.x < maxx && newPos.y > miny && newPos.y < maxy;
    }
}