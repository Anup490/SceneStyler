using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class GridManager
{
    public delegate void OnItemSelect(GameObject image, int index);

    readonly Vector3 offset = new Vector3(25.0f, 25.0f, 0.0f);
    readonly float itemGap = Screen.width / 16.0f;

    GameObject parent;
    GameObject target;
    OnItemSelect funcItemSelect;
    List<GameObject> currentItems;
    Vector3 min;
    Vector3 max;

    public GridManager(GameObject parent, GameObject target, OnItemSelect funcItemSelect)
    {
        this.parent = parent; 
        this.target = target;
        this.funcItemSelect = funcItemSelect;
        currentItems = new List<GameObject>();
        target.SetActive(false);
    }

    public void DrawGrid(List<Texture> textures)
    {
        if (textures.Count == 0) return;
        Vector3 position = parent.transform.position - new Vector3(itemGap, 0.0f, 0.0f);
        min.x = position.x - offset.x;
        min.y = position.y + offset.y;
        int i = 0;
        foreach (Texture texture in textures)
        {
            GameObject imageObject = Object.Instantiate(target, position, target.transform.rotation, parent.transform);
            imageObject.SetActive(true);
            RawImage image = imageObject.GetComponent<RawImage>();
            image.texture = texture;
            currentItems.Add(imageObject);
            position.x += itemGap;
            i++;
            if (i < 3)
                max.x = imageObject.transform.position.x + offset.x;
            if (i > 0 && (i % 3 == 0))
            {
                position.y -= itemGap;
                position.x -= (itemGap * 3.0f);
                max.x = imageObject.transform.position.x + offset.x;
            }
        }
        max.y = currentItems[textures.Count - 1].transform.position.y - offset.y;
    }

    public void RemoveGrid()
    {
        foreach (GameObject item in currentItems)
            Object.Destroy(item);
        min = Vector3.zero;
        max = Vector3.zero;
        currentItems.Clear();
    }

    public void OnGridSelect(Vector3 position)
    {
        Vector3 itemMin;
        Vector3 itemMax;
        int index = 0;
        foreach (GameObject item in currentItems)
        {
            itemMin.x = item.transform.position.x - offset.x;
            itemMin.y = item.transform.position.x + offset.y;
            itemMax.x = item.transform.position.x + offset.x;
            itemMax.y = item.transform.position.y - offset.y;
            if (position.x > itemMin.x && position.x < itemMax.x && position.y < itemMin.y && position.y > itemMax.y)
            {
                funcItemSelect(item, index);
                break;
            }
            index++;
        }
    }

    public bool IsInGrid(Vector3 position)
    {
        return position.x > min.x && position.x < max.x && position.y < min.y && position.y > max.y;
    }
}
