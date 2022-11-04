using TMPro;
using UnityEngine;
using UnityEngine.UI;

class UIManager
{
    Image image;
    TextMeshProUGUI textMesh;
    SliderBehaviour slider;

    public UIManager(GameObject main)
    {
        image = main.GetComponentInChildren<Image>();
        textMesh = main.GetComponentInChildren<TextMeshProUGUI>();
        slider = main.GetComponentInChildren<SliderBehaviour>();
        slider.ShowHide(false);
        ShowHideLabel(false);
    }

    public void ShowLabelAt(string text)
    {
        int width = 50 + text.Length * 5;
        ShowHideLabel(true);
        textMesh.text = text;
        RectTransform rectTransformImage = image.rectTransform;
        rectTransformImage.sizeDelta = new Vector2(width, 20);
        RectTransform rectTransformText = textMesh.rectTransform;
        rectTransformText.sizeDelta = new Vector2(width, 20);
    }

    public void ShowHideLabel(bool show)
    {
        if (image != null)
            image.gameObject.SetActive(show);
    }

    public void SetSliderValue(float value)
    {
        slider.UpdateYaw(value);
    }
}
