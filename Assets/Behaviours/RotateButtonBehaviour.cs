using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotateButtonBehaviour : WidgetBehaviour
{
    public GameObject rotateButtonObject;
    public GameObject sliderObject;

    Image rotateButtonBackground;
    TextMeshProUGUI rotateTextMesh;
    Slider slider;
    float prevSliderValue;

    public override void OnStart()
    {
        rotateButtonBackground = rotateButtonObject.GetComponent<Image>();
        rotateTextMesh = rotateButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        slider = sliderObject.GetComponent<Slider>();
        ShowHideSlider(false);
    }

    public override void OnUnselect()
    {
        rotateButtonBackground.color = Color.white;
        rotateTextMesh.color = Color.black;
        ShowHideSlider(false);
    }

    public override UIBehaviour.ActionType GetActionType()
    {
        return UIBehaviour.ActionType.ROTATE;
    }

    public void OnRotate()
    {
        rotateButtonBackground.color = Color.black;
        rotateTextMesh.color = Color.white;
        uiManager.OnWidgetSelect(index);
        uiManager.SetControlMode(UIBehaviour.ActionType.ROTATE);
        ShowHideSlider(true);
    }

    public void OnSliderChanged()
    {
        float valDiff = slider.value - prevSliderValue;
        uiManager.NotifyRotation(valDiff * 360.0f, slider.value);
        prevSliderValue = slider.value;
    }

    public void SetSliderValue(float val)
    {
        prevSliderValue = val;
        if (gameObject.activeSelf)
            slider.SetValueWithoutNotify(prevSliderValue);
    }

    void ShowHideSlider(bool show)
    {
        sliderObject.SetActive(show);
        if (show && slider != null)
            slider.SetValueWithoutNotify(prevSliderValue);
    }
}
