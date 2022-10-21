using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    Camera cam;
    GameObject selectedObject;
    GameObject canvas;
    Image image;
    TextMeshProUGUI textMesh;
    Vector3 lastMousePos;

    void Start()
    {
        cam = GetComponent<Camera>();
        InitUI();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (selectedObject == null)
                RayCast();
            else
                MoveObject();
        }
        else
        {
            lastMousePos = Vector3.zero;
            ResetSelectedObject();
            HideUI();
        }
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

    void RayCast()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            GameObject go = hit.collider.gameObject;
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                Material material = mr.materials[0];
                material.SetColor("_Color", Color.blue);
            }
            selectedObject = go;
        }
    }

    void MoveObject()
    {
        if (selectedObject != null)
        {
            if (IsZero(lastMousePos)) lastMousePos = Input.mousePosition;
            ShowUIAt(selectedObject.name);
            float zoffset = (cam.transform.position.z - selectedObject.transform.position.z) * -1.0f;
            float dragSpeed = zoffset * 0.001875f;
            Vector3 diff = (Input.mousePosition - lastMousePos) * dragSpeed;
            selectedObject.transform.position += diff;
            lastMousePos = Input.mousePosition;
        }
    }

    void ShowUIAt(string text)
    {
        int width = 50 + text.Length * 5;
        canvas.SetActive(true);
        textMesh.text = text;
        RectTransform rectTransformImage = image.rectTransform;
        rectTransformImage.position = Input.mousePosition;
        rectTransformImage.sizeDelta = new Vector2(width, 20);
        RectTransform rectTransformText = textMesh.rectTransform;
        rectTransformText.sizeDelta = new Vector2(width, 20);
    }

    void ResetSelectedObject()
    {
        if (selectedObject != null)
        {
            MeshRenderer mr = selectedObject.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                Material material = mr.materials[0];
                material.SetColor("_Color", Color.white);
            }
            selectedObject = null;
        }
    }

    void HideUI()
    {
        canvas.SetActive(false);
    }

    bool IsZero(Vector3 vec)
    {
        const float epsilon = 0.00001f;
        float diffx = vec.x - 0.0f;
        float diffy = vec.y - 0.0f;
        float diffz = vec.z - 0.0f;
        if (diffx < 0.0f) diffx *= -1.0f;
        if (diffy < 0.0f) diffy *= -1.0f;
        if (diffz < 0.0f) diffz *= -1.0f;
        return (diffx < epsilon) && (diffy < epsilon) && (diffz < epsilon);
    }
}
