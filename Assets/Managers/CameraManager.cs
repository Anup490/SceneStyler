using System.Collections;
using UnityEngine;

public class CameraManager
{
    ICameraCallback callback;
    Camera camera;
    MonoBehaviour behaviour;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalForward;
    Vector3 originalCursorPosition = Vector3.zero;
    Vector3 newForward;

    public CameraManager(Camera cam, ICameraCallback mCallback)
    {
        callback = mCallback;
        behaviour = mCallback.GetBehaviour();
        camera = cam;
        originalPosition = camera.transform.position;
        originalRotation = camera.transform.rotation;
        originalForward = camera.transform.forward;
    }

    public void ZoomIn(Vector3 cameraTarget, Vector3 lookAtPosition)
    {
        behaviour.StartCoroutine(MoveToTarget(cameraTarget, lookAtPosition));
    }

    public void RotateCamera(Vector2 axes)
    {
        float sensitivity = 20.0f;
        float yaw = axes.x * -sensitivity;
        float pitch = axes.y * sensitivity;
        Vector3 newRotation = camera.transform.eulerAngles - new Vector3(pitch, yaw, 0.0f);
        if (newRotation.x <= 89.0f || newRotation.x >= 271.0f)
            camera.transform.eulerAngles = newRotation;
    }

    public void OnRelease()
    {
        originalCursorPosition = Vector3.zero;
    }

    public void ZoomOut()
    {
        behaviour.StartCoroutine(MoveToOriginalPosition());
    }

    public void OnOrbitMode()
    {
        camera.transform.forward = newForward;
    }

    public void OrbitCamera(Vector3 cursorPosition, Vector3 lookAtPosition)
    {
        float diff = cursorPosition.x - originalCursorPosition.x;
        Vector3 lookAtDirection = camera.transform.position - lookAtPosition;
        Vector3 rotatedlookAtDirection = Quaternion.AngleAxis(Utils.IsZero(originalCursorPosition) ? 0 : diff, Vector3.up) * lookAtDirection;
        Vector3 offset = rotatedlookAtDirection - lookAtDirection;
        camera.transform.position += offset;
        camera.transform.forward = (lookAtPosition - camera.transform.position).normalized;
        originalCursorPosition = cursorPosition;
    }

    IEnumerator MoveToTarget(Vector3 cameraTarget, Vector3 lookAtPosition)
    {
        Vector3 cameraToTarget = cameraTarget - camera.transform.position;
        Vector3 cameraToTargetNormal = cameraToTarget.normalized;
        for (float i = 0.0f; i <= cameraToTarget.magnitude; i += 0.05f)
        {
            camera.transform.position += cameraToTargetNormal * 0.05f;
            camera.transform.forward = (lookAtPosition - camera.transform.position).normalized;
            yield return new WaitForSeconds(0.01f);
        }
        camera.transform.position = cameraTarget;
        camera.transform.forward = (lookAtPosition - cameraTarget).normalized;
        newForward = camera.transform.forward;
        callback.OnZoomIn();
    }

    IEnumerator MoveToOriginalPosition()
    {
        Vector3 cameraToTarget = originalPosition - camera.transform.position;
        Vector3 cameraToTargetNormal = cameraToTarget.normalized;
        float totalAngle = Quaternion.Angle(originalRotation, camera.transform.rotation);
        float iterations = cameraToTarget.magnitude / 0.05f;
        float angleIteration = totalAngle / iterations;
        float angle = 360.0f - totalAngle;
        Vector3 axis = Vector3.Cross(camera.transform.forward, originalForward);
        for (float i = 0.0f; i <= cameraToTarget.magnitude; i += 0.05f)
        {
            camera.transform.position += cameraToTargetNormal * 0.05f;
            if (angle > 0.0f)
            {
                camera.transform.rotation = Quaternion.AngleAxis(angle, axis);
                angle += angleIteration;
            }
            yield return new WaitForSeconds(0.01f);
        }
        camera.transform.position = originalPosition;
        camera.transform.rotation = originalRotation;
        newForward = Vector3.zero;
        callback.OnZoomOut();
    }
}
