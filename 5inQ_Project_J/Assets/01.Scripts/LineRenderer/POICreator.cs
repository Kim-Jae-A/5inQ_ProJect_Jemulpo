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

    void JsonCall() // JSON 데이터를 받아온다
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

            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(transform);
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i]);
            clone.transform.position = LongLat[0] - unityPosition;
        }

    }
    // 위도, 경도, 고도를 Unity 좌표로 변환하여 반환하는 메서드
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude)
    {
        LinRendererManager lineRendererManager = FindObjectOfType<LinRendererManager>();
        List<double> LRMlongitude = lineRendererManager.Longitude;
        List<double> LRMlatitude = lineRendererManager.Latitude;
        float worldX = (float)((LRMlongitude[0] - longitude) * 100000); // 경도 변환
        float worldZ = (float)((LRMlatitude[0] - latitude) * 100000); // 위도 변환
        float worldY = 1; // 고도 변환
        return new Vector3(worldX, worldY, worldZ);
    }
}
