using UnityEngine;

public class Windows : Device
{
    Vector3 lastMousePos;
    bool hasPressedLMB = false;

    public Windows(GameBehaviour gameBehaviour) : base(gameBehaviour) {}

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 diff = Input.mousePosition - lastMousePos;
            gameBehaviour.OnRotate(diff);
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (hasPressedLMB)
            {
                if (Utils.IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
                gameBehaviour.OnDrag(Input.mousePosition);
                lastMousePos = Input.mousePosition;
            }
            else
            {
                gameBehaviour.OnDragStart(Input.mousePosition);
                hasPressedLMB = true;
            }              
        }
        else
        {
            lastMousePos = Vector3.zero;
            hasPressedLMB = false;
            gameBehaviour.OnDragEnd();
        }
    }
}