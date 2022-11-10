using System;
using UnityEngine;

public class GameBehaviour : MonoBehaviour, IDeviceCallback
{
    Camera cam;
    AssetBehaviour selectedAsset;
    DeviceHandler deviceHandler;
    UIManager uiManager;
    RayCastManager rayCastManager;
    UIManager.ActionType mode = UIManager.ActionType.DRAG;
    Vector3 originalPosition;
    Quaternion originalRotation;

    public void SetControlMode(UIManager.ActionType gameMode)
    {
        mode = gameMode;
    }

    public void RotateAsset(float yaw, float yawDisplay)
    {
        if (selectedAsset != null)
            selectedAsset.Rotate(new Vector3(0, yaw, 0), yawDisplay);
    }

    public void OnClick(Vector3 position)
    {
        selectedAsset = rayCastManager.DetectAsset(position, mode == UIManager.ActionType.DRAG);
        if (selectedAsset != null)
        {
            uiManager.SetSliderValue(selectedAsset.yaw);
            uiManager.ShowHideSideBar(true, selectedAsset);
            deviceHandler.UpdateSideBarVisibility(true);
            ZoomIn();
        }
        else if (mode == UIManager.ActionType.DRAG)
        {
            uiManager.ShowHideSideBar(false, selectedAsset);
            deviceHandler.UpdateSideBarVisibility(false);
        }
    }

    public void OnHold(Vector3 position)
    {
        if (mode == UIManager.ActionType.DRAG && selectedAsset != null)
            selectedAsset.Displace(rayCastManager.GetTargetPosition(position, selectedAsset.transform.position));
    }

    public void OnRelease()
    {
        if (selectedAsset != null)
            selectedAsset.OnUnselect();            
    }

    DeviceHandler.Type IDeviceCallback.GetType()
    {
        return DeviceHandler.Type.GAME;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        deviceHandler = DeviceHandler.Get();
        deviceHandler.AddCallback(this);
        uiManager = UIManager.Get();
        uiManager.AddGame(this);
        rayCastManager = new RayCastManager(cam);
        originalPosition = cam.transform.position;
        originalRotation = cam.transform.rotation;
    }

    void Update()
    {
        deviceHandler.OnUpdate();
    }

    void ZoomIn()
    {
        if (mode == UIManager.ActionType.ZOOM)
        {
            (Vector3 cameraTarget, bool isValid) = selectedAsset.GetCameraLandingPosition();
            if (isValid)
            {
                cam.transform.position = cameraTarget;
                Vector3 camToAsset = selectedAsset.transform.position - cameraTarget;
                float cosine = Utils.GetCosine(camToAsset, cam.transform.forward);
                float angle = (float)Math.Acos(cosine);
                cam.transform.Rotate(angle, 0.0f, 0.0f);
                uiManager.OnZoomIn(ZoomOut);
            }
        }
    }

    void ZoomOut()
    {
        cam.transform.position = originalPosition;
        cam.transform.rotation = originalRotation;
    }
}