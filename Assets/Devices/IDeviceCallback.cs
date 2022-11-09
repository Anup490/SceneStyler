using UnityEngine;

public interface IDeviceCallback
{
    void OnWorldClick(Vector3 position);

    void OnUIClick(Vector3 position);
    
    void OnDrag(Vector3 position);
    
    void OnRelease();
}
