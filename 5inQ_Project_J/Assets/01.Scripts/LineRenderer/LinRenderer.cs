using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LinRenderer : MonoBehaviour
{
    public Transform XROrigin;
    private LineRenderer lineRenderer;


    // ������ �浵
    private Vector3[] LongLat;
    private List<float> Longitude = new List<float>();
    private List<float> Latitude = new List<float>();
    private GPSmanager gpsManagr;
    private GPSCoordinatesConverter converter;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gameObject.AddComponent<GPSCoordinatesConverter>();
        converter = GetComponent<GPSCoordinatesConverter>();
        gameObject.AddComponent<GPSmanager>();
        gpsManagr = GetComponent<GPSmanager>();

    }
    private void Start()
    {

        if (gpsManagr == null || converter == null)
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
        Vector2 location = GPSmanager.GetLocation();
        Longitude.Add(location.x);
        Latitude.Add(location.x);
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
        for (int i = 0; i < Longitude.Count && i < Latitude.Count; i++)
        {
            // ������ �浵�� ����Ƽ ��ǥ�� ��ȯ
            Vector3 unityCoordinates = converter.ConvertGPSToUnityCoordinates(Latitude[i], Longitude[i]);
            // ����Ƽ ��ǥ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(XROrigin.TransformPoint(unityCoordinates));
            // ��ȯ�� Vector3�� LongLat �迭�� �߰��մϴ�.
            LongLat[i] = screenPoint;
        }
        lineRenderer.positionCount = Longitude.Count;
        lineRenderer.SetPositions(LongLat); // ���� �������� ��ġ ����
    }
}