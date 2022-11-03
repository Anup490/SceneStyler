using TMPro;
using UnityEngine;
using UnityEngine.UI;

class DragBehaviour : ButtonBehaviour
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
        gameBehaviour.SetGameMode(GameBehaviour.GameMode.DRAG);
    }

    public override void OnOtherButtonClick()
    {
        buttonBackground.color = Color.white;
        textMesh.color = Color.black;
    }
}
