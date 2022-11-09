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
        float zdiff = transform.position.z - minz;
        float actualMinx = minx - zdiff;
        float actualMaxx = maxx + zdiff;
        return newPos.x > actualMinx && newPos.x < actualMaxx && newPos.z < maxz && newPos.z > minz;
    }
}
