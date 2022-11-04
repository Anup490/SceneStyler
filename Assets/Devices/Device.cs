using UnityEngine;

public abstract class Device
{
    protected GameBehaviour gameBehaviour;

    public Device(GameBehaviour behaviour)
    {
        gameBehaviour = behaviour;
    }

    public abstract void OnUpdate();
}
