using UnityEngine;

public class Android : Device
{
    int prevDragFingerId = -1;

    public Android(GameBehaviour behaviour) : base(behaviour) {}

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
            HandleDrag(Input.GetTouch(0));
        if ((Input.touchCount > 1) && (selectedObject != null))
            HandleRotation(Input.GetTouch(1));
    }

    void HandleDrag(Touch touch)
    {
        if ((touch.phase == TouchPhase.Began) && (prevDragFingerId == -1))
        {
            prevDragFingerId = touch.fingerId;
            RayCast(touch.position);
        }
        else if ((touch.phase == TouchPhase.Moved) && (prevDragFingerId == touch.fingerId))
            MoveObject(touch);
        else if (touch.phase == TouchPhase.Ended)
        {
            prevDragFingerId = -1;
            Reset();
        }
    }

    void HandleRotation(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            Transform transform = selectedObject.transform;
            transform.Rotate(-touch.deltaPosition.y, -touch.deltaPosition.x, 0);
        }
    }

    void MoveObject(Touch touch)
    {
        if (selectedObject != null)
        {
            gameBehaviour.ShowUIAt(selectedObject.name);
            float zoffset = (camera.transform.position.z - selectedObject.transform.position.z) * -1.0f;
            float dragSpeed = zoffset * 0.001875f;
            Vector3 diff = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y) * dragSpeed;
            Vector3 newPosition = selectedObject.transform.position + diff;
            if (newPosition.y >= miny)
                selectedObject.transform.position = newPosition;
        }
    }
}
