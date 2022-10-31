using UnityEngine;

public class Windows : Device
{
    Vector3 lastMousePos;

    public Windows(GameBehaviour gameBehaviour) : base(gameBehaviour) {}

    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (selectedObject == null)
                RayCast(Input.mousePosition);
            else
                MoveObject();
        }
        else
        {
            lastMousePos = Vector3.zero;
            Reset();
        }
    }

    /*void RayCast()
    {
        //Vector3 mousePosition = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
            selectedObject = hit.collider.gameObject;
    }*/

    void MoveObject()
    {
        if (selectedObject != null)
        {
            if (IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
            gameBehaviour.ShowUIAt(selectedObject.name);
            float zoffset = (camera.transform.position.z - selectedObject.transform.position.z) * -1.0f;
            float dragSpeed = zoffset * 0.001875f;
            Vector3 diff = (Input.mousePosition - lastMousePos) * dragSpeed;
            Vector3 newPosition = selectedObject.transform.position + diff;
            if (newPosition.y >= miny)
                selectedObject.transform.position = newPosition;
            lastMousePos = Input.mousePosition;
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