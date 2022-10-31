using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    Camera cam;
    GameObject canvas;
    GameObject selectedObject;
    Image image;
    TextMeshProUGUI textMesh;
    Device device;

    public void OnDragStart(Vector3 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
            selectedObject = hit.collider.gameObject;
    }

    public void OnDrag(Vector3 positionDiff)
    {
        if (selectedObject != null)
        {
            ShowUIAt(selectedObject.name);
            float zoffset = (cam.transform.position.z - selectedObject.transform.position.z) * -1.0f;
            float dragSpeed = zoffset * 0.001875f;
            Vector3 diff = positionDiff * dragSpeed;
            AssetBehaviour assetBehaviour = selectedObject.GetComponent<AssetBehaviour>();
            if (assetBehaviour != null)
                assetBehaviour.Displace(diff);
        }
    }

    public void OnDragEnd()
    {
        ShowHideUI(false);
        if (selectedObject != null)
            selectedObject = null;
    }

    public void OnRotate(Vector3 diff)
    {
        if (selectedObject != null)
            selectedObject.transform.Rotate(0, -diff.x, 0);
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        Init();
    }

    void Update()
    {
        device.OnUpdate();
    }

    void Init()
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
        ShowHideUI(false);
        device = (SystemInfo.deviceType == DeviceType.Handheld) ? new Android(this) : new Windows(this);
    }

    void ShowUIAt(string text)
    {
        int width = 50 + text.Length * 5;
        ShowHideUI(true);
        textMesh.text = text;
        RectTransform rectTransformImage = image.rectTransform;
        rectTransformImage.sizeDelta = new Vector2(width, 20);
        RectTransform rectTransformText = textMesh.rectTransform;
        rectTransformText.sizeDelta = new Vector2(width, 20);
    }

    void ShowHideUI(bool show)
    {
        if (canvas != null)
            canvas.SetActive(show);
    }
}
