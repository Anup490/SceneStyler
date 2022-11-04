using UnityEngine;

class RayCastManager
{
    Camera cam;
    Vector3 cursosPrevWorldPos;

    public RayCastManager(Camera camera)
    {
        cam = camera;
    }

    public AssetBehaviour DetectAsset(Vector3 cursorPosition, bool resetOnFail)
    {
        Ray ray = cam.ScreenPointToRay(cursorPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            AssetBehaviour behaviour = hit.collider.gameObject.GetComponent<AssetBehaviour>();
            if (behaviour != null)
                cursosPrevWorldPos = ray.origin;
            else if (resetOnFail)
                cursosPrevWorldPos = new Vector3();
            return behaviour;
        }
        return null;
    }

    public Vector3 GetTargetPosition(Vector3 cursorPosition, Vector3 assetPosition)
    {
        Ray ray = cam.ScreenPointToRay(cursorPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 cursorDiff = ray.origin - cursosPrevWorldPos;
        cursosPrevWorldPos = ray.origin;
        if (hit.collider != null)
        {
            float fDistFromCam = assetPosition.z - cam.transform.position.z;
            Vector3 vDistFromCam = new Vector3(0.0f, 0.0f, fDistFromCam);
            float cosine = Utils.GetCosine(ray.direction, vDistFromCam);
            float rayLength = fDistFromCam / cosine;
            Vector3 targetPosition = ray.direction * rayLength;
            targetPosition.y++;
            targetPosition.z = assetPosition.z + (cursorDiff.y * 50.0f);
            return targetPosition;
        }
        return Vector3.zero;
    }
}
