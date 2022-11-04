using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    GameBehaviour gameBehaviour;
    Slider slider; 
    float prevSliderValue;
    float sliderVal;

    public void OnSliderChanged()
    {
        float valDiff = slider.value - prevSliderValue;
        gameBehaviour.OnSliderChange(valDiff * 360.0f, slider.value);
        prevSliderValue = slider.value;
    }

    public void UpdateYaw(float val)
    {
        sliderVal = val;
        if (gameObject.activeSelf)
            slider.SetValueWithoutNotify(sliderVal);
    }

    public void ShowHide(bool show)
    {
        gameObject.SetActive(show);
        if (show)
            slider.SetValueWithoutNotify(sliderVal);
    }

    private void Start()
    {
        gameBehaviour = GetComponentInParent<GameBehaviour>();
        slider = GetComponent<Slider>();
    }
}
