using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapUI_Enum : MonoBehaviour
{
    public MapUI_Enum_Interface.Type type; // 이넘 타입의 종류

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
    public Image endPoint;
    public Text endText;

    private void Awake()
    {
        panel = GetComponent<Image>();

        // 이넘 종류에 따른 JSON 데이터 파싱
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

    /// <summary>
    /// JSON 데이터에서 받아온 데이터를 바탕으로 클릭시 해당 버튼이 가지고 있는 정보를 바탕으로 상세설명 창의 요소들을 변경 및 상세 정보 창 팝업
    /// </summary>
    /// <param name="b">누른 버튼의 정보</param>
    void OnButtonEnter(Button b)
    {
        MarkerInFo marker = b.GetComponent<MarkerInFo>();
        placeName.text = marker._name;
        placeInfo.text = marker._description;
        placeAddress.text = marker._Address;
        endText.text = marker._name;
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

    /// <summary>
    /// 생성된 POI Marker 버튼에 클릭 이벤트 할당
    /// </summary>
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

    /// <summary>
    /// POI JSON에서 파싱 후 생성함수 호출
    /// </summary>
    /// <param name="jsonfile">제이슨파일</param>
    private void LoadAndCreate(TextAsset jsonfile)
    {
        if (jsonfile != null)
        {
            AR_DataList ar_dataList = JsonUtility.FromJson<AR_DataList>(jsonfile.text); // JSON 데이터 파싱

            // 제이슨 데이터 갯수 만큼 생성 및 위도 경도에 따라 위치 변환
            foreach (AR_POI ARZone_ in ar_dataList.ARzone_List)
            {
                CreateMarker(ARZone_);                
            }
        }
    }

    /// <summary>
    /// 파싱된 데이터를 바탕으로 Marker 생성 및 각 마커 버튼에 정보 입력
    /// </summary>
    /// <param name="ARZone_poi">제이슨 파싱한 컨테이너</param>
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

    /// <summary>
    /// 위도 경도를 유니티 좌표계로 치환하는 식
    /// </summary>
    /// <param name="latitude">위도</param>
    /// <param name="longitude">경도</param>
    /// <returns>입력받은 위도 경도를 바탕으로 치환한 Vector2 값</returns>
    private Vector2 ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // 기준 위도, 경도
        double originLatitude = StaticMapManager.latitude;
        double originLongitude = StaticMapManager.longitude;

#if UNITY_EDITOR
        originLatitude = 37.713675f;
        originLongitude = 126.743572f;
#endif
        // 기준 x, y
        double originX = 0;
        double originY = 0;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 172238.37f;
        double yRatio = 265780.73f;

        double x = originX + (longitude - originLongitude) * xRatio;
        double y = originY + (latitude - originLatitude) * yRatio;

        return new Vector2((float)x, (float)y);
    }
}
