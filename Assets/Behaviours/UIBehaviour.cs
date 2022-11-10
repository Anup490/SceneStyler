using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public GameObject parentObject;

    public GameObject dragButtonObject;
    public GameObject rotateButtonObject;
    public GameObject zoomButtonObject;
    public GameObject sliderObject;

    public GameObject sideBarObject;
    public GameObject gridItemObject;

    GameBehaviour gameBehaviour;
    
    Image dragButtonBackground;
    TextMeshProUGUI dragTextMesh;

    Image rotateButtonBackground;
    TextMeshProUGUI rotateTextMesh;

    Image zoomButtonBackground;
    TextMeshProUGUI zoomTextMesh;
    bool isZoomedIn;

    Slider slider;
    float prevSliderValue;
    
    SideBarManager sideBarManager;

    public void OnDrag()
    {
        dragButtonBackground.color = Color.black;
        dragTextMesh.color = Color.white;
        gameBehaviour.SetControlMode(GameBehaviour.ControlMode.DRAG);

        rotateButtonBackground.color = Color.white;
        rotateTextMesh.color = Color.black;
        ShowHideSlider(false);

        zoomButtonBackground.color = Color.white;
        zoomTextMesh.color = Color.black;
    }

    public void OnRotate()
    {
        rotateButtonBackground.color = Color.black;
        rotateTextMesh.color = Color.white;
        gameBehaviour.SetControlMode(GameBehaviour.ControlMode.ROTATE);
        ShowHideSlider(true);

        dragButtonBackground.color = Color.white;
        dragTextMesh.color = Color.black;

        zoomButtonBackground.color = Color.white;
        zoomTextMesh.color = Color.black;
    }

    public void OnZoom()
    {
        if (!isZoomedIn)
        {
            zoomButtonBackground.color = Color.black;
            zoomTextMesh.color = Color.white;
            gameBehaviour.SetControlMode(GameBehaviour.ControlMode.ZOOM);

            dragButtonBackground.color = Color.white;
            dragTextMesh.color = Color.black;

            rotateButtonBackground.color = Color.white;
            rotateTextMesh.color = Color.black;
            ShowHideSlider(false);
        }
        else
        {
            zoomTextMesh.text = "ZOOM";
            rotateButtonObject.SetActive(true);
            dragButtonObject.SetActive(true);
            isZoomedIn = false;
            gameBehaviour.ZoomOut();
        }
    }

    public void OnSliderChanged()
    {
        float valDiff = slider.value - prevSliderValue;
        gameBehaviour.RotateAsset(valDiff * 360.0f, slider.value);
        prevSliderValue = slider.value;
    }

    public void SetSliderValue(float val)
    {
        prevSliderValue = val;
        if (gameObject.activeSelf)
            slider.SetValueWithoutNotify(prevSliderValue);
    }

    public void ShowHideSlider(bool show)
    {
        sliderObject.SetActive(show);
        if (show && slider != null)
            slider.SetValueWithoutNotify(prevSliderValue);
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

    public void OnZoomIn()
    {
        zoomTextMesh.text = "BACK";
        dragButtonObject.SetActive(false);
        rotateButtonObject.SetActive(false);
        isZoomedIn = true;
    }


    void Start()
    {
        gameBehaviour = parentObject.GetComponent<GameBehaviour>();
        
        dragButtonBackground = dragButtonObject.GetComponent<Image>();
        dragTextMesh = dragButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        
        rotateButtonBackground = rotateButtonObject.GetComponent<Image>();
        rotateTextMesh = rotateButtonObject.GetComponentInChildren<TextMeshProUGUI>();

        zoomButtonBackground = zoomButtonObject.GetComponent<Image>();
        zoomTextMesh = zoomButtonObject.GetComponentInChildren<TextMeshProUGUI>();

        slider = sliderObject.GetComponent<Slider>();
        ShowHideSlider(false);
        sideBarManager = new SideBarManager(sideBarObject, gridItemObject);
    }

    void Update()
    {
        sideBarManager.OnUpdate(this);  
    }
}
