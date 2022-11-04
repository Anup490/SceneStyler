using UnityEngine;

public class Windows : Device
{
    Vector3 lastMousePos;
    bool hasPressedLMB = false;

    public Windows(GameBehaviour gameBehaviour) : base(gameBehaviour) {}

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0) && Utils.IsNotTouchingUI(Input.mousePosition))
        {
            if (hasPressedLMB)
            {
                if (Utils.IsZero(lastMousePos)) lastMousePos = Input.mousePosition;            
                gameBehaviour.OnMouseDrag(Input.mousePosition);
                lastMousePos = Input.mousePosition;
            }
            else
            {
                gameBehaviour.OnMouseClick(Input.mousePosition);
                hasPressedLMB = true;
            }              
        }
        else
        {
            lastMousePos = Vector3.zero;
            hasPressedLMB = false;
            gameBehaviour.OnMouseRelease();
        }
    }
}