using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public Transform ARCamera;
    private LineRenderer lineRenderer;
    // 위도와 경도
    public Vector3[] LongLat;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        LineRenderDraw();
        UpdateARCameraPosition();     
    }
    void LineRenderDraw()
    {
        // JsonManager 클래스의 인스턴스를 통해 데이터에 접근
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];
            Start startPoint = JsonManager.instance.data.route.trafast[0].summary.start;
            Goal goalPoint = JsonManager.instance.data.route.trafast[0].summary.goal;
            List<float> startLocation = startPoint.location;
            List<float> goalLocation = goalPoint.location;
            Longitude.Add(startLocation[0]);
            Latitude.Add(startLocation[1]);
            // Path 데이터 확인
            foreach (var point in firstTraFast.path)
            {
                Longitude.Add(point[0]);
                Latitude.Add(point[1]);
            }
            Longitude.Add(goalLocation[0]);
            Latitude.Add(goalLocation[1]);
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
        lineRenderer.positionCount = Longitude.Count-1;
        lineRenderer.SetPositions(LongLat); // 라인 렌더러에 위치 설정
    }

    void UpdateARCameraPosition()
    {
        // AR 카메라가 바라보는 방향을 설정합니다.
        Vector3 cameraForward = LongLat[2] - LongLat[0];
        ARCamera.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        // AR 카메라의 위치를 AR 카메라 화면의 중앙에 LongLat[0]가 보이도록 조정합니다.
        // AR 카메라의 위치와 방향을 역으로 계산하여 조정합니다.
        Vector3 cameraPosition = LongLat[0] - cameraForward.normalized;
        ARCamera.position = cameraPosition;

        // AR 카메라의 y 값을 2로 보정합니다.
        Vector3 newPosition = ARCamera.position;
        newPosition.y = 2f;
        ARCamera.position = newPosition;
    }
}

