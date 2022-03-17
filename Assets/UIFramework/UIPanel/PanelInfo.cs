using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PanelInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
  /*  {
        get
        {
            return panelType.ToString()
        }
        set
        {
            UIPanelType type =  (UIPanelType)System.Enum.Parse(typeof(UIPanelType), value);
            panelType = type;
        }
    }
 */
    public string path;

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }
}
