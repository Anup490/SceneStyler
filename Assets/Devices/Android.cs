using UnityEngine;

public class Android : Device
{
    int prevDragFingerId = -1;
    bool hasClickedUI = false;

    public Android(GameBehaviour behaviour) : base(behaviour) {}

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Utils.IsNotTouchingUI(touch.position) && IsNotTouchingSideBar(touch.position))
                OnTouch(touch);
            else if (!IsNotTouchingSideBar(touch.position) && !hasClickedUI)
            {
                gameBehaviour.OnMouseClick(touch.position, true);
                hasClickedUI = true;
            }
        }
        else if (hasClickedUI)
            hasClickedUI = false;
    }

    void OnTouch(Touch touch)
    {
        if ((touch.phase == TouchPhase.Began) && (prevDragFingerId == -1))
        {
            prevDragFingerId = touch.fingerId;
            gameBehaviour.OnMouseClick(touch.position, false);
        }
        else if ((touch.phase == TouchPhase.Moved) && (prevDragFingerId == touch.fingerId))
            gameBehaviour.OnMouseDrag(touch.position);
        else if (touch.phase == TouchPhase.Ended)
        {
            prevDragFingerId = -1;
            gameBehaviour.OnMouseRelease();
        }
    }
}
