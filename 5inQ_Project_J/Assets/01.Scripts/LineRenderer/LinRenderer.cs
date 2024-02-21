using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LinRenderer : MonoBehaviour
{
    public Transform XROrigin;
    private LineRenderer lineRenderer;


    // 위도와 경도
    private Vector3[] LongLat;
    private List<float> Longitude = new List<float>();
    private List<float> Latitude = new List<float>();
    private GPSmanager gpsManagr;
    private GPSCoordinatesConverter converter;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gameObject.AddComponent<GPSCoordinatesConverter>();
        converter = GetComponent<GPSCoordinatesConverter>();
        gameObject.AddComponent<GPSmanager>();
        gpsManagr = GetComponent<GPSmanager>();

    }
    private void Start()
    {

        if (gpsManagr == null || converter == null)
        {
            Debug.LogError("GPS manager or converter is not initialized.");
            return;
        }
        LineRenderDraw();
    }
    private void Update()
    {
    }

    void LineRenderDraw()
    {
        Vector2 location = GPSmanager.GetLocation();
        Longitude.Add(location.x);
        Latitude.Add(location.x);
        // JsonManager 클래스의 인스턴스를 통해 데이터에 접근
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];

            // Path 데이터 확인
            foreach (var point in firstTraFast.path)
            {
                Longitude.Add(point[0]);
                Latitude.Add(point[1]);
            }
        }
        LongLat = new Vector3[Longitude.Count];
        for (int i = 0; i < Longitude.Count && i < Latitude.Count; i++)
        {
            // 위도와 경도를 유니티 좌표로 변환
            Vector3 unityCoordinates = converter.ConvertGPSToUnityCoordinates(Latitude[i], Longitude[i]);
            // 유니티 좌표를 화면 좌표로 변환
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(XROrigin.TransformPoint(unityCoordinates));
            // 변환된 Vector3를 LongLat 배열에 추가합니다.
            LongLat[i] = screenPoint;
        }
        lineRenderer.positionCount = Longitude.Count;
        lineRenderer.SetPositions(LongLat); // 라인 렌더러에 위치 설정
    }
}