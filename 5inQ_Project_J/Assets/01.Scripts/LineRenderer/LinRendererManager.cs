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
    public static LinRendererManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // 클래스의 인스턴스를 할당
        }
        else
        {
            Destroy(gameObject); // 이미 다른 인스턴스가 존재하면 이 객체를 파괴
            return;
        }
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        LineRenderDraw();   
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
        for (int i = 0; i < Longitude.Count; i++)
        {
            double drawLongitude = Longitude[i] - Longitude[0];
            double drawLatitude = Latitude[i] - Latitude[0];
            Vector3 position = new Vector3((float)drawLongitude * 100000, 0, (float)drawLatitude * 100000);
            LongLat[i] = position;
        }
        lineRenderer.positionCount = Longitude.Count;
        lineRenderer.SetPositions(LongLat); // 라인 렌더러에 위치 설정
    }
}

