using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MakerManager : MonoBehaviour
{
    public GameObject[] panel;
    public GameObject naviPanel;

    [Header("장소 정보")]
    public Text placeName;
    public Text placeInfo;
    public Text placeAddress;
    public Image placeImage;

    Button[] panel0_Button;
    Button[] panel1_Button;
    Button[] panel2_Button;

    private void Awake()
    {
        panel0_Button = panel[0].GetComponentsInChildren<Button>(); // 공공장소
        panel1_Button = panel[1].GetComponentsInChildren<Button>(); // 병원
        panel2_Button = panel[2].GetComponentsInChildren<Button>(); // 도슨트

        foreach (Button b in panel0_Button)
        {
            b.onClick.AddListener(delegate { OnButtonEnter(b); });
        }
        foreach (Button b in panel1_Button)
        {
            b.onClick.AddListener(delegate { OnButtonEnter(b); });
        }
        foreach (Button b in panel2_Button)
        {
            b.onClick.AddListener(delegate { OnButtonEnter(b); });
        }
    }

    void OnButtonEnter(Button b)
    {
        MarkerInFo marker = b.GetComponent<MarkerInFo>();
        placeName.text = marker._name;
        placeInfo.text = marker._description;
        placeAddress.text = marker._Address;
        Direction5Manager._endlatitude = marker._latitude;
        Direction5Manager._endlongitude = marker._longitude;
        Debug.Log(Direction5Manager._endlatitude);
        Debug.Log(Direction5Manager._endlongitude);

        Sprite sp = Resources.Load<Sprite>(marker._imagepath);
        if (sp != null)
        {
            placeImage.sprite = sp;
        }
        naviPanel.SetActive(true);
    }
}
