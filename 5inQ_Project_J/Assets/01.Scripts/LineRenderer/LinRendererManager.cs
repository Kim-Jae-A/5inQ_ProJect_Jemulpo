using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinRendererManager : MonoBehaviour
{
    public Transform ARCamera;
    private LineRenderer lineRenderer;
    // ������ �浵
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
        // JsonManager Ŭ������ �ν��Ͻ��� ���� �����Ϳ� ����
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
            // Path ������ Ȯ��
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
        lineRenderer.positionCount = Longitude.Count-1;
        lineRenderer.SetPositions(LongLat); // ���� �������� ��ġ ����
    }

    void UpdateARCameraPosition()
    {
        // AR ī�޶� �ٶ󺸴� ������ �����մϴ�.
        Vector3 cameraForward = LongLat[2] - LongLat[0];
        ARCamera.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        // AR ī�޶��� ��ġ�� AR ī�޶� ȭ���� �߾ӿ� LongLat[0]�� ���̵��� �����մϴ�.
        // AR ī�޶��� ��ġ�� ������ ������ ����Ͽ� �����մϴ�.
        Vector3 cameraPosition = LongLat[0] - cameraForward.normalized;
        ARCamera.position = cameraPosition;

        // AR ī�޶��� y ���� 2�� �����մϴ�.
        Vector3 newPosition = ARCamera.position;
        newPosition.y = 2f;
        ARCamera.position = newPosition;
    }
}

