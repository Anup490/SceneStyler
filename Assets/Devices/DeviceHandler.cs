using System.Collections.Generic;
using UnityEngine;

public class DeviceHandler
{
    static DeviceHandler handler;

    Device device;
    List<IDeviceCallback> callbacks;
    bool isSideBarVisible;
    bool hasClickedUI;

    public enum Type
    {
        GAME, UI
    }

    public static DeviceHandler Get()
    {
        if (handler == null)
            handler = new DeviceHandler();
        return handler;
    }

    private DeviceHandler()
    {
        device = (SystemInfo.deviceType == DeviceType.Handheld) ? new Android(this) : new Windows(this);
        callbacks = new List<IDeviceCallback>();
    }

    public void AddCallback(IDeviceCallback callback)
    {
        callbacks.Add(callback);
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
            GetCallback(Type.GAME).OnClick(position);
        else if (!hasClickedUI)
        {
            GetCallback(Type.UI).OnClick(position);
            hasClickedUI = true;
        }
    }

    public void OnDrag(Vector3 position)
    {
        if (IsNotTouchingSideBar(position))
            GetCallback(Type.GAME).OnHold(position);
    }

    public void OnRelease()
    {
        foreach (IDeviceCallback callback in callbacks)
            callback.OnRelease();
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

    IDeviceCallback GetCallback(Type type)
    {
        foreach (IDeviceCallback callback in callbacks)
        {
            if (callback.GetType() == type)
                return callback;
        }
        return null;
    }
}
