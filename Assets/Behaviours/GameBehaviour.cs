using System.Collections;
using UnityEngine;

public class GameBehaviour : MonoBehaviour, IDeviceCallback
{
    Camera cam;
    AssetBehaviour selectedAsset;
    AssetBehaviour previousAsset;
    DeviceHandler deviceHandler;
    UIManager uiManager;
    RayCastManager rayCastManager;
    UIManager.ActionType mode = UIManager.ActionType.DRAG;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalForward;
    bool allowInput = true;
    Vector3 originalCursorPosition = Vector3.zero;

    public void SetControlMode(UIManager.ActionType gameMode)
    {
        mode = gameMode;
    }

    public void RotateAsset(float yaw, float yawDisplay)
    {
        if (selectedAsset != null)
            selectedAsset.Rotate(new Vector3(0, yaw, 0), yawDisplay);
    }

    public void OnClick(Vector3 position)
    {
        if (allowInput && mode != UIManager.ActionType.ORBIT)
        {
            selectedAsset = rayCastManager.DetectAsset(position, mode == UIManager.ActionType.DRAG);
            if (selectedAsset != null)
            {
                uiManager.SetSliderValue(selectedAsset.yaw);
                uiManager.ShowHideSideBar(true, selectedAsset);
                deviceHandler.UpdateSideBarVisibility(true);
                ZoomIn();
            }
            else if (mode == UIManager.ActionType.DRAG)
            {
                uiManager.ShowHideSideBar(false, selectedAsset);
                deviceHandler.UpdateSideBarVisibility(false);
            }
        }
    }

    public void OnHold(Vector3 position)
    {
        if (allowInput && mode != UIManager.ActionType.ORBIT)
        {
            if (mode == UIManager.ActionType.DRAG && selectedAsset != null)
                selectedAsset.Displace(rayCastManager.GetTargetPosition(position, selectedAsset.transform.position));
            else if (mode == UIManager.ActionType.ZOOM)
                RotateCamera();
        }
        else if (mode == UIManager.ActionType.ORBIT)
            OrbitCamera(position);
    }

    public void OnRelease()
    {
        if (selectedAsset != null)
            selectedAsset.OnUnselect();
        originalCursorPosition = Vector3.zero;
    }

    DeviceHandler.Type IDeviceCallback.GetType()
    {
        return DeviceHandler.Type.GAME;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        deviceHandler = DeviceHandler.Get();
        deviceHandler.AddCallback(this);
        uiManager = UIManager.Get();
        uiManager.AddGame(this);
        rayCastManager = new RayCastManager(cam);
        originalPosition = cam.transform.position;
        originalRotation = cam.transform.rotation;
        originalForward = cam.transform.forward;
    }

    void Update()
    {
        deviceHandler.OnUpdate();
    }

    void ZoomIn()
    {
        if (mode == UIManager.ActionType.ZOOM && (!previousAsset || selectedAsset != previousAsset))
        {
            (Vector3 cameraTarget, bool isValid) = selectedAsset.GetCameraLandingPosition();
            if (isValid)
            {
                uiManager.ShowHideWidgets(false, null);
                allowInput = false;
                StartCoroutine(MoveToTarget(cameraTarget));
            }
        }
    }

    void RotateCamera()
    {
        if (previousAsset)
        {
            float sensitivity = 20.0f;
            float yaw = Input.GetAxis("Mouse X") * -sensitivity;
            float pitch = Input.GetAxis("Mouse Y") * sensitivity;
            Vector3 newRotation = transform.eulerAngles - new Vector3(pitch, yaw, 0.0f);
            if (newRotation.x <= 89.0f || newRotation.x >= 271.0f)
                transform.eulerAngles = newRotation;
        }
    }

    IEnumerator MoveToTarget(Vector3 cameraTarget)
    {
        Vector3 cameraToTarget = cameraTarget - cam.transform.position;
        Vector3 cameraToTargetNormal = cameraToTarget.normalized;
        Vector3 lookAtPosition = selectedAsset.GetLookAtPosition();
        for (float i = 0.0f; i <= cameraToTarget.magnitude; i += 0.05f)
        {
            cam.transform.position += cameraToTargetNormal * 0.05f;
            cam.transform.forward = (lookAtPosition - cam.transform.position).normalized;
            yield return new WaitForSeconds(0.01f);
        }
        cam.transform.position = cameraTarget;
        cam.transform.forward = (lookAtPosition - cameraTarget).normalized;
        previousAsset = selectedAsset;
        allowInput = true;
        uiManager.ShowHideWidgets(true, selectedAsset);
        uiManager.OnZoomIn(ZoomOut);
    }

    void ZoomOut()
    {
        uiManager.ShowHideWidgets(false, null);
        allowInput = false;
        StartCoroutine(MoveToOriginalPosition());
    }

    IEnumerator MoveToOriginalPosition()
    {
        Vector3 cameraToTarget = originalPosition - cam.transform.position;
        Vector3 cameraToTargetNormal = cameraToTarget.normalized;
        float totalAngle = Quaternion.Angle(originalRotation, cam.transform.rotation);
        float iterations = cameraToTarget.magnitude / 0.05f;
        float angleIteration = totalAngle / iterations;
        float angle = 360.0f - totalAngle;
        Vector3 axis = Vector3.Cross(cam.transform.forward, originalForward);
        for (float i = 0.0f; i <= cameraToTarget.magnitude; i += 0.05f)
        {
            cam.transform.position += cameraToTargetNormal * 0.05f;
            if (angle > 0.0f)
            {
                cam.transform.rotation = Quaternion.AngleAxis(angle, axis);
                angle += angleIteration;
            }
            yield return new WaitForSeconds(0.01f);
        }
        cam.transform.position = originalPosition;
        cam.transform.rotation = originalRotation;
        previousAsset = null;
        uiManager.ShowHideWidgets(true, selectedAsset);
        allowInput = true;
    }

    void OrbitCamera(Vector3 position)
    {
        float diff = position.x - originalCursorPosition.x;
        Vector3 lookAtPosition = selectedAsset.GetLookAtPosition();
        Vector3 lookAtDirection = cam.transform.position - lookAtPosition;
        Vector3 rotatedlookAtDirection;
        rotatedlookAtDirection = Quaternion.AngleAxis(Utils.IsZero(originalCursorPosition) ? 0 : diff, Vector3.up) * lookAtDirection;
        Vector3 offset = rotatedlookAtDirection - lookAtDirection;
        cam.transform.position += offset;
        cam.transform.forward = (lookAtPosition - cam.transform.position).normalized;
        originalCursorPosition = position;
    }
}