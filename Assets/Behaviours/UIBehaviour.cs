using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject sideBarObject;
    public GameObject gridItemObject;

    GameBehaviour gameBehaviour;
    SideBarManager sideBarManager;
    UIManager uiManager;

    public enum ActionType
    {
        DRAG, ROTATE, ZOOM
    }

    public void OnZoomIn()
    {
        ZoomButtonBehaviour zoomButton = uiManager.GetWidget(ActionType.ZOOM) as ZoomButtonBehaviour;
        if (zoomButton)
            zoomButton.OnZoomIn();
    }

    public void SetSliderValue(float val)
    {
        RotateButtonBehaviour rotateButton = uiManager.GetWidget(ActionType.ROTATE) as RotateButtonBehaviour;
        if (rotateButton)
            rotateButton.SetSliderValue(val);
    }

    public void SetControlMode(ActionType mode)
    {
        gameBehaviour.SetControlMode(mode);
    }

    public void NotifyRotation(float rotValue, float rotDisplayValue)
    {
        gameBehaviour.RotateAsset(rotValue, rotDisplayValue);
    }

    public void NotifyZoomOut()
    {
        gameBehaviour.ZoomOut();
    }

    public void ShowHideSideBar(bool show, AssetBehaviour asset)
    {
        if (asset == null)
            sideBarManager.ShowHide(false, null);
        else       
            sideBarManager.ShowHide(show, asset);
    }

    public void OnUIClick(Vector3 worldPosition)
    {
        sideBarManager.OnSideBarClick(worldPosition);
    }

    void Start()
    {
        gameBehaviour = parentObject.GetComponent<GameBehaviour>();
        sideBarManager = new SideBarManager(sideBarObject, gridItemObject);
        uiManager = UIManager.Get(this);
    }

    void Update()
    {
        sideBarManager.OnUpdate(this);  
    }
}
