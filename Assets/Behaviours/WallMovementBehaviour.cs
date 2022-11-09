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
        if (Utils.IsZero(deltaPosition))
            deltaPosition = newPosition - transform.position;
        else if (IsWithinBounds(newPosition))
            transform.position = newPosition - deltaPosition;
    }

    public override void Rotate(Vector3 targetRotation, float yawDisplay)
    {
        transform.Rotate(targetRotation);
        yaw = yawDisplay;
    }

    public override void OnUnselect()
    {
        deltaPosition = Vector3.zero;
    }

    public override string GetDescription()
    {
        return gameObject.name;
    }

    bool IsWithinBounds(Vector3 newPos)
    {
        return newPos.x > minx && newPos.x < maxx && newPos.y > miny && newPos.y < maxy;
    }
}