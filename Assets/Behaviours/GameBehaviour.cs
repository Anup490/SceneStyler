using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameBehaviour : MonoBehaviour
{
    Camera cam;
    GameObject selectedObject;
    Image image;
    TextMeshProUGUI textMesh;
    Device device;
    GameMode mode = GameMode.DRAG;

    public enum GameMode
    {
        DRAG, ROTATE
    }

    public void SetGameMode(GameMode gameMode)
    {
        mode = gameMode;
    }

    public void OnDragStart(Vector3 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            selectedObject = hit.collider.gameObject;
            cursosPrevWorldPos = ray.origin;
        }
        else
        {
            selectedObject = null;
            cursosPrevWorldPos = new Vector3();
        }  
    }

    Vector3 cursosPrevWorldPos;

    public void OnDrag(Vector3 position)
    {
        if (mode == GameMode.DRAG && selectedObject != null)
        {
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector3 cursorDiff = ray.origin - cursosPrevWorldPos;
            cursosPrevWorldPos = ray.origin;
            if (hit.collider != null)
            {
                float fDistFromCam = selectedObject.transform.position.z - cam.transform.position.z;
                Vector3 vDistFromCam = new Vector3(0.0f, 0.0f, fDistFromCam);
                float cosine = GetCosine(ray.direction, vDistFromCam);
                float rayLength = fDistFromCam / cosine;
                Vector3 targetPosition = ray.direction * rayLength;
                targetPosition.y++;
                targetPosition.z = selectedObject.transform.position.z + (cursorDiff.y * 50.0f);               
                AssetBehaviour assetBehaviour = selectedObject.GetComponent<AssetBehaviour>();
                if (assetBehaviour != null)
                {
                    ShowUIAt(selectedObject.name);
                    assetBehaviour.Displace(targetPosition);
                }
            }
        }
    }

    float GetCosine(Vector3 v1, Vector3 v2)
    {
        float dot = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        float v1Len = (float)Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        float v2Len = (float)Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z);
        return dot / (v1Len * v2Len);
    }

    public void OnDragEnd()
    {
        ShowHideUI(false);
    }

    public void OnRotate(Vector3 diff)
    {
        if (mode == GameMode.ROTATE && selectedObject != null)
        {
            ShowUIAt(selectedObject.name);
            selectedObject.transform.Rotate(0, -diff.x, 0);
        }
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
        if (image != null)
            image.gameObject.SetActive(show);
    }
}