using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : BasePanel
{
    public CanvasGroup MainMenuGroup;


    public void Start()
    {
        MainMenuGroup = GetComponent<CanvasGroup>();
    }

    public override void OnPause()
    {
        MainMenuGroup.blocksRaycasts = false;    
    }

    public override void OnResume()
    {
        MainMenuGroup.blocksRaycasts = true;
    }

    public void PushPanel(string PanelType)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), PanelType);
        UIManager.Instance.PushPanel(panelType);
    }
}
