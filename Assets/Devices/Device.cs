using UnityEngine;

public abstract class Device
{
    protected GameBehaviour gameBehaviour;
    protected bool isSideBarVisible;

    public Device(GameBehaviour behaviour)
    {
        gameBehaviour = behaviour;
    }

    public abstract void OnUpdate();

    public void UpdateSideBarVisibility(bool visibility)
    {
        isSideBarVisible = visibility;
    }

    protected bool IsNotTouchingSideBar(Vector3 cursorPos)
    {
        Vector2 screenPos = Utils.ToScreenSpace(cursorPos);
        float limit = Screen.width * 0.75f;
        if (isSideBarVisible)
            return cursorPos.x < limit;
        return true;
    }
}
