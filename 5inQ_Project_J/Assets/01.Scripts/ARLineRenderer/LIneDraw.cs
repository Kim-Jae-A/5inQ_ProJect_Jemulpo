using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDraw : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] LongLat;
    private List<float> Longitude = new List<float>();
    private List<float> Latitude = new List<float>();


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    //Json 데이터 호출 가능 여부 및 방법 확인용 코드
    void Start()
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
            // Longitude와 Latitude 리스트에서 Vector3로 만듭니다.
            Vector3 point = new Vector3(Longitude[i], Latitude[i], 0);

            // 변환된 Vector3를 LongLat 배열에 추가합니다.
            LongLat[i] = point;
        }

        lineRenderer.positionCount = Longitude.Count;
        lineRenderer.SetPositions(LongLat); // 라인 렌더러에 위치 설정
    }

}
