using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    Camera cam;
    GameObject canvas;
    Image image;
    TextMeshProUGUI textMesh;
    Device device;

    public void ShowUIAt(string text)
    {
        int width = 50 + text.Length * 5;
        ShowHideUI(true);
        textMesh.text = text;
        RectTransform rectTransformImage = image.rectTransform;
        rectTransformImage.sizeDelta = new Vector2(width, 20);
        RectTransform rectTransformText = textMesh.rectTransform;
        rectTransformText.sizeDelta = new Vector2(width, 20);
    }

    public void ShowHideUI(bool show)
    {
        if (canvas != null)
            canvas.SetActive(show);
    }

    public Camera GetCamera()
    {
        return cam;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        InitUI();
        InitDevice();
    }

    void Update()
    {
        device.OnUpdate();
    }

    void InitUI()
    {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (Object o in objects)
        {
            GameObject go = o as GameObject;
            if (go.name.Equals("Canvas"))
            {
                canvas = go;
                break;
            }
        }
        image = GetComponentInChildren<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        if (canvas != null)
            canvas.SetActive(false);
    }

    void InitDevice()
    {
        device = (SystemInfo.deviceType == DeviceType.Handheld) ? new Android(this) : new Windows(this);
    }
}
