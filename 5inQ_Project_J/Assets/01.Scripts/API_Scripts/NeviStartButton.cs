using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeviStartButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {       
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonEnter);
    }

    public void OnButtonEnter()
    {
        Direction5Manager.instance.OnNaviStartButtonEnter();
        Direction5Manager.instance.SomeFunction();
    }

}
