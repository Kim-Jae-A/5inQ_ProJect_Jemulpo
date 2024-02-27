using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeviStartButton : MonoBehaviour
{
    Button button;
    Direction5Manager direction5Manager;

    private void Awake()
    {       
        direction5Manager = Direction5Manager.instance.GetComponent<Direction5Manager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonEnter);
    }

    public void OnButtonEnter()
    {
        direction5Manager.OnNaviStartButtonEnter();
        direction5Manager.SomeFunction();
    }

}
