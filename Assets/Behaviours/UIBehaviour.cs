using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    Image labelImage;
    TextMeshProUGUI labelTextMesh;

    GameBehaviour gameBehaviour;
    Image dragButtonBackground;
    TextMeshProUGUI dragTextMesh;

    Image rotateButtonBackground;
    TextMeshProUGUI rotateTextMesh;

    GameObject sliderObject;
    Slider slider;

    float prevSliderValue;

    void Start()
    {
        labelImage = GetComponentInChildren<Image>();
        labelTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        ShowHideLabel(false);
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
    }

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
        gameBehaviour.OnSliderChange(valDiff * 360.0f, slider.value);
        prevSliderValue = slider.value;
    }

    public void ShowLabelAt(string text)
    {
        int width = 50 + text.Length * 5;
        ShowHideLabel(true);
        labelTextMesh.text = text;
        RectTransform rectTransformImage = labelImage.rectTransform;
        rectTransformImage.sizeDelta = new Vector2(width, 20);
        RectTransform rectTransformText = labelTextMesh.rectTransform;
        rectTransformText.sizeDelta = new Vector2(width, 20);
    }

    public void ShowHideLabel(bool show)
    {
        if (labelImage != null)
            labelImage.gameObject.SetActive(show);
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
}
