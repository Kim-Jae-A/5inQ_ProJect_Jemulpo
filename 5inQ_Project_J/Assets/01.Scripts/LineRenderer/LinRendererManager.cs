using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public Transform XROrigin;
    private LineRenderer lineRenderer;

    // 위도와 경도
    private Vector3[] LongLat;
    private List<double> Longitude = new List<double>();
    private List<double> Latitude = new List<double>();
    private GPSmanager gpsManager;
    private GPSCoordinatesConverter converter;

    // AR 카메라 위치 변수
    public Vector3 ARCameraPosition { get; private set; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gameObject.AddComponent<GPSCoordinatesConverter>();
        converter = GetComponent<GPSCoordinatesConverter>();
        gameObject.AddComponent<GPSmanager>();
        gpsManager = GetComponent<GPSmanager>();
    }

    private void Start()
    {
        if (gpsManager == null || converter == null)
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
        for (int i = 0; i < Longitude.Count - 1 && i < Latitude.Count - 1; i++)
        {
            double drawLongitude = Longitude[i] - Longitude[i + 1];
            double drawLatitude = Latitude[i] - Latitude[i + 1];
            Vector3 position = new Vector3((float)drawLongitude * 100000, 0, (float)drawLatitude * 100000);
            // 변환된 Vector3를 LongLat 배열에 추가합니다.
            if (i > 0)
            {
                LongLat[i] = LongLat[i - 1] + position;
            }
            else
            {
                LongLat[i] = position;
            }
        }
        lineRenderer.positionCount = Longitude.Count - 1;
        lineRenderer.SetPositions(LongLat); // 라인 렌더러에 위치 설정

        // 첫 번째 위치를 ARCameraPosition에 저장
        ARCameraPosition = lineRenderer.GetPosition(0);
    }
}
