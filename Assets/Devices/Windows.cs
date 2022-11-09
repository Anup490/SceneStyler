using UnityEngine;

public class Windows : Device
{
    Vector3 lastMousePos;
    bool hasPressedLMB = false;
    bool hasClickedUI = false;

    public Windows(GameBehaviour gameBehaviour) : base(gameBehaviour) {}

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0) && Utils.IsNotTouchingUI(Input.mousePosition))
        {
            if (hasPressedLMB && IsNotTouchingSideBar(Input.mousePosition))
            {
                if (Utils.IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
                gameBehaviour.OnMouseDrag(Input.mousePosition);
                lastMousePos = Input.mousePosition;
            }
            else
            {
                if (IsNotTouchingSideBar(Input.mousePosition))
                {
                    gameBehaviour.OnMouseClick(Input.mousePosition, false);
                    hasPressedLMB = true;
                }
                else if (!hasClickedUI)
                {
                    gameBehaviour.OnMouseClick(Input.mousePosition, true);
                    hasClickedUI = true;
                }
            }              
        }  
        else
        {
            lastMousePos = Vector3.zero;
            hasPressedLMB = false;
            hasClickedUI = false;
            gameBehaviour.OnMouseRelease();
        }
    }
}