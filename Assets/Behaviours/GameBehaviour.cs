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

    public enum ControlMode
    {
        DRAG, ROTATE
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
        Init();
    }

    void Update()
    {
        deviceHandler.OnUpdate();
    }

    void Init()
    {      
        deviceHandler = DeviceHandler.Get(this);
        uiBehaviour = uiBehaviourObject.GetComponent<UIBehaviour>();
        rayCastManager = new RayCastManager(cam);
    }
}