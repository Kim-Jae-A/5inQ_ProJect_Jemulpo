using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapUI_Enum : MonoBehaviour
{
    public MapUI_Enum_Interface.Type type;

    private TextAsset json;
    [SerializeField] private GameObject marker;
    private Image panel;
    [SerializeField] private GameObject naviPanel;

    [Header("장소 정보")]
    public Text placeName;
    public Text placeInfo;
    public Text placeAddress;
    public Image placeImage;

    Button[] panelButton;

    private void Awake()
    {
        panel = GetComponent<Image>();
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
        LoadAndCreate(json);        
    }
    private void Start()
    {
        panelButton = panel.GetComponentsInChildren<Button>();

        foreach (Button b in panelButton)
        {
            Debug.Log(b.name);
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

    private void LoadAndCreate(TextAsset jsonfile)
    {
        if (jsonfile != null)
        {
            AR_DataList ar_dataList = JsonUtility.FromJson<AR_DataList>(jsonfile.text);

            foreach (AR_POI ARZone_ in ar_dataList.ARzone_List)
            {
                CreateScrollviewContent(ARZone_);                
            }
        }
    }
    private void CreateScrollviewContent(AR_POI ARZone_poi)
    {
        if (ARZone_poi.Name != null)
        {
            GameObject a = Instantiate(marker);         
            a.transform.SetParent(transform);
            a.transform.position = transform.position;
            a.transform.rotation = new Quaternion(0, 0, 0, 0);
            a.transform.localScale = new Vector3(2, 2, 1);
            MarkerInFo _marker = a.GetComponent<MarkerInFo>();
            _marker._name = ARZone_poi.Name;
            _marker._description = ARZone_poi.Info;
            _marker._Address = ARZone_poi.Address;
            _marker._latitude = ARZone_poi.latitude;
            _marker._longitude = ARZone_poi.longitude;
            _marker._imagepath = ARZone_poi.imagepath;
        }
    }
}
