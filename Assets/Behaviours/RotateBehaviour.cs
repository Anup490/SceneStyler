using TMPro;
using UnityEngine;
using UnityEngine.UI;

class RotateBehaviour : ButtonBehaviour
{
    Image buttonBackground;
    TextMeshProUGUI textMesh;

    public override void OnStart()
    {
        buttonBackground = GetComponent<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnThisButtonClick()
    {
        buttonBackground.color = Color.black;
        textMesh.color = Color.white;
        gameBehaviour.SetGameMode(GameBehaviour.GameMode.ROTATE);
    }

    public override void OnOtherButtonClick()
    {
        buttonBackground.color = Color.white;
        textMesh.color = Color.black;
    }
}
