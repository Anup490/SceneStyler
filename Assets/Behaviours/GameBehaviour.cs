using System;
using UnityEngine;

public class GameBehaviour : MonoBehaviour, IDeviceCallback
{
    public GameObject uiBehaviourObject;

    Camera cam;
    AssetBehaviour selectedAsset;
    DeviceHandler deviceHandler;
    UIBehaviour uiBehaviour;
    RayCastManager rayCastManager;
    ControlMode mode = ControlMode.DRAG;
    Vector3 originalPosition;
    Quaternion originalRotation;
    bool hasZoomedIn;

    public enum ControlMode
    {
        DRAG, ROTATE, ZOOM
    }

    public void SetControlMode(ControlMode gameMode)
    {
        mode = gameMode;
    }

    public void RotateAsset(float yaw, float yawDisplay)
    {
        if (selectedAsset != null)
            selectedAsset.Rotate(new Vector3(0, yaw, 0), yawDisplay);
    }

    public void OnWorldClick(Vector3 position)
    {
        selectedAsset = rayCastManager.DetectAsset(position, mode == ControlMode.DRAG);
        if (selectedAsset != null)
        {
            uiBehaviour.SetSliderValue(selectedAsset.yaw);
            uiBehaviour.ShowHideSideBar(true, selectedAsset);
            deviceHandler.UpdateSideBarVisibility(true);
            ZoomIn();
        }
        else if (mode == ControlMode.DRAG)
        {
            uiBehaviour.ShowHideSideBar(false, selectedAsset);
            deviceHandler.UpdateSideBarVisibility(false);
        }
    }

    public void OnUIClick(Vector3 position)
    {
        uiBehaviour.OnUIClick(position);
    }

    public void OnDrag(Vector3 position)
    {
        if (mode == ControlMode.DRAG && selectedAsset != null)
            selectedAsset.Displace(rayCastManager.GetTargetPosition(position, selectedAsset.transform.position));
    }

    public void OnRelease()
    {
        if (selectedAsset != null)
            selectedAsset.OnUnselect();            
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        deviceHandler = DeviceHandler.Get(this);
        uiBehaviour = uiBehaviourObject.GetComponent<UIBehaviour>();
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
        if (mode == ControlMode.ZOOM && !hasZoomedIn)
        {
            (Vector3 cameraTarget, bool isValid) = selectedAsset.GetCameraLandingPosition();
            if (isValid)
            {
                cam.transform.position = cameraTarget;
                Vector3 camToAsset = selectedAsset.transform.position - cameraTarget;
                float cosine = Utils.GetCosine(camToAsset, cam.transform.forward);
                float angle = (float)Math.Acos(cosine);
                cam.transform.Rotate(angle, 0.0f, 0.0f);
                hasZoomedIn = true;
                uiBehaviour.OnZoomIn();
            }
        }
    }

    public void ZoomOut()
    {
        cam.transform.position = originalPosition;
        cam.transform.rotation = originalRotation;
        hasZoomedIn = false;
    }
}