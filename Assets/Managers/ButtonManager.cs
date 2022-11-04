using System.Collections.Generic;

class ButtonManager
{
    static ButtonManager manager;

    List<ButtonBehaviour> buttons = new List<ButtonBehaviour>();

    public static ButtonManager Get()
    {
        if (manager == null)
            manager = new ButtonManager();
        return manager;
    }

    private ButtonManager() { }

    public int Register(ButtonBehaviour button)
    {
        buttons.Add(button);
        return buttons.Count - 1;
    }

    public void OnClick(int index)
    {
        for (int i=0; i<buttons.Count; i++)
        {
            if (i != index)
                buttons[i].OnOtherButtonClick();
        }
    }
}
