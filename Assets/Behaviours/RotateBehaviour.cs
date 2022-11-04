using TMPro;
using UnityEngine;
using UnityEngine.UI;

class RotateBehaviour : ButtonBehaviour
{
    Image buttonBackground;
    TextMeshProUGUI textMesh;
    SliderBehaviour slider;

    public override void OnStart()
    {
        buttonBackground = GetComponent<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        GameObject sliderObject = GameObject.Find("Slider");
        if (sliderObject != null)
            slider = sliderObject.GetComponent<SliderBehaviour>();
    }

    public override void OnThisButtonClick()
    {
        buttonBackground.color = Color.black;
        textMesh.color = Color.white;
        gameBehaviour.SetGameMode(GameBehaviour.GameMode.ROTATE);
        slider.ShowHide(true);               
    }

    public override void OnOtherButtonClick()
    {
        buttonBackground.color = Color.white;
        textMesh.color = Color.black;
        slider.ShowHide(false);
    }
}
