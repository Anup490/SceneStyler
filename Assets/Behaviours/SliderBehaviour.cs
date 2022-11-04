using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    GameBehaviour gameBehaviour;
    Slider slider; 
    float prevSliderValue;

    public void OnSliderChanged()
    {
        float valDiff = slider.value - prevSliderValue;
        gameBehaviour.OnSliderChange(valDiff * 360.0f, slider.value);
        prevSliderValue = slider.value;
    }

    public void UpdateYaw(float val)
    {
        prevSliderValue = val;
        if (gameObject.activeSelf)
            slider.SetValueWithoutNotify(prevSliderValue);
    }

    public void ShowHide(bool show)
    {
        gameObject.SetActive(show);
        if (show)
            slider.SetValueWithoutNotify(prevSliderValue);
    }

    private void Start()
    {
        gameBehaviour = GetComponentInParent<GameBehaviour>();
        slider = GetComponent<Slider>();
    }
}
