using System.Collections.Generic;
using UnityEngine;

public class UIManager : IDeviceCallback
{
    public delegate void OnBack();

    static UIManager manager;

    List<WidgetBehaviour> widgets;
    UIBehaviour uiBehaviour;
    GameBehaviour gameBehaviour;
    DeviceHandler deviceHandler;

    public enum ActionType
    {
        DRAG, ROTATE, ZOOM, ORBIT
    }

    public static UIManager Get()
    {
        if (manager == null)
            manager = new UIManager();
        return manager;
    }

    UIManager()
    {
        widgets = new List<WidgetBehaviour>();
        deviceHandler = DeviceHandler.Get();
        deviceHandler.AddCallback(this);
    }

    public void AddUI(UIBehaviour behaviour)
    {
        uiBehaviour = behaviour;
    }

    public void AddGame(GameBehaviour behaviour)
    {
        gameBehaviour = behaviour;
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

    public void SetControlMode(UIManager.ActionType mode)
    {
        if (gameBehaviour != null)
            gameBehaviour.SetControlMode(mode);
    }

    public void NotifyRotation(float rotValue, float rotDisplayValue)
    {
        if (gameBehaviour != null)
            gameBehaviour.RotateAsset(rotValue, rotDisplayValue);
    }

    public void OnZoomIn(OnBack funcOnBack)
    {
        ZoomButtonBehaviour zoomButton = GetWidget(UIManager.ActionType.ZOOM) as ZoomButtonBehaviour;
        if (zoomButton)
            zoomButton.NotifyZoomAction(funcOnBack);
    }

    public void SetSliderValue(float val)
    {
        RotateButtonBehaviour rotateButton = GetWidget(UIManager.ActionType.ROTATE) as RotateButtonBehaviour;
        if (rotateButton)
            rotateButton.SetSliderValue(val);
    }

    public void ShowHideSideBar(bool show, AssetBehaviour asset)
    {
        if (uiBehaviour)
        {
            if (asset == null)
                uiBehaviour.ShowHideSideBar(false, null);
            else
                uiBehaviour.ShowHideSideBar(show, asset);
        }
    }

    public void ShowHideWidgets(bool show, AssetBehaviour asset)
    {
        foreach (WidgetBehaviour widget in widgets)
            widget.gameObject.SetActive(show);
    }

    public void OnClick(Vector3 position)
    {
        if (uiBehaviour != null)
            uiBehaviour.OnClick(position);
    }

    public void OnHold(Vector3 position) {}

    public void OnRelease() {}

    DeviceHandler.Type IDeviceCallback.GetType()
    {
        return DeviceHandler.Type.UI;
    }

    WidgetBehaviour GetWidget(ActionType actionType)
    {
        foreach (WidgetBehaviour widget in widgets)
        {
            if (widget.GetActionType() == actionType)
                return widget;
        }
        return null;
    }
}
