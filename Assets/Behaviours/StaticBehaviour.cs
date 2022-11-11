using UnityEngine;

public class StaticBehaviour : AssetBehaviour
{
    public override void Displace(Vector3 targetPosition) {}

    public override void Rotate(Vector3 targetRotation, float yawDisplay) { }

    public override string GetDescription()
    {
        return gameObject.name;
    }

    public override Vector3 GetLookAtPosition()
    {
        return transform.position;
    }

    public override void OnUnselect() {}
}
