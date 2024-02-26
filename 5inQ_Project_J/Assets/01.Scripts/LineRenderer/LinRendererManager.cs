using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public Transform XROrigin;
    private LineRenderer lineRenderer;
    public Text text;
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
    private void Update()
    {
        GameObject ARCamera = GameObject.Find("AR Session Origin");
        text.text = $"{ARCamera.transform.position}";
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
        GameObject ARCamera = GameObject.Find("AR Session Origin");
        // AR 카메라의 위치를 설정
        Vector3 newPosition = LongLat[0];
        newPosition.y = 2f; 
        ARCamera.transform.position = newPosition;

        // LinRendererManager에서 AR 카메라의 위치를 가져옵니다.
        Vector3 direction = LongLat[1] - ARCamera.transform.position;

        // 방향 벡터를 회전하여 타겟 방향으로 오브젝트를 회전시킵니다.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        ARCamera.transform.rotation = targetRotation;
    }
}
