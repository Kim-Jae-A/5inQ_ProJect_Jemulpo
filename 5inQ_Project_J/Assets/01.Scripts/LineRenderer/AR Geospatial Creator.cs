using System.Collections.Generic;
using UnityEngine;


public class ARGeospatialCreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
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
            clone.transform.SetParent(transform);
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i], 68);
            clone.transform.position = unityPosition;
        }

    }
    // ����, �浵, ���� Unity ��ǥ�� ��ȯ�Ͽ� ��ȯ�ϴ� �޼���
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude, double altitude)
    {
        // ������ �浵�� ���� ��ǥ�� ��ȯ
        float worldX = (float)((StaticMapManager.longitude - longitude) * 100000); // �浵 ��ȯ
        float worldZ = (float)((StaticMapManager.latitude - latitude) * 100000); // ���� ��ȯ
        float worldY = 1; // �� ��ȯ

        return new Vector3(worldX, worldY, worldZ);
    }
}
