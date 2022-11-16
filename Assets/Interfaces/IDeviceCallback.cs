using UnityEngine;

public interface IDeviceCallback
{
    void OnClick(Vector3 position);
    
    void OnHold(Vector3 position, Vector2 axes);
    
    void OnRelease();

    DeviceHandler.Type GetType();
}
