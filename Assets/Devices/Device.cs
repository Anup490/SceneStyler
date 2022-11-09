using UnityEngine;

public abstract class Device
{
    protected DeviceHandler handler;

    public Device(DeviceHandler deviceHandler)
    {
        handler = deviceHandler;
    }

    public abstract void OnUpdate();
}
