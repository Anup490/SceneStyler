using UnityEngine;

public abstract class WidgetBehaviour : MonoBehaviour
{
    public GameObject canvas;
    protected UIManager uiManager;
    protected int index;

    void Start()
    {
        UIBehaviour uiBehaviour = canvas.GetComponent<UIBehaviour>();
        uiManager = UIManager.Get(uiBehaviour);
        index = uiManager.RegisterWidget(this);
        OnStart();
    }

    public abstract void OnStart();

    public abstract void OnUnselect();

    public abstract UIBehaviour.ActionType GetActionType();
}
