using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class SideBarManager
{
    bool isSideBarVisible;
    bool showSideBar;
    bool sideBarRunning;
    RectTransform rectTransform;
    TextMeshProUGUI textMesh;
    AssetBehaviour selectedAsset;
    GridManager gridManager;

    readonly float maxWidth;

    public SideBarManager(GameObject sideBarObject)
    {
        Image background = sideBarObject.GetComponent<Image>();
        textMesh = sideBarObject.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = "";
        rectTransform = background.rectTransform;
        rectTransform.sizeDelta = new Vector2(0.0f, rectTransform.sizeDelta.y);
        maxWidth = Screen.width / 4.0f;
        GameObject rawImageObject = GameObject.Find("RawImage");
        gridManager = new GridManager(sideBarObject, rawImageObject, OnGridItemSelect);
    }

    public void OnGridItemSelect(GameObject image, int index)
    {
        selectedAsset.ApplyTextureAt(index);
    }

    public void ShowHide(bool show, AssetBehaviour asset)
    {
        selectedAsset = asset;
        showSideBar = show;
    }

    public void OnUpdate(MonoBehaviour behaviour)
    {
        if (!sideBarRunning)
        {
            if (showSideBar && !isSideBarVisible)
            {
                sideBarRunning = true;
                behaviour.StartCoroutine(ShowSideBar());
            }
            else if (!showSideBar && isSideBarVisible)
            {
                sideBarRunning = true;
                behaviour.StartCoroutine(HideSideBar());
            }
            else if (showSideBar && isSideBarVisible)
            {
                textMesh.text = selectedAsset.gameObject.name;
                gridManager.RemoveGrid();
                gridManager.DrawGrid(selectedAsset.textures);
            }
        }
    }

    public void OnSideBarClick(Vector3 position)
    {
        if (gridManager.IsInGrid(position))
            gridManager.OnGridSelect(position);
    }

    IEnumerator ShowSideBar()
    {
        for (float i = 0.0f; i < maxWidth; i += 10.0f)
        {
            yield return new WaitForSeconds(0.001f);
            rectTransform.sizeDelta = new Vector2(i, rectTransform.sizeDelta.y);
        }
        rectTransform.position -= new Vector3(maxWidth / 2.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.001f);
        rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
        textMesh.text = selectedAsset.gameObject.name;
        isSideBarVisible = true;
        sideBarRunning = false;
        gridManager.DrawGrid(selectedAsset.textures);
    }

    IEnumerator HideSideBar()
    {
        textMesh.text = "";
        gridManager.RemoveGrid();
        rectTransform.position += new Vector3(maxWidth / 2.0f, 0.0f, 0.0f);
        for (float i = maxWidth; i > 0.0f; i -= 10.0f)
        {
            yield return new WaitForSeconds(0.001f); 
            rectTransform.sizeDelta = new Vector2(i, rectTransform.sizeDelta.y);
        }
        yield return new WaitForSeconds(0.001f);
        rectTransform.sizeDelta = new Vector2(0.0f, rectTransform.sizeDelta.y);
        isSideBarVisible = false;
        sideBarRunning = false;
        selectedAsset = null;
    }
}
