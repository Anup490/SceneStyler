using UnityEngine;

public abstract class Device
{
    protected Camera camera;
    protected GameBehaviour gameBehaviour;
    protected const float miny = -0.09f;
    protected GameObject selectedObject;

    public Device(GameBehaviour behaviour)
    {
        gameBehaviour = behaviour;
        camera = behaviour.GetCamera();
    }

    public abstract void OnUpdate();

    protected void RayCast(Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
            selectedObject = hit.collider.gameObject;
    }

    protected void Reset()
    {
        gameBehaviour.ShowHideUI(false);
        if (selectedObject != null)
            selectedObject = null;
    }
}
