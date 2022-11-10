using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoomButtonBehaviour : WidgetBehaviour
{
    public GameObject zoomButtonObject;

    Image zoomButtonBackground;
    TextMeshProUGUI zoomTextMesh;
    bool isZoomedIn;
    UIManager.OnBack funcOnBack;

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

    public override UIManager.ActionType GetActionType()
    {
        return UIManager.ActionType.ZOOM;
    }

    public void OnZoom()
    {
        if (isZoomedIn)
            OnBack();
        else
        {
            zoomButtonBackground.color = Color.black;
            zoomTextMesh.color = Color.white;
            uiManager.OnWidgetSelect(index);
            uiManager.SetControlMode(UIManager.ActionType.ZOOM);
        }
    }

    public void NotifyZoomAction(UIManager.OnBack fOnBack)
    {
        zoomTextMesh.text = "BACK";
        uiManager.ShowHideOtherButtons(index, false);
        isZoomedIn = true;
        funcOnBack = fOnBack;
    }

    public void OnBack()
    {
        zoomTextMesh.text = "ZOOM";
        uiManager.ShowHideOtherButtons(index, true);
        isZoomedIn = false;
        if (funcOnBack != null)
            funcOnBack();
    }
}
