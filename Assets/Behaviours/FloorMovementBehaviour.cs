using UnityEngine;

public class FloorMovementBehaviour : AssetBehaviour
{
    public override void Displace(Vector3 diff)
    {
        Transform transform = GetComponent<Transform>();
        Vector3 actualDiff = new Vector3(diff.x, 0.0f, diff.y);
        transform.position += actualDiff;
    }
}
