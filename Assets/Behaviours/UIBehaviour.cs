using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject dragButtonObject;
    public GameObject rotateButtonObject;
    public GameObject sliderObject;
    public GameObject sideBarObject;
    public GameObject rawImageObject;

    GameBehaviour gameBehaviour;
    Image dragButtonBackground;
    TextMeshProUGUI dragTextMesh;
    Image rotateButtonBackground;
    TextMeshProUGUI rotateTextMesh;
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
    }

    public void OnRotate()
    {
        rotateButtonBackground.color = Color.black;
        rotateTextMesh.color = Color.white;
        gameBehaviour.SetControlMode(GameBehaviour.ControlMode.ROTATE);
        ShowHideSlider(true);

        dragButtonBackground.color = Color.white;
        dragTextMesh.color = Color.black;
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

    void Start()
    {
        gameBehaviour = parentObject.GetComponent<GameBehaviour>();
        dragButtonBackground = dragButtonObject.GetComponent<Image>();
        dragTextMesh = dragButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        rotateButtonBackground = rotateButtonObject.GetComponent<Image>();
        rotateTextMesh = rotateButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        slider = sliderObject.GetComponent<Slider>();
        ShowHideSlider(false);
        sideBarManager = new SideBarManager(sideBarObject, rawImageObject);
    }

    void Update()
    {
        sideBarManager.OnUpdate(this);  
    }
}
