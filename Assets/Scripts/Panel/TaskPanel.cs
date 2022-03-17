using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : BasePanel
{
    public CanvasGroup TaskGroup;

    public void Start()
    {
        TaskGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if(TaskGroup == null)
            TaskGroup = GetComponent<CanvasGroup>();
        TaskGroup.alpha = 1;
        TaskGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        TaskGroup.alpha = 0;
        TaskGroup.blocksRaycasts = false;
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}
