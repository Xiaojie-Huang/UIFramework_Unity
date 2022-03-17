using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {   
            if (canvasTransform == null)
                canvasTransform = GameObject.Find("Canvas").transform;
            return canvasTransform;
        }
    }

    //存储所有面板的路径
    private Dictionary<UIPanelType, string> PanelPathDict;

    //存储已经实例化的面板的BasePanel组件
    private Dictionary<UIPanelType, BasePanel> PanelDict;

    //根据面板类型得到对应面板
    private BasePanel GetPanel(UIPanelType PanelType)
    {
        if (PanelDict == null)
            PanelDict = new Dictionary<UIPanelType, BasePanel>();

        BasePanel panel;
        PanelDict.TryGetValue(PanelType, out panel);

        if(panel == null)
        {
            string path;
            PanelPathDict.TryGetValue(PanelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            PanelDict.Add(PanelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }


    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<PanelInfo> infoList;
    }

    private void ParseUIPanelTypeJson()
    {
        PanelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach(PanelInfo info in jsonObject.infoList)
        {
            PanelPathDict.Add(info.panelType, info.path);
        }
    }

    private Stack<BasePanel> PanelStack;

    public void PushPanel(UIPanelType PanelType)
    {
        if (PanelStack == null)
            PanelStack = new Stack<BasePanel>();

        //若栈内有其他页面，需要使其暂停交互
        if(PanelStack.Count > 0)
        {
            BasePanel topPanel = PanelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(PanelType);
        panel.OnEnter();
        PanelStack.Push(panel);
    }

    public void PopPanel()
    {
        if (PanelStack == null)
            PanelStack = new Stack<BasePanel>();
        if (PanelStack.Count <= 0)
            return;

        BasePanel topPanel = PanelStack.Peek();
        topPanel.OnExit();

        if (PanelStack.Count <= 0)
            return;

        BasePanel topPanel2 = PanelStack.Peek();
        topPanel2.OnResume();

    }


    public void Test()
    {
        string path;
        PanelPathDict.TryGetValue(UIPanelType.Bag, out path);
        Debug.Log(path);
    }
}
