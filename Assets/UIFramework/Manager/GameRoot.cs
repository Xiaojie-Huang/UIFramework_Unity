using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
