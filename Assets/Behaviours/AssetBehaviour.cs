using UnityEngine;

abstract public class AssetBehaviour : MonoBehaviour
{
    protected Vector3 deltaPosition;

    public float yaw{ get; set; }

    abstract public void Displace(Vector3 targetPosition);

    abstract public void OnUnselect();
}
