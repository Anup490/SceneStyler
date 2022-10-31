using UnityEngine;

public class Android : Device
{
    int prevDragFingerId = -1;

    public Android(GameBehaviour behaviour) : base(behaviour) {}

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
            OnFirstTouch(Input.GetTouch(0));
        if (Input.touchCount > 1)
            OnSecondTouch(Input.GetTouch(1));
    }

    void OnFirstTouch(Touch touch)
    {
        if ((touch.phase == TouchPhase.Began) && (prevDragFingerId == -1))
        {
            prevDragFingerId = touch.fingerId;
            gameBehaviour.OnDragStart(touch.position);
        }
        else if ((touch.phase == TouchPhase.Moved) && (prevDragFingerId == touch.fingerId))
            gameBehaviour.OnDrag(new Vector3(touch.deltaPosition.x, touch.deltaPosition.y));
        else if (touch.phase == TouchPhase.Ended)
        {
            prevDragFingerId = -1;
            gameBehaviour.OnDragEnd();
        }
    }

    void OnSecondTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
            gameBehaviour.OnRotate(touch.deltaPosition);   
    }
}
