using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragButtonBehaviour : WidgetBehaviour
{
    public GameObject dragButtonObject;

    Image dragButtonBackground;
    TextMeshProUGUI dragTextMesh;

    public override void OnStart()
    {
        dragButtonBackground = dragButtonObject.GetComponent<Image>();
        dragTextMesh = dragButtonObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnUnselect()
    {
        dragButtonBackground.color = Color.white;
        dragTextMesh.color = Color.black;
    }

    public override UIBehaviour.ActionType GetActionType()
    {
        return UIBehaviour.ActionType.DRAG;
    }

    public void OnDrag()
    {
        dragButtonBackground.color = Color.black;
        dragTextMesh.color = Color.white;
        uiManager.OnWidgetSelect(index);
        uiManager.SetControlMode(UIBehaviour.ActionType.DRAG);
    }
}
