using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoomButtonBehaviour : WidgetBehaviour
{
    public GameObject zoomButtonObject;
    public GameObject orbitButtonObject;

    Image zoomButtonBackground;
    TextMeshProUGUI zoomTextMesh;
    bool isZoomedIn;
    UIManager.OnBack funcOnBack;

    Image orbitButtonBackground;
    bool isOrbiting;
    TextMeshProUGUI orbitTextMesh;

    public override void OnStart()
    {
        zoomButtonBackground = zoomButtonObject.GetComponent<Image>();
        zoomTextMesh = zoomButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        orbitButtonBackground = orbitButtonObject.GetComponent<Image>();
        orbitTextMesh = orbitButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        orbitButtonObject.SetActive(false);
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
        if (isZoomedIn && isOrbiting)
        {
            isOrbiting = false;
            zoomTextMesh.text = "BACK";
            orbitButtonObject.SetActive(true);
            uiManager.SetControlMode(UIManager.ActionType.ZOOM);
        }
        else if (isZoomedIn)
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
        orbitButtonObject.SetActive(true);
        isZoomedIn = true;
        funcOnBack = fOnBack;
    }

    public void OnBack()
    {
        zoomTextMesh.text = "ZOOM";
        uiManager.ShowHideOtherButtons(index, true);
        orbitButtonObject.SetActive(false);
        isZoomedIn = false;
        if (funcOnBack != null)
            funcOnBack();
    }

    public void OnOrbit()
    {
        zoomTextMesh.text = "STOP";
        orbitButtonObject.SetActive(false);
        isOrbiting = true;
        uiManager.SetControlMode(UIManager.ActionType.ORBIT);
    }
}
