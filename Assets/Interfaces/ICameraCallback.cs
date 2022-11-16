using UnityEngine;

public interface ICameraCallback
{
    MonoBehaviour GetBehaviour();
    void OnZoomIn();
    void OnZoomOut();
}
