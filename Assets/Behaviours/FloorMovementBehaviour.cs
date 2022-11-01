using UnityEngine;

public class FloorMovementBehaviour : AssetBehaviour
{
    const float minx = -3.0f;
    const float maxx = 3.0f;
    const float minz = -8.0f;
    const float maxz = -4.5f;

    public override void Displace(Vector3 diff)
    {    
        Vector3 actualDiff = new Vector3(diff.x, 0.0f, diff.y);
        Vector3 newPos = transform.position + actualDiff;
        if (IsWithinBounds(newPos))
            transform.position = newPos;
    }

    bool IsWithinBounds(Vector3 newPos)
    {
        float zdiff = transform.position.z - minz;
        float actualMinx = minx - zdiff;
        float actualMaxx = maxx + zdiff;
        return newPos.x > actualMinx && newPos.x < actualMaxx && newPos.z < maxz;
    }
}
