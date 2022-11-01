using UnityEngine;

public class WallMovementBehaviour : AssetBehaviour
{
    const float minx = -9.0f;
    const float maxx = 9.0f;
    const float miny = 0.5f;
    const float maxy = 4.5f;

    public override void Displace(Vector3 diff)
    {
        Vector3 actualDiff = new Vector3(diff.x, diff.y, 0.0f);
        Vector3 newPos = transform.position + actualDiff;
        if (IsWithinBounds(newPos))
            transform.position = newPos;
    }

    bool IsWithinBounds(Vector3 newPos)
    {
        return newPos.x > minx && newPos.x < maxx && newPos.y > miny && newPos.y < maxy;
    }
}