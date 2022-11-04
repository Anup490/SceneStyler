using UnityEngine;

public class Android : Device
{
    int prevDragFingerId = -1;

    public Android(GameBehaviour behaviour) : base(behaviour) {}

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Utils.IsNotTouchingUI(touch.position))
                OnTouch(touch);
        }
    }

    void OnTouch(Touch touch)
    {
        if ((touch.phase == TouchPhase.Began) && (prevDragFingerId == -1))
        {
            prevDragFingerId = touch.fingerId;
            gameBehaviour.OnMouseClick(touch.position);
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
