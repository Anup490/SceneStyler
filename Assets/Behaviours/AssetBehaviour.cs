using UnityEngine;

abstract public class AssetBehaviour : MonoBehaviour
{
    public float yaw{ get; set; }

    abstract public void Displace(Vector3 targetPosition);

    abstract public void OnUnselect();
}
