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
                if (IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
                gameBehaviour.OnDrag(Input.mousePosition - lastMousePos);
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

    bool IsZero(Vector3 vec)
    {
        const float epsilon = 0.00001f;
        float diffx = vec.x - 0.0f;
        float diffy = vec.y - 0.0f;
        float diffz = vec.z - 0.0f;
        if (diffx < 0.0f) diffx *= -1.0f;
        if (diffy < 0.0f) diffy *= -1.0f;
        if (diffz < 0.0f) diffz *= -1.0f;
        return (diffx < epsilon) && (diffy < epsilon) && (diffz < epsilon);
    }
}