using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public GameObject uiBehaviourObject;

    Camera cam;
    AssetBehaviour selectedAsset;
    Device device;
    UIBehaviour uiBehaviour;
    RayCastManager rayCastManager;
    GameMode mode = GameMode.DRAG;

    public enum GameMode
    {
        DRAG, ROTATE
    }

    public void SetGameMode(GameMode gameMode)
    {
        mode = gameMode;
    }

    public void RotateAsset(float yaw, float yawDisplay)
    {
        if (selectedAsset != null)
        {
            selectedAsset.transform.Rotate(0, yaw, 0);
            selectedAsset.yaw = yawDisplay;
        }
    }

    public void OnMouseClick(Vector3 position, bool onUI)
    {
        if (onUI)
            uiBehaviour.OnUIClick(position);
        else
        {
            selectedAsset = rayCastManager.DetectAsset(position, mode == GameMode.DRAG);
            if (selectedAsset != null)
            {
                uiBehaviour.SetSliderValue(selectedAsset.yaw);
                uiBehaviour.ShowHideSideBar(true, selectedAsset);
                device.UpdateSideBarVisibility(true);
            }
            else if (mode == GameMode.DRAG)
            {
                uiBehaviour.ShowHideSideBar(false, selectedAsset);
                device.UpdateSideBarVisibility(false);
            }
        }   
    }

    public void OnMouseDrag(Vector3 position)
    {
        if (mode == GameMode.DRAG && selectedAsset != null)
            selectedAsset.Displace(rayCastManager.GetTargetPosition(position, selectedAsset.transform.position));      
    }

    public void OnMouseRelease()
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
        device.OnUpdate();
    }

    void Init()
    {      
        device = (SystemInfo.deviceType == DeviceType.Handheld) ? new Android(this) : new Windows(this);
        uiBehaviour = uiBehaviourObject.GetComponent<UIBehaviour>();
        rayCastManager = new RayCastManager(cam);
    }
}