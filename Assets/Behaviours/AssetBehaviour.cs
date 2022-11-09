using System.Collections.Generic;
using UnityEngine;

abstract public class AssetBehaviour : MonoBehaviour
{
    public List<Texture> textures = new List<Texture>();
    public List<GameObject> children = new List<GameObject>();
    protected Vector3 deltaPosition;

    public float yaw{ get; set; }

    public void ApplyTextureAt(int index)
    {
        foreach (GameObject child in children)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            meshRenderer.material.mainTexture = textures[index];
        }
    }

    abstract public void Displace(Vector3 targetPosition);

    abstract public void OnUnselect();

    abstract public string GetDescription();
}
