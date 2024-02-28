using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MinMapLineRender : MonoBehaviour
{
    public Transform lineContainer;
    public RectTransform linePrefab;

    public void OnButtonEnter()
    {
        if (JsonManager.instance != null && JsonManager.instance.data != null && JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];

            RectTransform previousLine = null;

            // ���̽� �Ľ��� ��ǥ�� �̹����� �����ϰ� �̹����� ���η�����ó�� ���̰� ��ȯ���� ��θ� ǥ���Ѵ�
            for (int i = 0; i < firstTraFast.path.Count - 1; i++)
            {
                var point1 = firstTraFast.path[i];
                var point2 = firstTraFast.path[i + 1];

                Vector2 position1 = ConvertGeoToUnityCoordinate(point1[1], point1[0]);
                Vector2 position2 = ConvertGeoToUnityCoordinate(point2[1], point2[0]);

                if (previousLine != null)
                {
                    RectTransform line = Instantiate(linePrefab, lineContainer);
                    line.anchoredPosition = (position1 + position2) / 2f;

                    float distance = Vector2.Distance(position1, position2);
                    line.sizeDelta = new Vector2(distance, line.sizeDelta.y);

                    Vector2 direction = position2 - position1;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    line.rotation = Quaternion.Euler(0, 0, angle);
                }

                previousLine = Instantiate(linePrefab, lineContainer);
                previousLine.anchoredPosition = position2;
            }
        }
    }


    /// <summary>
    /// ���� �浵�� ����Ƽ ��ǥ��� ġȯ�ϴ� ��
    /// </summary>
    /// <param name="latitude">����</param>
    /// <param name="longitude">�浵</param>
    /// <returns>�Է¹��� ���� �浵�� �������� ġȯ�� Vector2 ��</returns>
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