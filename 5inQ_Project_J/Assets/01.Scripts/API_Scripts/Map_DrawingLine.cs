using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Map_DrawingLine : MonoBehaviour
{

    public GameObject lineObj;
    public GameObject line;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = lineObj.GetComponent<LineRenderer>();
    }

    public void OnButtonEnter()
    {
        if (JsonManager.instance != null && JsonManager.instance.data != null && JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];

            // 위에 3줄 코드 입력후 불러올 데이터 foreach문으로 확인 or 출력

            GameObject a;

            // Path 데이터 확인
            foreach (var point in firstTraFast.path)
            {

                Vector2 v = ConvertGeoToUnityCoordinate(point[1], point[0]);
                a = Instantiate(line);
                a.transform.rotation = new Quaternion(0, 0, 0, 0);
                a.transform.position = new Vector3(v.x, 0 , v.y);
                a.transform.SetParent(lineObj.transform, false);
                a.transform.localScale = new Vector3(10, 10, 10);
            }
            a = Instantiate(line);
            a.transform.rotation = new Quaternion(0, 0, 0, 0);
            a.transform.position = new Vector3(0, 0, 0);
            a.transform.SetParent(lineObj.transform, false);
            a.transform.localScale = new Vector3(10, 10, 10);

            int childCount = lineObj.transform.childCount; // 자식 객체의 수를 구합니다.

            lineRenderer.positionCount = childCount -1; // 라인의 점 개수를 자식 객체의 수로 설정합니다.

            for (int i = 0; i < lineObj.transform.childCount -1; i++)
            {
                lineRenderer.SetPosition(i, lineObj.transform.GetChild(i).position); // 각 점의 위치를 자식 객체의 위치로 설정합니다.
            }
        }
    }

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
