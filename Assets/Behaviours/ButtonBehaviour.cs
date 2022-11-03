using UnityEngine;

abstract class ButtonBehaviour : MonoBehaviour
{
    protected GameBehaviour gameBehaviour;  

    ButtonManager buttonManager;
    int buttonIndex;

    public void OnClick()
    {
        buttonManager.OnClick(buttonIndex);
        OnThisButtonClick();
    }

    public abstract void OnStart();

    public abstract void OnThisButtonClick();

    public abstract void OnOtherButtonClick();

    private void Start()
    {
        gameBehaviour = GetComponentInParent<GameBehaviour>();
        buttonManager = ButtonManager.Get();
        buttonIndex = buttonManager.Register(this);
        OnStart();
    }
}
