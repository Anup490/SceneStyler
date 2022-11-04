using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    Camera cam;
    GameObject selectedObject;
    Image image;
    TextMeshProUGUI textMesh;
    Device device;
    GameMode mode = GameMode.DRAG;
    Vector3 cursosPrevWorldPos;
    SliderBehaviour slider;

    public enum GameMode
    {
        DRAG, ROTATE
    }

    public void SetGameMode(GameMode gameMode)
    {
        mode = gameMode;
    }

    public void OnSliderChange(float yaw, float sliderVal)
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(0, yaw, 0);
            AssetBehaviour assetBehaviour = selectedObject.GetComponent<AssetBehaviour>();
            if (assetBehaviour != null)
                assetBehaviour.yaw = sliderVal;          
        }
    }

    public void OnMouseClick(Vector3 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            GameObject gObject = hit.collider.gameObject;
            AssetBehaviour assetBehaviour = gObject.GetComponent<AssetBehaviour>();
            if (assetBehaviour != null)
            { 
                selectedObject = gObject;
                cursosPrevWorldPos = ray.origin;
                ShowUIAt(selectedObject.name);
                slider.UpdateYaw(assetBehaviour.yaw);
            }
            else if (mode == GameMode.DRAG)
            {
                selectedObject = null;
                cursosPrevWorldPos = new Vector3();
                ShowHideUI(false);
            }
        }
    }

    public void OnMouseDrag(Vector3 position)
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
                float cosine = Utils.GetCosine(ray.direction, vDistFromCam);
                float rayLength = fDistFromCam / cosine;
                Vector3 targetPosition = ray.direction * rayLength;
                targetPosition.y++;
                targetPosition.z = selectedObject.transform.position.z + (cursorDiff.y * 50.0f);               
                AssetBehaviour assetBehaviour = selectedObject.GetComponent<AssetBehaviour>();
                if (assetBehaviour != null)
                    assetBehaviour.Displace(targetPosition);
            }
        }
    }

    public void OnMouseRelease()
    {
        if (selectedObject != null)
        {
            AssetBehaviour assetBehaviour = selectedObject.GetComponent<AssetBehaviour>();
            if (assetBehaviour != null)
                assetBehaviour.OnUnselect();     
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
        GameObject sliderObject = GameObject.Find("/Main Camera/Canvas/Slider");
        slider = sliderObject.GetComponent<SliderBehaviour>();
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