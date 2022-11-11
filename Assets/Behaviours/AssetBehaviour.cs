using System.Collections.Generic;
using UnityEngine;

abstract public class AssetBehaviour : MonoBehaviour
{
    public List<Texture> textures = new List<Texture>();
    public List<GameObject> children = new List<GameObject>();
    public GameObject cameraLanding;
    public Vector3 lookAtOffset;
    public string materialName;

    protected Vector3 deltaPosition;

    public float yaw{ get; set; }

    public void ApplyTextureAt(int index)
    {
        foreach (GameObject child in children)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            meshRenderer.material.mainTexture = textures[index];
            if (materialName != null && !materialName.Equals(""))
            {
                foreach (Material material in meshRenderer.materials)
                {
                    if (material.name.Contains(materialName))
                    {
                        material.SetTexture(materialName, textures[index]);
                        break;
                    }
                }
            }
        }
    }

    public (Vector3, bool) GetCameraLandingPosition()
    {
        if (cameraLanding != null)
            return (cameraLanding.transform.position, true);
        return (Vector3.zero, false);
    }    

    abstract public void Displace(Vector3 targetPosition);

    abstract public void Rotate(Vector3 targetRotation, float yawDisplay);

    abstract public string GetDescription();

    abstract public Vector3 GetLookAtPosition();

    abstract public void OnUnselect();
}
