using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    GameBehaviour gameBehaviour;
    Image dragButtonBackground;
    TextMeshProUGUI dragTextMesh;

    Image rotateButtonBackground;
    TextMeshProUGUI rotateTextMesh;

    GameObject sliderObject;
    Slider slider;

    float prevSliderValue;

    SideBarManager sideBarManager;

    public void OnDrag()
    {
        dragButtonBackground.color = Color.black;
        dragTextMesh.color = Color.white;
        gameBehaviour.SetGameMode(GameBehaviour.GameMode.DRAG);

        rotateButtonBackground.color = Color.white;
        rotateTextMesh.color = Color.black;
        ShowHideSlider(false);
    }

    public void OnRotate()
    {
        rotateButtonBackground.color = Color.black;
        rotateTextMesh.color = Color.white;
        gameBehaviour.SetGameMode(GameBehaviour.GameMode.ROTATE);
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
        gameBehaviour = GetComponentInParent<GameBehaviour>();
        GameObject dragButtonObject = GameObject.Find("DragButton");
        dragButtonBackground = dragButtonObject.GetComponent<Image>();
        dragTextMesh = dragButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        GameObject rotateButtonObject = GameObject.Find("RotateButton");
        rotateButtonBackground = rotateButtonObject.GetComponent<Image>();
        rotateTextMesh = rotateButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        sliderObject = GameObject.Find("Slider");
        slider = sliderObject.GetComponent<Slider>();
        ShowHideSlider(false);
        sideBarManager = new SideBarManager(GameObject.Find("SideBar"));
    }

    void Update()
    {
        sideBarManager.OnUpdate(this);  
    }
}
