using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoomButtonBehaviour : WidgetBehaviour
{
    public GameObject zoomButtonObject;

    Image zoomButtonBackground;
    TextMeshProUGUI zoomTextMesh;
    bool isZoomedIn;

    public override void OnStart()
    {
        zoomButtonBackground = zoomButtonObject.GetComponent<Image>();
        zoomTextMesh = zoomButtonObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnUnselect()
    {
        zoomButtonBackground.color = Color.white;
        zoomTextMesh.color = Color.black;
    }

    public override UIBehaviour.ActionType GetActionType()
    {
        return UIBehaviour.ActionType.ZOOM;
    }

    public void OnZoom()
    {
        if (isZoomedIn)
            OnZoomOut();
        else
        {
            zoomButtonBackground.color = Color.black;
            zoomTextMesh.color = Color.white;
            uiManager.OnWidgetSelect(index);
            uiManager.SetControlMode(UIBehaviour.ActionType.ZOOM);
        }
    }

    public void OnZoomIn()
    {
        zoomTextMesh.text = "BACK";
        uiManager.ShowHideOtherButtons(index, false);
        isZoomedIn = true;
    }

    public void OnZoomOut()
    {
        zoomTextMesh.text = "ZOOM";
        uiManager.ShowHideOtherButtons(index, true);
        isZoomedIn = false;
        uiManager.NotifyZoomOut();
    }
}
