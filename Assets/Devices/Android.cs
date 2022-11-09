using UnityEngine;

public class Android : Device
{
    int prevDragFingerId = -1;

    public Android(DeviceHandler deviceHandler) : base(deviceHandler) {}

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
            OnTouch(Input.GetTouch(0));
    }

    void OnTouch(Touch touch)
    {
        if ((touch.phase == TouchPhase.Began) && (prevDragFingerId == -1))
        {
            prevDragFingerId = touch.fingerId;
            handler.OnClick(touch.position);
        }
        else if ((touch.phase == TouchPhase.Moved) && (prevDragFingerId == touch.fingerId))
            handler.OnDrag(touch.position);
        else if (touch.phase == TouchPhase.Ended)
        {
            prevDragFingerId = -1;
            handler.OnRelease();
        }
    }
}
