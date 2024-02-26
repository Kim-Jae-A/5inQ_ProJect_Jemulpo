using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapUI_Enum : MonoBehaviour
{
    public MapUI_Enum_Interface.Type type; // �̳� Ÿ���� ����

    private TextAsset json;
    [SerializeField] private GameObject marker;
    private Image panel;
    [SerializeField] private GameObject naviPanel;

    [Header("��� ����")]
    public Text placeName;
    public Text placeInfo;
    public Text placeAddress;
    public Image placeImage;

    Button[] panelButton;
    public Image endPoint;

    private void Awake()
    {
        panel = GetComponent<Image>();

        // �̳� ������ ���� JSON ������ �Ľ�
        switch (type)
        {
            case MapUI_Enum_Interface.Type.Publicplaces:
                json = Resources.Load<TextAsset>("Json/POIData_Public");
                break;
            case MapUI_Enum_Interface.Type.Hospital:
                json = Resources.Load<TextAsset>("Json/POIData_Hospital");
                break;
            case MapUI_Enum_Interface.Type.PhotoZone:
                json = Resources.Load<TextAsset>("Json/POIData");
                break;
            case MapUI_Enum_Interface.Type.Docent:
                json = Resources.Load<TextAsset>("Json/POIData_Docent");
                break;
        }
#if UNITY_EDITOR
        LoadingMarker();
#endif
    }

    void OnButtonEnter(Button b)
    {
        // ���� �ۼ� ��ư �̺�Ʈ
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
        endPoint.transform.position = b.transform.position;
        endPoint.gameObject.SetActive(true);
        naviPanel.SetActive(true);
    }

    public void LoadingMarker()
    {
        LoadAndCreate(json);
        panelButton = panel.GetComponentsInChildren<Button>();

        foreach (Button b in panelButton)
        {
            //b.onClick.AddListener(delegate { OnButtonEnter(b); });
            b.onClick.AddListener(() => OnButtonEnter(b));
        }
    }
    private void LoadAndCreate(TextAsset jsonfile)
    {
        if (jsonfile != null)
        {
            AR_DataList ar_dataList = JsonUtility.FromJson<AR_DataList>(jsonfile.text); // JSON ������ �Ľ�

            // ���̽� ������ ���� ��ŭ ���� �� ���� �浵�� ���� ��ġ ��ȯ
            foreach (AR_POI ARZone_ in ar_dataList.ARzone_List)
            {
                CreateMarker(ARZone_);                
            }
        }
    }

    private void CreateMarker(AR_POI ARZone_poi)
    {
        if (ARZone_poi.Name != null)
        {
            GameObject a = Instantiate(marker);       
            MarkerInFo _marker = a.GetComponent<MarkerInFo>();
            _marker._name = ARZone_poi.Name;
            _marker._description = ARZone_poi.Info;
            _marker._Address = ARZone_poi.Address;
            _marker._latitude = ARZone_poi.latitude;
            _marker._longitude = ARZone_poi.longitude;
            _marker._imagepath = ARZone_poi.imagepath;
            Vector2 v = ConvertGeoToUnityCoordinate(Convert.ToDouble(_marker._latitude), Convert.ToDouble(_marker._longitude));
            a.transform.rotation = new Quaternion(0, 0, 0, 0);
            a.transform.position = new Vector3(v.x, v.y, 0);
            a.transform.SetParent(transform, false);
            a.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    // ���� �浵�� ���� ����Ƽ ��ǥ��� ġȯ
    private Vector2 ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // ���� ����, �浵
        double originLatitude = StaticMapManager.latitude;
        double originLongitude = StaticMapManager.longitude;

#if UNITY_EDITOR
        originLatitude = 37.713675f;
        originLongitude = 126.743572f;
#endif
        // ���� x, y
        double originX = 0;
        double originY = 0;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        double xRatio = 172238.37f;
        double yRatio = 265780.73f;

        double x = originX + (longitude - originLongitude) * xRatio;
        double y = originY + (latitude - originLatitude) * yRatio;

        return new Vector2((float)x, (float)y);
    }
}
