using UnityEngine;

public class GameBehaviour : MonoBehaviour, IDeviceCallback, ICameraCallback
{
    AssetBehaviour selectedAsset;
    AssetBehaviour zoomedAsset;
    DeviceHandler deviceHandler;
    UIManager uiManager;
    RayCastManager rayCastManager;
    CameraManager cameraManager;
    UIManager.ActionType mode = UIManager.ActionType.DRAG;
    bool allowInput = true;

    public void SetControlMode(UIManager.ActionType gameMode)
    {
        mode = gameMode;
        if (gameMode == UIManager.ActionType.ORBIT)
            cameraManager.OnOrbitMode();
    }

    public void RotateAsset(float yaw, float yawDisplay)
    {
        if (selectedAsset != null)
            selectedAsset.Rotate(new Vector3(0, yaw, 0), yawDisplay);
    }

    public void OnClick(Vector3 position)
    {
        if (allowInput && mode != UIManager.ActionType.ORBIT)
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
    }

    public void OnHold(Vector3 position, Vector2 axes)
    {
        if (allowInput && mode != UIManager.ActionType.ORBIT)
        {
            if (mode == UIManager.ActionType.DRAG && selectedAsset != null)
                selectedAsset.Displace(rayCastManager.GetTargetPosition(position, selectedAsset.transform.position));
            else if (mode == UIManager.ActionType.ZOOM)
                cameraManager.RotateCamera(axes);
        }
        else if (mode == UIManager.ActionType.ORBIT)
            cameraManager.OrbitCamera(position, selectedAsset.GetLookAtPosition());
    }

    public void OnRelease()
    {
        if (selectedAsset != null)
            selectedAsset.OnUnselect();
        cameraManager.OnRelease();
    }

    public MonoBehaviour GetBehaviour()
    {
        return this;
    }

    public void OnZoomIn()
    {
        zoomedAsset = selectedAsset;
        allowInput = true;
        uiManager.ShowHideWidgets(true, selectedAsset);
        uiManager.OnZoomIn(ZoomOut);
    }

    public void OnZoomOut()
    {
        zoomedAsset = null;
        uiManager.ShowHideWidgets(true, selectedAsset);
        allowInput = true;
    }

    DeviceHandler.Type IDeviceCallback.GetType()
    {
        return DeviceHandler.Type.GAME;
    }

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        deviceHandler = DeviceHandler.Get();
        deviceHandler.AddCallback(this);
        uiManager = UIManager.Get();
        uiManager.AddGame(this);
        rayCastManager = new RayCastManager(cam);
        cameraManager = new CameraManager(cam, this);
    }

    void Update()
    {
        deviceHandler.OnUpdate();
    }

    void ZoomIn()
    {
        if (mode == UIManager.ActionType.ZOOM && (!zoomedAsset || selectedAsset != zoomedAsset))
        {
            (Vector3 cameraTarget, bool isValid) = selectedAsset.GetCameraLandingPosition();
            if (isValid)
            {
                uiManager.ShowHideWidgets(false, null);
                allowInput = false;
                cameraManager.ZoomIn(cameraTarget, selectedAsset.GetLookAtPosition());
            }
        }
    }

    void ZoomOut()
    {
        uiManager.ShowHideWidgets(false, null);
        allowInput = false;
        cameraManager.ZoomOut();
    }
}