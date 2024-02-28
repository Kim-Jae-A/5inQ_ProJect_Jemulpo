using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class POICreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public List<int> pointIndex = new List<int>();
    public List<string> instructions = new List<string>();
    POIManager poiManager;
    private void Awake()
    {
        poiManager = GetComponent<POIManager>();
    }
    void Start()
    {
        JsonCall();
        CreateAnchor();

    }

    void JsonCall() // JSON �����͸� �޾ƿ´�
    {
        if (poiManager.jsonFile != null)
        {
            for (int i = 0; i < poiManager.jsonFile.Count; i++)
            {
                poiManager.arZoneList = JsonUtility.FromJson<ARZoneList>(poiManager.jsonFile[i].text);
                if (poiManager.arZoneList != null)
                {
                    foreach (var zone in poiManager.arZoneList.ARzone_List)
                    {
                        Longitude.Add(zone.longitude);
                        Latitude.Add(zone.latitude);
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse JSON file.");
                }
            }

        }
        else
        {
            Debug.LogError("JSON file is missing.");
        }
    }


    void CreateAnchor()
    {
        LinRendererManager lineRendererManager = FindObjectOfType<LinRendererManager>();
        Vector3[] LongLat = lineRendererManager.LongLat;
        for (int i = 0; i < Longitude.Count; i++)
        {

            // AR Geospatial Creator ���� ������Ʈ ����
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(transform);
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i]);
            clone.transform.position = LongLat[0] - unityPosition;
        }

    }
    // ����, �浵, ���� Unity ��ǥ�� ��ȯ�Ͽ� ��ȯ�ϴ� �޼���
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude)
    {
        LinRendererManager lineRendererManager = FindObjectOfType<LinRendererManager>();
        List<double> LRMlongitude = lineRendererManager.Longitude;
        List<double> LRMlatitude = lineRendererManager.Latitude;
        float worldX = (float)((LRMlongitude[0] - longitude) * 100000); // �浵 ��ȯ
        float worldZ = (float)((LRMlatitude[0] - latitude) * 100000); // ���� ��ȯ
        float worldY = 1; // �� ��ȯ
        return new Vector3(worldX, worldY, worldZ);
    }
}
