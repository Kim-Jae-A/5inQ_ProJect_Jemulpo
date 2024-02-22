using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public Transform XROrigin;
    private LineRenderer lineRenderer;

    // ������ �浵
    private Vector3[] LongLat;
    private List<double> Longitude = new List<double>();
    private List<double> Latitude = new List<double>();
    private GPSmanager gpsManager;
    private GPSCoordinatesConverter converter;

    // AR ī�޶� ��ġ ����
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
        // JsonManager Ŭ������ �ν��Ͻ��� ���� �����Ϳ� ����
        if (JsonManager.instance != null && JsonManager.instance.data != null &&
            JsonManager.instance.data.route.trafast.Count > 0)
        {
            TraFast firstTraFast = JsonManager.instance.data.route.trafast[0];

            // Path ������ Ȯ��
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
            // ��ȯ�� Vector3�� LongLat �迭�� �߰��մϴ�.
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
        lineRenderer.SetPositions(LongLat); // ���� �������� ��ġ ����

        // ù ��° ��ġ�� ARCameraPosition�� ����
        ARCameraPosition = lineRenderer.GetPosition(0);
    }
}
