using UnityEngine;

public class Windows : Device
{
    Vector3 lastMousePos;
    bool hasPressedLMB = false;
    
    public Windows(DeviceHandler deviceHandler) : base(deviceHandler) {}

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (hasPressedLMB)
            {
                if (Utils.IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
                handler.OnDrag(Input.mousePosition);
                lastMousePos = Input.mousePosition;
            }
            else
            {
                handler.OnClick(Input.mousePosition);
                hasPressedLMB = true;
            }              
        }  
        else
        {
            lastMousePos = Vector3.zero;
            hasPressedLMB = false;
            handler.OnRelease();
        }
    }
}