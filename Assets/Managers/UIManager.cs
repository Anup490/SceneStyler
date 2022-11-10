using System.Collections.Generic;

public class UIManager
{
    static UIManager manager;

    List<WidgetBehaviour> widgets;
    UIBehaviour uiBehaviour;

    public static UIManager Get(UIBehaviour behaviour)
    {
        if (manager == null)
            manager = new UIManager(behaviour);
        return manager;
    }

    UIManager(UIBehaviour behaviour)
    {
        widgets = new List<WidgetBehaviour>();
        uiBehaviour = behaviour;
    }

    public int RegisterWidget(WidgetBehaviour widget)
    {
        widgets.Add(widget);
        return widgets.Count - 1;
    }

    public void OnWidgetSelect(int index)
    {
        for (int i = 0; i < widgets.Count; i++)
        {
            if (i != index)
                widgets[i].OnUnselect();
        }
    }
    public void ShowHideOtherButtons(int index, bool show)
    {
        for (int i = 0; i < widgets.Count; i++)
        {
            if (i != index)
                widgets[i].gameObject.SetActive(show);
        }
    }

    public WidgetBehaviour GetWidget(UIBehaviour.ActionType actionType)
    {
        foreach (WidgetBehaviour widget in widgets)
        {
            if (widget.GetActionType() == actionType)
                return widget;
        }
        return null;
    }

    public void SetControlMode(UIBehaviour.ActionType mode)
    {
        uiBehaviour.SetControlMode(mode);
    }

    public void NotifyRotation(float rotValue, float rotDisplayValue)
    {
        uiBehaviour.NotifyRotation(rotValue, rotDisplayValue);
    }

    public void NotifyZoomOut()
    {
        uiBehaviour.NotifyZoomOut();
    }
}
