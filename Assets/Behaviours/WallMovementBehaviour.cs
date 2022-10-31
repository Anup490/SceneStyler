using UnityEngine;

public class WallMovementBehaviour : AssetBehaviour
{
    public override void Displace(Vector3 diff)
    {
        Transform transform = GetComponent<Transform>();
        Vector3 actualDiff = new Vector3(diff.x, diff.y, 0.0f);
        transform.position += actualDiff;
    }
}