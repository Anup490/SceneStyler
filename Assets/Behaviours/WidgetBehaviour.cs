using UnityEngine;

public abstract class WidgetBehaviour : MonoBehaviour
{
    protected UIManager uiManager;
    protected int index;

    void Start()
    {
        uiManager = UIManager.Get();
        index = uiManager.RegisterWidget(this);
        OnStart();
    }

    public abstract void OnStart();

    public abstract void OnUnselect();

    public abstract UIManager.ActionType GetActionType();
}
