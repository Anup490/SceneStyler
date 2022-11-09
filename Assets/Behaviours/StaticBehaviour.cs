using UnityEngine;

public class StaticBehaviour : AssetBehaviour
{
    public override void Displace(Vector3 targetPosition) {}

    public override string GetDescription()
    {
        return gameObject.name;
    }

    public override void OnUnselect() {}

    public override void Rotate(Vector3 targetRotation, float yawDisplay) {}
}
