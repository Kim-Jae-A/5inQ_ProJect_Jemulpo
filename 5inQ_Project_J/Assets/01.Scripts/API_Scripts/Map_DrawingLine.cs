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

            // ���� 3�� �ڵ� �Է��� �ҷ��� ������ foreach������ Ȯ�� or ���

            GameObject a;

            // Path ������ Ȯ��
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

            int childCount = lineObj.transform.childCount; // �ڽ� ��ü�� ���� ���մϴ�.

            lineRenderer.positionCount = childCount -1; // ������ �� ������ �ڽ� ��ü�� ���� �����մϴ�.

            for (int i = 0; i < lineObj.transform.childCount -1; i++)
            {
                lineRenderer.SetPosition(i, lineObj.transform.GetChild(i).position); // �� ���� ��ġ�� �ڽ� ��ü�� ��ġ�� �����մϴ�.
            }
        }
    }

    private Vector2 ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // ���� ����, �浵
        double originLatitude = StaticMapManager.latitude;
        double originLongitude = StaticMapManager.longitude;

#if UNITY_EDITOR
        originLatitude = 37.713675f;
        originLongitude = 126.743572f;
#endif

        // ���� x, y
        double originX = 0;
        double originY = 0;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        double xRatio = 172238.37f;
        double yRatio = 265780.73f;

        double x = originX + (longitude - originLongitude) * xRatio;
        double y = originY + (latitude - originLatitude) * yRatio;

        return new Vector2((float)x, (float)y);
    }
}
