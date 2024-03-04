using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public GameObject GuidePrefab;
    public Transform ARCamera;
    private LineRenderer lineRenderer;
    public TextMeshPro textMeshPro;
    // 위도와 경도
    public Vector3[] LongLat;
    public List<int> pointIndex = new List<int>();
    public List<string> instructions = new List<string>();
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public static LinRendererManager instance;

    private List<GameObject> clones = new List<GameObject>(); // 생성된 클론들을 저장할 리스트

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
        CreateGuide();
    }
/// <summary>
/// 라인 렌더러가 실제랑 불일치하는 이유는 현실과 유니티 상의 거리 비가 다르기 때문이다.
/// 계상 편의상  Vector3 position에 10만을 곱했을 때 실제 거리와 20m정도 차이가 났다.
/// 이 코드를 사용하기 위해서는 9만이나 8만 등 곱하는 수의 크기를 줄이면서 현실과 유니티 상에서 일치하는 값을 찾아내서 곱해야한다.
/// <summary>
    void LineRenderDraw()
    {
        // JsonManager 클래스의 인스턴스를 통해 데이터에 접근
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];
            foreach (Guide guideInfo in firstTraFast.guide)
            {
                pointIndex.Add(guideInfo.pointIndex);
                instructions.Add(guideInfo.instructions);
            }
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

    void CreateGuide()
    {
        for (int i = 0; i < pointIndex.Count; i++)
        {
            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(GuidePrefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(transform);
            Vector3 newPosition = LongLat[pointIndex[i] + 1];
            newPosition.y = 2f; // y 좌표를 2로 수정
            clone.transform.position = newPosition;
            clones.Add(clone); // 생성된 클론을 리스트에 추가

            textMeshPro = clone.GetComponentInChildren<TextMeshPro>();
            if (textMeshPro != null)
            {
                textMeshPro.text = $"{instructions[i]}"; // 원하는 텍스트로 변경
            }
        }
    }

    void Update()
    {
        // AR 카메라의 Transform을 가져옵니다.
        Transform arCameraTransform = ARCamera.transform;

        // 생성된 클론들이 AR 카메라를 바라보도록 회전 조정
        foreach (GameObject clone in clones)
        {
            // 클론의 회전 각도를 AR 카메라의 앞쪽 방향과 같은 방향으로 설정합니다.
            clone.transform.rotation = Quaternion.LookRotation(arCameraTransform.forward, Vector3.up);
        }
    }
}
