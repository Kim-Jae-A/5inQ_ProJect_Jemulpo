using System.Collections.Generic;
using UnityEngine;


public class POICreator : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public List<double> Longitude = new List<double>();
    public List<double> Latitude = new List<double>();
    public List<string> Name = new List<string>();
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
    void JsonCall()
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
                        Name.Add(zone.Name);
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
        for (int i = 0; i < Longitude.Count; i++)
        {

            // AR Geospatial Creator 게임 오브젝트 생성
            GameObject clone = Instantiate(AnchorPrefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(transform);
            clone.name = Name[i];
            Vector3 unityPosition = ConvertGeoToUnityCoordinates(Latitude[i], Longitude[i]);
            clone.transform.position = unityPosition;
        }

    }
    // 위도, 경도, 고도를 Unity 좌표로 변환하여 반환하는 메서드
    public Vector3 ConvertGeoToUnityCoordinates(double latitude, double longitude)
    {
        Start startPoint = JsonManager.instance.data.route.trafast[0].summary.start;
        List<float> startLocation = startPoint.location;
        // 위도와 경도를 월드 좌표로 변환
        float worldX = (float)((startLocation[0] - longitude) * 100000); // 경도 변환
        float worldZ = (float)((startLocation[1] - latitude) * 100000); // 위도 변환
        float worldY = 1; // 고도 변환

        return new Vector3(worldX, worldY, worldZ);
    }
}
