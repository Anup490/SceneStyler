using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public GameObject sideBarObject;
    public GameObject gridItemObject;

    SideBarManager sideBarManager;
    UIManager uiManager;

    public void ShowHideSideBar(bool show, AssetBehaviour asset)
    {
        sideBarManager.ShowHide(show, asset);
    }

    public void OnUIClick(Vector3 worldPosition)
    {
        sideBarManager.OnSideBarClick(worldPosition);
    }

    void Start()
    {
        sideBarManager = new SideBarManager(sideBarObject, gridItemObject);
        uiManager = UIManager.Get();
        uiManager.AddUI(this);
    }

    void Update()
    {
        sideBarManager.OnUpdate(this);  
    }
}
