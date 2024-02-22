using Google.XR.ARCoreExtensions;
using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARGeospatialCreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public double Originlatitude = 37.71432699999999;
    public double Originlongitude = 126.74199899999999;
    public double Originaltitude = 77.42;
    void Start()
    {
        JsonCall();
        CreateAnchor();

    }
    void JsonCall()
    {
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
    }

    void CreateAnchor()
    {
        for (int i = 0; i < Longitude.Count; i++)
        {

            // AR Geospatial Creator ���� ������Ʈ ����
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            // AR Geospatial Creator ���� ������Ʈ�� �ڽ����� Anchor ��ũ��Ʈ�� �߰�
            ARGeospatialCreatorAnchor anchorComponent = clone.AddComponent<ARGeospatialCreatorAnchor>();
            clone.transform.SetParent(transform);
            // Latitude�� Longitude ����
            //anchorComponent.SetLatitude(Latitude[i]);
            //anchorComponent.SetLongitude(Longitude[i]);
            //anchorComponent.SetAltitude(68);
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i], 68);
            clone.transform.position = unityPosition;
        }

    }
    // ����, �浵, ���� Unity ��ǥ�� ��ȯ�Ͽ� ��ȯ�ϴ� �޼���
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude, double altitude)
    {
        // ������ �浵�� ���� ��ǥ�� ��ȯ
        float worldX = (float)((longitude - Originlongitude) * 111319.9); // �浵 ��ȯ
        float worldZ = (float)((latitude - Originlatitude) * 111319.9); // ���� ��ȯ
        float worldY = (float)(altitude - Originaltitude); // �� ��ȯ

        return new Vector3(worldX, worldY, worldZ);
    }
}
