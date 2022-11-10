using UnityEngine;

public class DeviceHandler
{
    static DeviceHandler handler;

    Device device;
    IDeviceCallback deviceCallback;
    bool isSideBarVisible;
    bool hasClickedUI;

    public static DeviceHandler Get(IDeviceCallback callback)
    {
        if (handler == null)
            handler = new DeviceHandler(callback);
        return handler;
    }

    private DeviceHandler(IDeviceCallback callback)
    {
        device = (SystemInfo.deviceType == DeviceType.Handheld) ? new Android(this) : new Windows(this); 
        deviceCallback = callback;
    }

    public void UpdateSideBarVisibility(bool visibility)
    {
        isSideBarVisible = visibility;
    }

    public void OnUpdate()
    {
        device.OnUpdate();
    }

    public void OnClick(Vector3 position)
    {
        if (IsNotTouchingSideBar(position))
            deviceCallback.OnWorldClick(position);
        else if (!hasClickedUI)
        {
            deviceCallback.OnUIClick(position);
            hasClickedUI = true;
        }
    }

    public void OnDrag(Vector3 position)
    {
        if (IsNotTouchingSideBar(position))
            deviceCallback.OnDrag(position);
    }

    public void OnRelease()
    {
        deviceCallback.OnRelease();
        hasClickedUI = false;
    }

    bool IsNotTouchingSideBar(Vector3 cursorPos)
    {
        Vector2 screenPos = Utils.ToScreenSpace(cursorPos);
        float limit = Screen.width * 0.75f;
        if (isSideBarVisible)
            return cursorPos.x < limit;
        return true;
    }
}
